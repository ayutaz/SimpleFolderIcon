using UnityEditor;

namespace Editor
{
    public static class ExportUnityPackage
    {
        [MenuItem("SimpleFolderIcon/Export Unity Package")]
        private static void ExportPackage()
        {
            AssetDatabase.ExportPackage(
                new string[]
                {
                    "Assets/SimpleFolderIcon",
                },
                "SimpleFolderIcon.unitypackage",
                ExportPackageOptions.Recurse |
                ExportPackageOptions.IncludeDependencies
            );
        }
    }
}