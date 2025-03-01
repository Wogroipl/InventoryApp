using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace InventoryWPF.ViewModels;

public partial class CustomersViewModel : ViewModelBase
{
    private readonly InventoryDbContext _context;

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = new();

    [RelayCommand]
    public async Task LoadCustomers()
    {
        await LoadCustomersAsync();
    }

    public CustomersViewModel(InventoryDbContext context)
    {
        PageName = PageType.Customers;
        _context = context;
    }

    private async ValueTask LoadCustomersAsync()
    {
        var result = await _context.Customers.ToListAsync();
        Customers = [.. result];
    }


}