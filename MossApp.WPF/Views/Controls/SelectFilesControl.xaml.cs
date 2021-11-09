using MaterialDesignExtensions.Controls;
using MossApp.WPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MossApp.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for SelectFilesControl.xaml
    /// </summary>
    public partial class SelectFilesControl : UserControl
    {
        public SelectFilesControl()
        {
            InitializeComponent();
        }

        private void OpenMultipleFilesControl_DirectoriesSelected(object sender, RoutedEventArgs args)
        {
            if (args is FilesSelectedEventArgs eventArgs && DataContext is OpenMultipleFilesControlViewModel viewModel)
            {
                StringBuilder sb = new StringBuilder("Selected files: ");
                eventArgs.Files.ForEach(file => sb.Append($"{file}; "));

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
