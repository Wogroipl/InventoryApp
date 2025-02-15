using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Repository.Core;


/// <summary>
/// Base class for all view models. Implements the INotifyPropertyChanged interface.
/// </summary>
public class ViewModelbase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
