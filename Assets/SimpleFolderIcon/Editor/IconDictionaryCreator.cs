using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SimpleFolderIcon.Editor
{
    public class IconDictionaryCreator : AssetPostprocessor
    {
        internal static Dictionary<string, Texture> IconDictionary;
        private const string PackageManagerIconPath = "Packages/jp.ayutaz.simplefoldericon/Icons";
        private const string UnityPackageIconPath = "Assets/Simplefoldericon/Icons";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!ContainsIconAsset(importedAssets) &&
                !ContainsIconAsset(deletedAssets) &&
                !ContainsIconAsset(movedAssets) &&
                !ContainsIconAsset(movedFromAssetPaths))
            {
                return;
            }

            BuildDictionary();
        }

        private static bool ContainsIconAsset(IEnumerable<string> assets)
        {
            return assets.Any(str => (ReplaceSeparatorChar(Path.GetDirectoryName(str)) == PackageManagerIconPath) || (ReplaceSeparatorChar(Path.GetDirectoryName(str)) == UnityPackageIconPath));
        }

        private static string ReplaceSeparatorChar(string path)
        {
            return path.Replace("\\", "/");
        }

        internal static void BuildDictionary()
        {
            var dictionary = new Dictionary<string, Texture>();
            var iconFolderPath = GetIconPath();
            var dir = new DirectoryInfo(iconFolderPath);
            var info = dir.GetFiles("*.png");
            foreach (var fileInfo in info)
            {
                var texture = (Texture)AssetDatabase.LoadAssetAtPath(Path.Combine(iconFolderPath, fileInfo.Name), typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(fileInfo.Name), texture);
            }

            IconDictionary = dictionary;
        }

        private static string GetIconPath()
        {
            if (Directory.Exists(PackageManagerIconPath))
            {
                return PackageManagerIconPath;
            }

            if (Directory.Exists(UnityPackageIconPath))
            {
                return UnityPackageIconPath;
            }

            return string.Empty;
        }
    }
}