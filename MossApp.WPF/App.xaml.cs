using Microsoft.Extensions.DependencyInjection;
using MossApp.Data;
using MossApp.Data.Models;
using MossApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MossApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<MossResultsContext>();
            services.AddTransient<IMossResultsRepository, MossResultsRepository>();
            //services.AddSingleton<MossRepository<Results>>(s => 
            //new MossRepository<Results>("Data Source=(localdb)\\ProjectsV13;Initial Catalog=MossDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            //services.AddSingleton<MossRepository<MatchPair>>(s =>
            //new MossRepository<MatchPair>("Data Source=(localdb)\\ProjectsV13;Initial Catalog=MossDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddSingleton<MainWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
