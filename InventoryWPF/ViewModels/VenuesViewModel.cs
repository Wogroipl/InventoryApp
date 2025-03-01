using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace InventoryWPF.ViewModels;

public partial class VenuesViewModel : ViewModelBase
{
    #region Private Fields
    private readonly InventoryDbContext _context;
    #endregion

    #region Properties
    [ObservableProperty]
    private ObservableCollection<Venue> _venues = new();
    #endregion

    #region Commands
    [RelayCommand]
    public async Task LoadVenues()
    {
        await LoadVenuesAsync();
    }
    #endregion


    public VenuesViewModel(InventoryDbContext context)
    {
        PageName = PageType.Venues;
        _context = context;
    }

    private async Task LoadVenuesAsync()
    {
        var result = await _context.Venues.ToListAsync();
        Venues = [.. result];
    }

}