using System;
using InventoryWinUI.ViewModels;
using Repository.Core;

namespace InventoryWinUI.Factories;

public class PageFactory(Func<PageType, ViewModelBase> viewModelFactory)
{
    public ViewModelBase GetViewModel(PageType pageType) => viewModelFactory.Invoke(pageType);
}
