using DataAccess;
using DataModels.Models;
using InventoryApp.Core;

namespace InventoryApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly DataService<Job> _dataService;

        public List<Job> Jobs { get; private set; } = new List<Job>();
        public List<Job> FilteredJobs { get; private set; } = new List<Job>();

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
            Jobs = (await _dataService.GetAllAsync()).ToList();
            FilterJobs();
        }

        public void FilterJobs()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredJobs = Jobs;
            }
            else
            {
                FilteredJobs = Jobs.Where(j => j.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

    }
}
