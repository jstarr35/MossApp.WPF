using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using MossApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MossApp.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for RequestConfigWindow.xaml
    /// </summary>
    public partial class RequestConfigWindow : MetroWindow
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly RequestConfigViewModel _requestConfigViewModel;
        private readonly IOpenMultipleFilesControlViewModel _openMultipleFilesControlViewModel;

        public RequestConfigWindow()
        {
           // _serviceProvider = serviceProvider;
            InitializeComponent();
            this.DataContext = new RequestConfigViewModel();
          //  _requestConfigViewModel = serviceProvider.GetService<RequestConfigViewModel>();
          //  this.DataContext = _requestConfigViewModel;
         //   _openMultipleFilesControlViewModel = serviceProvider.GetService<IOpenMultipleFilesControlViewModel>();
        }

        private void GoToGitHubButtonClickHandler(object sender, RoutedEventArgs args)
        {
            OpenLink("https://github.com/spiegelp/MaterialDesignExtensions");
        }

        private void OpenLink(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
    }
}
