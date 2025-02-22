using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace InventoryAppAvalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly HomeViewModel _homePage = new();
    private readonly JobViewModel _jobPage = new();

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase? _currentViewModel = new();

    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    private void NavigateHome() => CurrentViewModel = _homePage;

    [RelayCommand]
    private void NavigateJob() => CurrentViewModel = _jobPage;

    /*[RelayCommand]
    private void NavigateInventory() => CurrentViewModel = _inventoryPage;

    [RelayCommand]
    private void NavigateCustomers() => CurrentViewModel = _customersPage;

    [RelayCommand]
    private void NavigateVenues() => CurrentViewModel = _venuesPage;*/


    /// <summary>
    /// MainWindowViewModel constructor
    /// </summary>
    public MainWindowViewModel()
    {
        _currentViewModel = new HomeViewModel();
    }

}
