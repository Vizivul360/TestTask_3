using UnityEditor;

namespace EcsEditor
{
    public class MenuCommands
    {
        const string ROOT = "Ecs/";

        [MenuItem(ROOT + "Generate Code")]
        static void GenerateCode()
        {
            new CodeGen().Process();

            AssetDatabase.Refresh();
        }
    }
}
