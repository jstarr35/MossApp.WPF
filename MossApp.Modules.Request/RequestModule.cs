﻿using MossApp.Modules.Request.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MossApp.Modules.Request
{
    public class RequestModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SourceFilesRegion", typeof(SelectFilesFlyoutContentView));
            regionManager.RegisterViewWithRegion("TopMenuBarRegion", typeof(TopMenuBarView));
            //regionManager.RegisterViewWithRegion("FileSelectionRegion", typeof(FileSelectionView));
        }



        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}