using Repository.Core;
using Repository.DataAccess;
using Repository.Models;

namespace Repository.ViewModels;

/// <summary>
/// View model for the Job page.
/// </summary>
public class JobViewModel : ViewModelbase
{
    // Data service for the Job model
    private readonly DataService<Job> _jobService;
    private readonly DataService<Transaction> _transactionService;


    // Current job
    private Job? _currentJob;

    public Job CurrentJob
    {
        get { return _currentJob; }
        set
        {
            _currentJob = value;
            OnPropertyChanged(nameof(CurrentJob));
        }
    }

    // Collection of transactions
    public IQueryable<Transaction>? Transactions { get; private set; }

    /// <summary>
    /// Constructor to inject DataService instance.
    /// </summary>
    /// <param name="jobService"></param>
    public JobViewModel(DataService<Job> jobService, DataService<Transaction> transactionService)
    {
        _jobService = jobService;
        _transactionService = transactionService;
        CurrentJob = new();
    }

    /// <summary>
    /// Load a job by Id.
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public async Task LoadJobAsync(Guid jobId)
    {
        if (jobId == Guid.Empty)
        {
            CurrentJob = new();
            CurrentJob.Id = Guid.CreateVersion7();
        }
        CurrentJob = await _jobService.GetByIdAsync(jobId);
        await LoadTransactions();
    }

    public async Task LoadTransactions()
    {
        if (CurrentJob != null)
        {
            var allTransactions = await _transactionService.GetAllAsync();
            Transactions = allTransactions.Where(t => t.JobId == CurrentJob.Id).AsQueryable();
        }
    }
    public async Task DeleteTransactionAsync(Guid transactionId)
    {
        await _transactionService.DeleteAsync(transactionId);
        await LoadTransactions(); // Reload transactions to reflect the changes
    }
}
