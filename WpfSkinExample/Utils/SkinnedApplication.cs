using System;
using System.ComponentModel;
using System.Windows;

namespace WpfSkinExample
{
    public delegate void SkinChangedEventHandler(object sender, object eventArgs);

    public abstract class SkinnedApplication : Application, ISupportInitialize
    {
        public SkinnedApplication() : base()
        { }

        public event SkinChangedEventHandler SkinChangedEvent;

        public string DefaultSkinName { get; set; } = string.Empty;

        public string ActiveSkin
        {
            get => activeSkin;
            set
            {
                var val = value.Trim().ToUpperInvariant();
                if (activeSkin.Equals(val)) return;
                activeSkin = val;
                SkinResourceDictionary.ChangeSkin(activeSkin);
                SkinChangedEvent?.Invoke(this, activeSkin);
            }
        }
        private string activeSkin = string.Empty; // irrelevant until set

        public void BeginInit()
        { }

        public void EndInit()
        {
            if (string.IsNullOrWhiteSpace(DefaultSkinName))
                throw new Exception($"Property \"{nameof(DefaultSkinName)}\" is missing");
        }
    }
}
