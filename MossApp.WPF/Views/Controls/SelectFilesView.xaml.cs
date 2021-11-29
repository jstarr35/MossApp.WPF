using MaterialDesignExtensions.Controls;
using MossApp.WPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MossApp.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class SelectFilesView : UserControl
    {
        public SelectFilesView()
        {
            InitializeComponent();
        }

        private void OpenMultipleFilesControl_FilesSelected(object sender, RoutedEventArgs args)
        {
            if (args is FilesSelectedEventArgs eventArgs && DataContext is RequestConfigViewModel viewModel)
            {
                StringBuilder sb = new("Selected files: ");
                eventArgs.Files.ForEach(file => sb.Append($"{file}; "));
                foreach (string f in eventArgs.Files)
                {
                    viewModel.SelectedAction = f;
                    //viewModel.SendFile();
                }


                //viewModel.SelectedAction = sb.ToString();

            }
        }

        private void OpenMultipleFilesControl_Cancel(object sender, RoutedEventArgs args)
        {
            if (DataContext is RequestConfigViewModel viewModel)
            {
                viewModel.SelectedAction = "Cancel open files";
            }

        }

        private void OpenMultipleFilesControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RequestConfigViewModel viewModel)
            {
                //viewModel.ControlLoaded();
            }
        }
    }
}
