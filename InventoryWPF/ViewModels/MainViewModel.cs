using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryWPF.Factories;
using Repository.Core;

namespace InventoryWPF.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private int _paneWidth = 200;

    [ObservableProperty]
    private PageType _activePage;

    private PageFactory _pageFactory;

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    // Toggle the pane
    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
        PaneWidth = IsPaneOpen ? 200 : 48;
    }

    //Generic method to navigate to a page
    [RelayCommand]
    private void Navigate(PageType pageType)
    {
        CurrentPage = _pageFactory.GetViewModel(pageType);
        ActivePage = pageType;
    }

    /// <summary>
    /// MainWindowViewModel constructor
    /// </summary>

    public MainViewModel(PageFactory pageFactory)
    {
        PageName = PageType.Main;
        _pageFactory = pageFactory;
        Navigate(PageType.Inventory);
    }
}
