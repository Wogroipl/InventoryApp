using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace InventoryWPF.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    #region Properties
    // Collection of jobs
    [ObservableProperty]
    public ObservableCollection<Job>? _jobs;
    // Filtered collection of jobs
    [ObservableProperty]
    public ObservableCollection<Job>? _filteredJobs;


    private InventoryDbContext _context { get; }

    /// <summary>
    /// Search text for filtering jobs.
    /// </summary>
    [ObservableProperty]
    private string _searchText = string.Empty;

    /// <summary>
    /// Start date for filtering jobs.
    /// </summary>
    [ObservableProperty]
    private DateTime _startDate = DateTime.Today;


    /// <summary>
    /// End date for filtering jobs.
    /// </summary>
    [ObservableProperty]
    private DateTime _endDate = DateTime.Today.AddDays(30);

    [ObservableProperty]
    private bool _includePast = false;

    #endregion

    #region Commands

    [RelayCommand]
    public async Task LoadJobs()
    {
        await LoadJobsAsync();
    }

    #endregion

    #region Constructor
    /// <summary>
    /// Constructor for HomeViewModel.
    /// </summary>
    /// <param name="context">InventoryDbContext from the Repository.DataAccess namespace</param>
    public HomeViewModel(InventoryDbContext context)
    {
        PageName = PageType.Home;
        _context = context;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Loads all jobs from the data service.
    /// </summary>
    /// <returns></returns>
    public async ValueTask LoadJobsAsync()
    {
        var result = await _context!.Jobs
            .Include(j => j.Customer)
            .Include(j => j.Venue)
            .ToListAsync();
        Jobs = new ObservableCollection<Job>(result);
        FilterJobs();

    }

    /// <summary>
    /// Filters jobs based on search text and date range.
    /// </summary>
    public void FilterJobs()
    {

        if (Jobs == null)
        {
            return;
        }
        DateRange dateRange = new((DateTime)StartDate, (DateTime)EndDate);

        if (IncludePast)
        {
            dateRange = new(DateTime.MinValue, (DateTime)EndDate!);
        }

        var filtered = Jobs.Where(j => j.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) &&
            (dateRange.Includes(j.Loadin) || dateRange.Includes(j.Loadout)));

        FilteredJobs = new ObservableCollection<Job>(filtered);
    }

    /// <summary>
    /// Deletes a job from the data service.
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    /*public async Task DeleteJob(Job job)
    {
        DbContext!.Remove<Job>(job);
        await DbContext.SaveChangesAsync();
        await LoadJobsAsync();
    }*/

    /// <summary>
    /// Filters jobs for the next week.
    /// </summary>#
    [RelayCommand]
    public void FilterWeek()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(7);
        FilterJobs();
    }

    /// <summary>
    /// Filters jobs for the next month.
    /// </summary>
    [RelayCommand]
    public void FilterMonth()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(30);
        FilterJobs();
    }
    #endregion
}