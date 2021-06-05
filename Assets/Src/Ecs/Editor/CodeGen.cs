using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Ecs;

namespace EcsEditor
{
    public class CodeGen
    {
        private string component;

        private Assembly assembly;

        private List<Type> types;

        public CodeGen()
        {
            assembly = typeof(Context).Assembly;
            component = typeof(IComponent).FullName;

            types = new List<Type>();
        }

        public void Process()
        {
            scanTypes();

            componentPools();

            entityPool();

            components();

            entity();

            listener();
        }

        #region -= basic =-
        private void scanTypes()
        {
            foreach (var type in assembly.GetTypes())
                if (type.HasInterface(component)) types.Add(type);
        }

        private string date()
        {
            return DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        }
        #endregion

        #region -= pools =-
        private void componentPools()
        {
            var pool = File.ReadAllText(TEMPLATE.POOL);
            var pools = new List<string>();

            foreach (var obj in types)
                pools.Add(createPool(pool, obj));
            
            var text = string.Format(
                File.ReadAllText(TEMPLATE.POOLS),
                pools.Join("\r\n\r\n\t"),
                date());

            File.WriteAllText(GENERATED.POOLS, text);
        }

        private string createPool(string format, Type type)
        {
            var sets = new List<string>();

            foreach (var field in type.GetFields())
                sets.Add(string.Format("obj.{0} = default({1});", field.Name, field.FieldType));

            return string.Format(format, type, type.Name, sets.Join("\r\n\t\t\t"));
        }

        private void entityPool()
        {
            var pools = new List<string>();
            var creates = new List<string>();
            var props = new List<string>();

            foreach (var obj in types)
            {
                var nameLower = obj.Name.ToLower();

                pools.Add(string.Format("public IPool<{0}> {1} {{ get; private set; }}", obj, nameLower));
                creates.Add(string.Format("{0} = new {1}Pool();", nameLower, obj.Name));
                props.Add(string.Format("IPool<{0}> {1} {{ get; }}", obj, nameLower));
            }

            var text = string.Format(
                File.ReadAllText(TEMPLATE.ENTITY_POOL),
                pools.Join("\r\n\t\t"),
                creates.Join("\r\n\t\t\t"),
                props.Join("\r\n\t\t"),
                date());

            File.WriteAllText(GENERATED.ENTITY_POOL, text);
        }
        #endregion

        #region -= components =-
        private void components()
        {
            var format = File.ReadAllText(TEMPLATE.COMPONENT);

            foreach (var obj in types)
                createComponent(format, obj);
        }

        private void createComponent(string format, Type type)
        {
            var args = new List<string>();
            var sets = new List<string>();

            var lowerName = type.Name.ToLower();

            foreach(var obj in type.GetFields())
            {
                args.Add(string.Format("{0} {1}", obj.FieldType, obj.Name));
                sets.Add(string.Format("{0}.{1} = {1};", lowerName, obj.Name));
            }

            var text = string.Format(
                format,
                type,
                type.Name,
                lowerName,
                args.Join(", "),
                sets.Join("\r\n\t\t\t"),
                date());

            var path = string.Format(GENERATED.COMPONENT, type.Name);

            File.WriteAllText(path, text);
        }
        #endregion

        private void entity()
        {
            var clear = new List<string>();
            var react = new List<string>();

            foreach (var obj in types)
            {
                clear.Add(string.Format("unset{0}(false);", obj.Name));
                react.Add(string.Format("addListener(obj as Listener<{0}>);", obj));
            }

            var text = string.Format(
                File.ReadAllText(TEMPLATE.ENTITY),
                clear.Join("\r\n\t\t\t"),
                react.Join("\r\n\t\t\t"),
                date());

            File.WriteAllText(GENERATED.ENTITY, text);
        }

        private void listener()
        {
            var clear = new List<string>();

            foreach (var obj in types)
                clear.Add(string.Format("on{0}.Clear();", obj.Name));

            var text = string.Format(
                File.ReadAllText(TEMPLATE.ENTITY_LISTENER),
                clear.Join("\r\n\t\t\t"),
                date());

            File.WriteAllText(GENERATED.ENTITY_LISTENER, text);
        }
    }

    public static class GenExtension
    {
        public static bool HasInterface(this Type type, string name)
        {
            return type.GetInterface(name) != null;
        }

        public static string Join(this List<string> list, string separator)
        {
            return string.Join(separator, list.ToArray());
        }
    }
}
