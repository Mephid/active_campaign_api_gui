using Contact.Service;
using Contact.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Contact
{
    public class ContactModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ModuleRegion", typeof(Views.Contact));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IContactBulkOptionsService, ContactBulkOptionsService>();
        }
    }
}