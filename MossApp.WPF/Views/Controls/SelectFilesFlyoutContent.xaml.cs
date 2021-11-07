using MaterialDesignExtensions.Controls;
using MossApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MossApp.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for SelectFilesFlyoutContent.xaml
    /// </summary>
    public partial class SelectFilesFlyoutContent : UserControl
    {
        private readonly IOpenMultipleFilesControlViewModel _vm;
        public SelectFilesFlyoutContent()
        {
           
            InitializeComponent();
        }

        private void OpenMultipleFilesControl_DirectoriesSelected(object sender, RoutedEventArgs args)
        {
            if (args is FilesSelectedEventArgs eventArgs && DataContext is OpenMultipleFilesControlViewModel viewModel)
            {
                StringBuilder sb = new StringBuilder("Selected files: ");
                eventArgs.Files.ForEach(file => sb.Append($"{file}; "));
                eventArgs.Files.ForEach(f => viewModel.AddSourceFile(f));

                viewModel.SelectedAction = sb.ToString();
              
            }
        }

        private void OpenMultipleFilesControl_Cancel(object sender, RoutedEventArgs args)
        {
            if (DataContext is OpenMultipleFilesControlViewModel viewModel)
            {
                viewModel.SelectedAction = "Cancel open files";
            }
            
        }

     
    }
}
