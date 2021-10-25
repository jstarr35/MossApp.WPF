using System.ComponentModel;

namespace MossApp.Utilities.Wrapper
{
    public interface IValidatableTrackingObject :
      IRevertibleChangeTracking,
      INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}
