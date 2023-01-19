using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Containers.State;
public class BaseViewModel : IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingField, value)) return;

        backingField = value;
        OnPropertyChanged(propertyName);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
