using System;
using InventoryAppAvalonia.ViewModels;
using Repository.Core;

namespace InventoryAppAvalonia.Factories;

public class PageFactory(Func<PageType, ViewModelBase> viewModelFactory)
{
    public ViewModelBase GetViewModel(PageType pageType) => viewModelFactory.Invoke(pageType);
}
