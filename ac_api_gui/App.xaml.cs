using ac_api_gui.Views;
using Contact;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Reflection;
using System.Windows;

namespace ac_api_gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var startWindow = Container.Resolve<MainWindow>();
            return startWindow;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ContactModule>();
        }
    }
}
