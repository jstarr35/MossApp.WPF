using System;
using System.ComponentModel;
using System.Windows;

namespace MossApp.Utilities
{
    public class ViewModelLocator
    {


        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoWireViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool),
                typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            string viewTypeName = d.GetType().FullName;
            string[] namespaces = viewTypeName.Split('.');

            string viewModelTypeName = namespaces[0] + ".ViewModels." + namespaces[3] + "Model";
            Type viewModelType = Type.GetType(viewModelTypeName);
            object viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
