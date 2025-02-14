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
