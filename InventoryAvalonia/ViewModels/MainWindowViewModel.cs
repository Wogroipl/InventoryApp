using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repository.Core;

namespace InventoryAppAvalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly HomeViewModel _homePage = new();
    private readonly JobViewModel _jobPage = new();
    private readonly InventoryViewModel _inventoryPage = new();
    private readonly CustomersViewModel _customersPage = new();
    private readonly VenuesViewModel _venuesPage = new();

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase? _currentViewModel = new();

    [ObservableProperty]
    private PageType _activePage;

    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    private void NavigateHome()
    {
        CurrentViewModel = _homePage;
        ActivePage = PageType.Home;
    }

    [RelayCommand]
    private void NavigateJob()
    {
        CurrentViewModel = _jobPage;
        ActivePage = PageType.Job;
    }

    [RelayCommand]
    private void NavigateInventory()
    {
        CurrentViewModel = _inventoryPage;
        ActivePage = PageType.Inventory;
    }

    [RelayCommand]
    private void NavigateCustomers()
    {
        CurrentViewModel = _customersPage;
        ActivePage = PageType.Customers;
    }

    [RelayCommand]
    private void NavigateVenues()
    {
        CurrentViewModel = _venuesPage;
        ActivePage = PageType.Venues;
    }


    /// <summary>
    /// MainWindowViewModel constructor
    /// </summary>
    public MainWindowViewModel()
    {
        _currentViewModel = new HomeViewModel();
    }

}
