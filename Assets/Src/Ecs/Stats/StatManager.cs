using System.Collections.Generic;
using UnityEngine.Profiling;
using System.Text;
using Ecs.Pool;
using System;

namespace Ecs
{
    public class StatManager
    {
        private static List<PoolStats> data = new List<PoolStats>();

        public static IStats GetStats(Type type)
        {
            var obj = new PoolStats(type);
            data.Add(obj);

            return obj;
        }

        public static string Print()
        {
            var sb = new StringBuilder();

            foreach (var obj in data)
            {
                sb.Append(obj);
                sb.Append("\r\n");
            }

            return sb.ToString();
        }
    }
}
