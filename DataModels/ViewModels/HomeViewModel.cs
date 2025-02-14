using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace Repository.ViewModels;

public class HomeViewModel : ViewModelbase
{
    private readonly DataService<Job> _dataService;

    public IQueryable<Job>? Jobs { get; private set; }
    public IQueryable<Job>? FilteredJobs { get; private set; }

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




    public HomeViewModel(DataService<Job> dataService)
    {
        _dataService = dataService;
    }

    public async Task LoadJobsAsync()
    {
        Jobs = (await _dataService.GetAllAsync()).AsQueryable();
        FilterJobs();
    }

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

    public void ClearFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            SearchText = string.Empty;
        }
    }
}
