using MaterialDesignExtensions.Controls;
using MossApp.Modules.Request.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MossApp.Modules.Request.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class SelectFilesFlyoutContentView : UserControl
    {
        public SelectFilesFlyoutContentView()
        {
            InitializeComponent();
        }

        private void OpenMultipleFilesControl_DirectoriesSelected(object sender, RoutedEventArgs args)
        {
            if (args is FilesSelectedEventArgs eventArgs && DataContext is SelectFilesFlyoutContentViewModel viewModel)
            {
                StringBuilder sb = new StringBuilder("Selected files: ");
                eventArgs.Files.ForEach(file => sb.Append($"{file}; "));
                foreach (var f in eventArgs.Files)
                {
                    viewModel.SelectedAction = f;
                    //viewModel.SendFile();
                }


                //viewModel.SelectedAction = sb.ToString();

            }
        }

        private void OpenMultipleFilesControl_Cancel(object sender, RoutedEventArgs args)
        {
            if (DataContext is SelectFilesFlyoutContentViewModel viewModel)
            {
                viewModel.SelectedAction = "Cancel open files";
            }

        }
    }
}
