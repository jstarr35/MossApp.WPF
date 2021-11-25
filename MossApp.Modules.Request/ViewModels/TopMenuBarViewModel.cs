using MaterialDesignThemes.Wpf;

using MossApp.Modules.Request.Views;
using MossApp.Utilities;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MossApp.Modules.Request.ViewModels
{
    public class TopMenuBarViewModel : BindableBase
    {
        public ICommand SaveDialogCommand => new RelayCommand(ExecuteSaveDialog);

        private async void ExecuteSaveDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            SaveDialog view = new SaveDialog()
            {
                DataContext = new SaveDialog()
            };

            //show the dialog
            object result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
           => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new ProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            _ = Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
