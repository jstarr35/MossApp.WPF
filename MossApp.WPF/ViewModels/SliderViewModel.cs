using MossApp.Utilities.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MossApp.WPF.ViewModels
{
    public class SliderViewModel : BindableBase
    {
        private double _minimum;
        private double _maximum = 1000.0;
        private double _tickFrequency = 1.0;
        private double _value = 50.0;

        public double Minimum
        {
            get => _minimum;
            set => SetProperty(ref _minimum, value);
        }

        public double Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }

        public double TickFrequency
        {
            get => _tickFrequency;
            set => SetProperty(ref _tickFrequency, value);
        }

        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
