using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace InventoryWPF.ViewModels;

public partial class VenuesViewModel : ViewModelBase
{
    private readonly InventoryDbContext _context;

    [ObservableProperty]
    private ObservableCollection<Venue> _venues = new();

    public VenuesViewModel(InventoryDbContext context)
    {
        PageName = PageType.Venues;
        _context = context;
        LoadVenuesAsync();
    }

    private void LoadVenuesAsync()
    {
        var result = _context.Venues.ToList();
        Venues = new ObservableCollection<Venue>(result);
    }

}