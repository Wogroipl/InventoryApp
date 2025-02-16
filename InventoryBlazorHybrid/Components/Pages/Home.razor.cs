using InventoryBlazorHybrid.DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace InventoryBlazorHybrid.Components.Pages;

public partial class Home
{
    #region Dependency Injection
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private InventoryDbContext? DbContext { get; set; }
    #endregion

    #region Properties
    // Collection of jobs
    public IQueryable<Job>? Jobs { get; private set; }
    // Filtered collection of jobs
    public IQueryable<Job>? FilteredJobs { get; private set; }

    /// <summary>
    /// Search text for filtering jobs.
    /// </summary>
    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
        }
    }

    /// <summary>
    /// Start date for filtering jobs.
    /// </summary>
    private DateTime _startDate = DateTime.Today;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
        }
    }

    /// <summary>
    /// End date for filtering jobs.
    /// </summary>
    private DateTime _endDate = DateTime.Today.AddDays(30);

    public DateTime EndDate
    {
        get { return _endDate; }
        set
        {
            _endDate = value;
        }
    }
    #endregion

    #region Methods
    protected override async Task OnInitializedAsync() => await LoadJobsAsync();

    private void EditJob(Job job) => NavigationManager?.NavigateTo($"/job/{job.Id}");

    /// <summary>
    /// Loads all jobs from the data service.
    /// </summary>
    /// <returns></returns>
    public async ValueTask LoadJobsAsync()
    {
        var result = await DbContext!.Jobs
            .Include(j => j.Customer)
            .Include(j => j.Venue)
            .Include(j => j.Transactions)
            .ToListAsync();
        Jobs = result.AsQueryable();
        FilterJobs();
    }

    /// <summary>
    /// Filters jobs based on search text.
    /// </summary>
    public void FilterJobs()
    {
        if (Jobs == null)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredJobs = Jobs;
        }
        else
        {
            FilteredJobs = Jobs.Where(j => j.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Deletes a job from the data service.
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public async Task DeleteJob(Job job)
    {
        DbContext!.Remove<Job>(job);
        await DbContext.SaveChangesAsync();
        await LoadJobsAsync();
    }
    #endregion
}
