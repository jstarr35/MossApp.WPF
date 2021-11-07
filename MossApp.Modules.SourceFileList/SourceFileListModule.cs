using MossApp.Modules.SourceFileList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MossApp.Modules.SourceFileList
{
    public class SourceFileListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SourceFileListRegion", typeof(SourceFileListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}