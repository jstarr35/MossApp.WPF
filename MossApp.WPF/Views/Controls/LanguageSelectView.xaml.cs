using System.Windows.Controls;

namespace MossApp.WPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for LanguageSelectView.xaml
    /// </summary>
    public partial class LanguageSelectView : UserControl
    {
        public LanguageSelectView()
        {
            InitializeComponent();
        }

        //private void LanguageSelected(object sender, RoutedEventArgs args)
        //{
        //    if(args is LanguageSelectedEventArgs eventArgs && DataContext is RequestConfigViewModel viewModel)
        //    {
        //        viewModel.SelectedAction = 
        //    }
        //}
    }

    //public class LanguageSelectedEventArgs : EventArgs
    //{
    //    public LanguageSelectedEventArgs(string? name)
    //    {

    //    }

    //    public virtual string? Name { get; }
    //}
}
