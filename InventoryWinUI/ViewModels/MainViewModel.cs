using System;
using CommunityToolkit.Mvvm.ComponentModel;
using InventoryWinUI.Factories;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Repository.Core;

namespace InventoryWinUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly PageFactory _pageFactory;

    #region properties
    [ObservableProperty]
    public ViewModelBase? _currentPage;


    #endregion

    /// <summary>
    /// Initializes a new instance of the MainWindowViewModel class.
    /// </summary>
    public MainViewModel(PageFactory pageFactory)
    {
        PageName = PageType.Main;
        CurrentPage = new HomeViewModel();
        _pageFactory = pageFactory;
    }

    public void NavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        //if (args.IsSettingsSelected == true)
        //{
        //    Navigate(typeof(SettingsPage));
        //}
        //else
        if (args.SelectedItemContainer != null)
        {
            if (Enum.TryParse<PageType>(args.SelectedItemContainer.Tag.ToString(), out var pageType))
            {
                Navigate(pageType, args.RecommendedNavigationTransitionInfo);
            }
        }
    }

    private void Navigate(PageType pageType, NavigationTransitionInfo transitionInfo)
    {
        CurrentPage = _pageFactory.GetViewModel(pageType);
    }
}
