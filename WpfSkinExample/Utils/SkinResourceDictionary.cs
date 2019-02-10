using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;

namespace WpfSkinExample
{
    public class SkinResourceDictionary : ResourceDictionary, ISupportInitialize
    {
        private static readonly Dictionary<(string name, string content), Uri> skinUris = new Dictionary<(string name, string content), Uri>();

        public static void ValidateSkins()
        {
            var names = skinUris.Select(d => d.Key.name).Distinct().ToList();
            var contentTypes = skinUris.Select(d => d.Key.content).Distinct().ToList();
            foreach(string name in names)
            {
                foreach(string content in contentTypes)
                {
                    if (!skinUris.ContainsKey((name, content)))
                        throw new Exception($"Skin \"{name}\" is missing content \"{content}\"");
                }
            }
        }

        public static void ChangeSkin(string skinName)
        {
            foreach (ResourceDictionary dict in Application.Current.Resources.MergedDictionaries)
            {
                if (dict is SkinResourceDictionary skinDict)
                    skinDict.SkinName = skinName;
                else
                    dict.Source = dict.Source;
            }
        }

        public SkinResourceDictionary() : base()
        { }

        bool IsInitializing = false;

        public string SkinName
        {
            get => skinName;
            set
            {
                skinName = value.Trim().ToUpperInvariant();
                if (!IsInitializing && skinUris.ContainsKey((skinName, skinContent)))
                    Source = skinUris[(skinName, skinContent)];
            }
        }
        private string skinName = string.Empty;

        public string SkinContent
        {
            get => skinContent;
            set
            {
                if (IsInitializing)
                    skinContent = value.Trim().ToUpperInvariant();
                else
                    throw new Exception($"\"{nameof(SkinContent)}\" property must not change at runtime");
            }
        }
        private string skinContent = string.Empty;

        public new void BeginInit()
        {
            System.Diagnostics.Debug.WriteLine("BeginInit 0");
            IsInitializing = true;
            base.BeginInit();
            System.Diagnostics.Debug.WriteLine("BeginInit 1");
        }

        public new void EndInit()
        {
            System.Diagnostics.Debug.WriteLine("EndInit 0");
            if (string.IsNullOrWhiteSpace(SkinName)) throw new Exception($"The property \"{nameof(SkinName)}\" is missing");
            if (string.IsNullOrWhiteSpace(SkinContent)) throw new Exception($"The property \"{nameof(SkinContent)}\" is missing");
            base.EndInit();
            System.Diagnostics.Debug.WriteLine("EndInit 1");
            if (skinUris.ContainsKey((SkinName, SkinContent))) skinUris.Remove((SkinName, SkinContent));
            skinUris.Add((SkinName, SkinContent), Source);
            IsInitializing = false;
            System.Diagnostics.Debug.WriteLine("EndInit 2");
        }
    }
}
