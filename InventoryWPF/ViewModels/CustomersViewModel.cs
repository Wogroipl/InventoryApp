using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
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

    public CustomersViewModel(InventoryDbContext context)
    {
        PageName = PageType.Customers;
        _context = context;
        LoadCustomersAsync();
    }

    private async ValueTask LoadCustomersAsync()
    {
        var result = await _context.Customers.ToListAsync();
        Customers = new ObservableCollection<Customer>(result);
    }
}