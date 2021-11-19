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
