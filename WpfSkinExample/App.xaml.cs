using System.Windows;

namespace WpfSkinExample
{
    // If the compiler complains about a missing Main entrypoint, set the
    // App.xaml Build Action to ApplicationDefinition in the file properties.

    public partial class App : SkinnedApplication
    {
        public App() : base()
        { }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SkinResourceDictionary.ValidateSkins();

            // TODO load user preferences, set menu checkmark to match selected skin

            // without this, we would start with the last skin defined in the XAML
            ActiveSkin = DefaultSkinName; 

            // now we can start the party
            MainWindow w = new MainWindow();
            w.Show();
        }
    }
}
