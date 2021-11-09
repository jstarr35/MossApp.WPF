using MossApp.Utilities;
using MossApp.Utilities.Wrapper;

namespace MossApp.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Private Members

        private bool mIsSettingsOpen;

        #endregion

        #region Public Properties

        //public AccordianViewModel Accordian
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<AccordianViewModel>();
        //    }
        //}

        //public DisplayGridViewModel DisplayGrid
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<DisplayGridViewModel>();
        //    }
        //}

        //public SettingsViewModel Settings
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<SettingsViewModel>();
        //    }
        //}

        public bool IsSettingsOpen
        {
            get => mIsSettingsOpen;
            set => SetProperty(ref mIsSettingsOpen, value);
        }

        public RelayCommand OpenSettings { get; set; }

        #endregion Public Properties

        #region Constructor

        public MainViewModel()
        {
            OpenSettings = new RelayCommand((object o) => { IsSettingsOpen = true; });
        }


        #endregion

    }
}
