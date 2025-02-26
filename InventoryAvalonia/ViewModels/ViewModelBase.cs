using CommunityToolkit.Mvvm.ComponentModel;
using Repository.Core;

namespace InventoryAppAvalonia.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private PageType _pageName;
}