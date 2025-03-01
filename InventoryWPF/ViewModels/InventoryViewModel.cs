using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace InventoryWPF.ViewModels;

public partial class InventoryViewModel : ViewModelBase
{
    private InventoryDbContext _context { get; }

    #region Properties
    // Collection of products
    [ObservableProperty]
    public ObservableCollection<Product> _products = new();

    #endregion

    #region Commands
    [RelayCommand]
    public async Task LoadProducts()
    {
        await LoadProductsAsync();
    }

    #endregion

    #region Constructor
    /// <summary>
    /// Constructor for InventoryViewModel.
    /// </summary>
    /// <param name="context">InventoryDbContext from the Repository.DataAccess namespace</param>
    public InventoryViewModel(InventoryDbContext context)
    {
        PageName = PageType.Inventory;
        _context = context;
        LoadProductsAsync();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Load the List of Products from the database.
    /// </summary>
    private async ValueTask LoadProductsAsync()
    {
        var result = await _context.Products.ToListAsync();
        Products = [.. result];
    }

    #endregion

}