using CommunityToolkit.Mvvm.ComponentModel;
using Repository.Core;

namespace InventoryAppAvalonia.ViewModels;

public partial class TempViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;


    public TempViewModel()
    {
        PageName = PageType.Unknown;
    }
}
