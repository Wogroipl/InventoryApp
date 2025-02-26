using CommunityToolkit.Mvvm.ComponentModel;
using Repository.Core;

namespace InventoryWPF.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private PageType _pageName;
}