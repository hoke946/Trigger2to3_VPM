#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Trigger2to3
{
    public static class T23_Localization
    {
        public const string MenuPath = "Trigger2to3/Localization";

        public enum ELanguage
        {
            Japanese,
            English
        }
        private static ELanguage _language = ELanguage.Japanese;
        private static bool _read = false;

        public struct TableElement
        {
            public string japanese;
            public string english;
        }
        private static Dictionary<string, TableElement> _table = new Dictionary<string, TableElement>();

        public static void Boot()
        {
            LoadLanguageFile();
            UpdateMenuChecked();
            LoadLocalizationTable();
        }

        public static string GetWord(string key, params string[] wildcards)
        {
            if (_table.ContainsKey(key))
            {
                string word = "";
                if (_language == ELanguage.Japanese) { word = _table[key].japanese; }
                else if (_language == ELanguage.English) { word = _table[key].english; }
                for (int i = 0; i < wildcards.Length; i++)
                {
                    word = word.Replace($"<f{i + 1}>", wildcards[i]);
                }
                return word;
            }
            return $"[{key}]";
        }

        [MenuItem(MenuPath + "/Japanese", false, 0)]
        public static void SetJapanese()
        {
            Execute(ELanguage.Japanese);
        }

        [MenuItem(MenuPath + "/English", false, 1)]
        public static void SetEnglish()
        {
            Execute(ELanguage.English);
        }

        private static void Execute(ELanguage language)
        {
            _language = language;
            UpdateMenuChecked();
            var path = Path.Combine(Application.temporaryCachePath, "VRTestOn");
            File.WriteAllText(path, _language.ToString());
        }

        private static void UpdateMenuChecked()
        {
            EditorApplication.delayCall += () =>
            {
                Menu.SetChecked(MenuPath + "/Japanese", _language == ELanguage.Japanese);
                Menu.SetChecked(MenuPath + "/English", _language == ELanguage.English);
            };
        }

        private static void LoadLanguageFile()
        {
            if (_read) { return; }

            try
            {
                var path = Path.Combine(Application.temporaryCachePath, "Language");
                var txt = File.ReadAllText(path);
                _language = (ELanguage)Enum.Parse(typeof(ELanguage), txt);
                _read = true;
                return;
            }
            catch
            {
                if (Application.systemLanguage == SystemLanguage.Japanese) { _language = ELanguage.Japanese; }
                else { _language = ELanguage.English; }
                _read = true;
                return;
            }
        }

        private static void LoadLocalizationTable()
        {
            var txt = (TextAsset)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath("2d6778cd6302ebf41b212ed0477d8cf3"), typeof(TextAsset));
            if (txt != null)
            {
                foreach (var line in txt.text.Split("\n"))
                {
                    if (line == "") { continue; }
                    var element = line.Split(';');
                    if (element.Length != 3)
                    {
                        Debug.LogWarning($"Syntax error in LocalizationTable. : {element[0]}");
                        continue;
                    }
                    _table.Add(element[0], new TableElement { japanese = element[1], english = element[2] });
                }
            }
        }
    }

    [InitializeOnLoad]
    public class T23_LocalizationBoot
    {
        static T23_LocalizationBoot()
        {
            T23_Localization.Boot();
        }
    }
}
#endif