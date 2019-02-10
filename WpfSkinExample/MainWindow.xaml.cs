using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfSkinExample
{
    public partial class MainWindow : Window, IDisposable
    {
        private readonly App app = App.Current as App;

        public MainWindow()
        {
            InitializeComponent();

            app.SkinChangedEvent += SkinUpdated;

            Loaded += (s1, e1) 
                => app.MainWindow.Closing += (s2, e2) 
                => Dispose();
        }

        private void SkinUpdated(object sender, object eventArgs)
        {
            SquareContainer.Child = new Square();
        }

        public void Dispose()
        {
            app.SkinChangedEvent -= SkinUpdated;
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            // TODO save user preferences

            var menu = (MenuItem)sender;
            app.ActiveSkin = (string)menu.Tag;

            foreach(MenuItem m in ((MenuItem)menu.Parent).ItemContainerGenerator.Items)
                m.IsChecked = (menu.Tag.Equals(m.Tag));
        }
    }
}
