using MossApp.Utilities;
using MossApp.Utilities.Wrapper;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MossApp.WPF.Views.Windows;
using MaterialDesignExtensions.Model;

using System.Collections.Generic;

namespace MossApp.WPF.ViewModels
{
    public class RequestConfigViewModel : BindableBase
    {
        
        public RelayCommand ShowOptionsFlyoutCommand { get; }
        public RelayCommand ShowSourceFilesFlyoutCommand { get; }
        public RelayCommand ShowBaseFileFlyoutCommand { get; }
        private ResourceDictionary DialogDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml") };
        private int _sourceButtonZIndex = 1;
        public int SourceButtonZIndex
        {
            get => _sourceButtonZIndex;
            set
            {
                SetProperty(ref _sourceButtonZIndex, value);
                
            }
        }

        private int _baseButtonZIndex;
        public int BaseButtonZIndex
        {
            get => _baseButtonZIndex;
            set
            {
                SetProperty(ref _baseButtonZIndex, value);

            }
        }

        private List<string> SourceFileList { get; set; } = new List<string>();

       


       // private readonly IOpenMultipleFilesControlViewModel _filesControlViewModel;

        private IServiceProvider _provider;
        public RequestConfigViewModel()
        {
            ShowOptionsFlyoutCommand = new RelayCommand(_ => ShowOptionsFlyout());
            ShowSourceFilesFlyoutCommand = new RelayCommand(_ => ShowSourceFilesFlyout());
            ShowBaseFileFlyoutCommand = new RelayCommand(_ => ShowBaseFileFlyout());
            //_filesControlViewModel = openMultipleFilesControlViewModel;
        }

        public Flyout? OptionsFlyout { get; set; }
        public Flyout? SourceFilesFlyout { get; set; }

        private void ShowOptionsFlyout()
        {
           
            ((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen = !((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen;
        }

        private void ShowSourceFilesFlyout()
        {
            while (BaseButtonZIndex >= SourceButtonZIndex)
            {
                SourceButtonZIndex++;
                BaseButtonZIndex--;
                
            }
            ((RequestConfigWindow)Application.Current.MainWindow).SourceFilesFlyout.IsOpen = !((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen;
        }

        public void ShowBaseFileFlyout()
        {
            while (SourceButtonZIndex >= BaseButtonZIndex)
            {
                BaseButtonZIndex++;
                SourceButtonZIndex--;

            }
        }

        //public override void Receive(string from, string message)
        //{
        //    if (from.Equals("OpenMultipleFilesControlViewModel"))
        //    {
        //        SourceFileList.Add(message);
        //    }
        //}



    }
}
