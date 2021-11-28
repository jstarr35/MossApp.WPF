using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using MossApp.Data.Services;
using MossApp.Request;
using MossApp.Utilities.Regions.Adapters;
using MossApp.WPF.ViewModels;
using MossApp.WPF.Views.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Serilog;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Unity.Microsoft.DependencyInjection;

namespace MossApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {


            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            PaletteHelper helper = new PaletteHelper();
            ITheme theme = helper.GetTheme();
            theme.SetBaseTheme(Theme.Dark);
            Color primaryColor = (Color)ColorConverter.ConvertFromString("#FFB83A4B");
            Color secondaryColor = (Color)ColorConverter.ConvertFromString("#FF009AB4");

            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(secondaryColor);
            theme.Background = (Color)ColorConverter.ConvertFromString("#292929");
            helper.SetTheme(theme);
            return Container.Resolve<MainShell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            _ = containerRegistry.Register<MossRequest>();
            _ = containerRegistry.Register<IMossResultsRepository, MossResultsRepository>();

            //  containerRegistry.Register


        }

        protected override IContainerExtension CreateContainerExtension()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            _ = serviceCollection.AddLogging(l => l.AddSerilog(dispose: true));
            Unity.UnityContainer container = new();
            _ = container.BuildServiceProvider(serviceCollection);

            return new UnityContainerExtension(container);
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(FlyoutsControl), Container.Resolve<FlyoutsControlRegionAdapter>());
            // regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new ConfigurationModuleCatalog();
        //}
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                string viewName = viewType.FullName;
                string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                string viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });

            //  ViewModelLocationProvider.Register<SelectFilesFlyoutContentView, SelectFilesFlyoutContentViewModel>();

            //   ViewModelLocationProvider.Register<SourceFileListView, SourceFileListViewModel>();
            //  ViewModelLocationProvider.Register<RequestConfigWindow, RequestConfigViewModel>();
            ViewModelLocationProvider.Register<MainShell, RequestConfigViewModel>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _ = moduleCatalog.AddModule<Modules.Request.RequestModule>();
            //moduleCatalog.AddModule<Modules.SourceFileList.SourceFileListModule>();
        }

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new ConfigurationModuleCatalog();
        //}


        // private ServiceProvider serviceProvider;
        //public App()
        //{
        //    ServiceCollection services = new ServiceCollection();
        //    ConfigureServices(services);
        //    serviceProvider = services.BuildServiceProvider();
        //}
        //private void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddDbContext<MossResultsContext>();
        //    services.AddTransient<IMossResultsRepository, MossResultsRepository>();
        //    services.AddScoped<IOpenMultipleFilesControlViewModel, OpenMultipleFilesControlViewModel>();
        //    services.AddSingleton<RequestConfigViewModel>();
        //    //services.AddSingleton<MossRepository<Results>>(s => 
        //    //new MossRepository<Results>("Data Source=(localdb)\\ProjectsV13;Initial Catalog=MossDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
        //    //services.AddSingleton<MossRepository<MatchPair>>(s =>
        //    //new MossRepository<MatchPair>("Data Source=(localdb)\\ProjectsV13;Initial Catalog=MossDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

        //    services.AddSingleton<RequestConfigWindow>();
        //}
        //private void OnStartup(object sender, StartupEventArgs e)
        //{
        //    PaletteHelper helper = new PaletteHelper();
        //    ITheme theme = helper.GetTheme();
        //    theme.SetBaseTheme(Theme.Dark);
        //    Color primaryColor = (Color)ColorConverter.ConvertFromString("#FFb83a4b");
        //    Color secondaryColor = (Color)ColorConverter.ConvertFromString("#FF175E54");

        //    theme.SetPrimaryColor(primaryColor);
        //    theme.SetSecondaryColor(secondaryColor);

        //    helper.SetTheme(theme);

        //    //var viewModelMediator = serviceProvider.GetService<ViewModelMediator>();
        //    //ViewModel[] viewModels = { new RequestConfigViewModel(), new OpenMultipleFilesControlViewModel() };
        //    //viewModelMediator.RegisterViewModels(viewModels);


        //    var requestConfigWindow = serviceProvider.GetService<RequestConfigWindow>();

        //    requestConfigWindow.Show();
        //}
    }
}
