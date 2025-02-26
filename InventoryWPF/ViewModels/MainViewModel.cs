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
    private PageType _activePage;

    private PageFactory _pageFactory;

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    // Toggle the pane
    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    // Navigate to the home page
    [RelayCommand]
    private void NavigateHome()
    {
        CurrentPage = _pageFactory.GetViewModel(PageType.Home);
        ActivePage = PageType.Home;
    }

    //Navigate to the job page
    [RelayCommand]
    private void NavigateJob()
    {
        CurrentPage = _pageFactory.GetViewModel(PageType.Job);
        ActivePage = PageType.Job;
    }

    //Navigate to the inventory page
    [RelayCommand]
    private void NavigateInventory()
    {
        CurrentPage = _pageFactory.GetViewModel(PageType.Inventory);
        ActivePage = PageType.Inventory;
    }

    //Navigate to the customers page
    [RelayCommand]
    private void NavigateCustomers()
    {
        CurrentPage = _pageFactory.GetViewModel(PageType.Customers);
        ActivePage = PageType.Customers;
    }

    //Navigate to the venues page
    [RelayCommand]
    private void NavigateVenues()
    {
        CurrentPage = _pageFactory.GetViewModel(PageType.Venues);
        ActivePage = PageType.Venues;
    }


    /// <summary>
    /// MainWindowViewModel constructor
    /// </summary>

    public MainViewModel(PageFactory pageFactory)
    {
        PageName = PageType.Main;
        _pageFactory = pageFactory;
        NavigateHome();
    }
}
