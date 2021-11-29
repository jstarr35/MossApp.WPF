using System.Threading.Tasks;

namespace MossApp.WPF.Views.Services
{
    public interface IMessageDialogService
    {
        Task ShowInfoDialogAsync(string text);
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
       
    }
}