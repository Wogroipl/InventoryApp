using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace Repository.ViewModels;

/// <summary>
/// View model for the home page.
/// </summary>
public class HomeViewModel : ViewModelbase
{
    // Data service for the Job model
    private readonly DataService<Job> _dataService;

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
            OnPropertyChanged(nameof(SearchText));
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
            OnPropertyChanged(nameof(StartDate));
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
            OnPropertyChanged(nameof(EndDate));
        }
    }



    /// <summary>
    /// Constructor to inject data service.
    /// </summary>
    /// <param name="dataService"></param>
    public HomeViewModel(DataService<Job> dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Loads all jobs from the data service.
    /// </summary>
    /// <returns></returns>
    public async Task LoadJobsAsync()
    {
        Jobs = (await _dataService.GetAllAsync()).AsQueryable();
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
    public async Task DeleteJob(Guid jobId)
    {
        await _dataService.DeleteAsync(jobId);
        await LoadJobsAsync();
    }
}
