using InventoryWPF.ViewModels;
using Repository.Core;

namespace InventoryWPF.Factories;

public class PageFactory(Func<PageType, ViewModelBase> viewModelFactory)
{
    public ViewModelBase GetViewModel(PageType pageType) => viewModelFactory.Invoke(pageType);
}
