using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Repository.Core;
using Repository.Models;

namespace InventoryAppAvalonia.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    #region Properties
    // Collection of jobs
    public ObservableCollection<Job>? Jobs { get; private set; }
    // Filtered collection of jobs
    public ObservableCollection<Job>? FilteredJobs { get; private set; }

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

    //public InventoryDbContext DbContext { get; }
    #endregion

    #region Constructor
    public HomeViewModel()
    {
        // DbContext = dbContext;
        //LoadJobsAsync().ConfigureAwait(false);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Loads all jobs from the data service.
    /// </summary>
    /// <returns></returns>
   /* public async ValueTask LoadJobsAsync()
    {
        if (Jobs is null)
        {
            var result = await DbContext!.Jobs
                //.Include(j => j.Customer)
                //.Include(j => j.Venue)
                .ToListAsync();
            Jobs = new ObservableCollection<Job>(result);
            FilterJobs();
        }
    }*/

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

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var filtered = Jobs.Where(j =>
                (string.IsNullOrWhiteSpace(SearchText) || j.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
                (dateRange.Includes(j.Loadin) || dateRange.Includes(j.Loadout)));
            FilteredJobs = new ObservableCollection<Job>(filtered.ToList());
        }

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
    public void FilterWeek()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(7);
        FilterJobs();
    }

    /// <summary>
    /// Filters jobs for the next month.
    /// </summary>
    public void FilterMonth()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(30);
        FilterJobs();
    }
    #endregion
}