
using MossApp.Data.Services;

using System.Linq;

using System.Windows;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace MossApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMossResultsRepository _repo;
        ScaleTransform buttonTransform;
        //public string TestMsg = "";
        public MainWindow(IMossResultsRepository repo)
        {
            _repo = repo;
            InitializeComponent();
        }
        //private void Enter(object sender, MouseEventArgs args)
        //{
        //    DoubleAnimation animation = new DoubleAnimation();
        //    animation.From = 1;
        //    animation.To = -1;
        //    animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
        //    animation.AutoReverse = true;
        //    animation.RepeatBehavior = RepeatBehavior.Forever;
        //    buttonTransform = new ScaleTransform(-1, 0);

        //    ClearSourceFilesButton.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
           
           
        //}

       
       
    }
}
