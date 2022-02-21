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
        private const string AssetFolderPath = "Packages/jp.ayutaz.simplefoldericon/Icons";

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
            return assets.Any(str => ReplaceSeparatorChar(Path.GetDirectoryName(str)) == AssetFolderPath);
        }

        private static string ReplaceSeparatorChar(string path)
        {
            return path.Replace("\\", "/");
        }

        internal static void BuildDictionary()
        {
            var dictionary = new Dictionary<string, Texture>();

            var dir = new DirectoryInfo(AssetFolderPath);
            var info = dir.GetFiles("*.png");
            foreach (var fileInfo in info)
            {
                var texture = (Texture)AssetDatabase.LoadAssetAtPath(Path.Combine(AssetFolderPath, fileInfo.Name), typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(fileInfo.Name), texture);
            }

            IconDictionary = dictionary;
        }
    }
}