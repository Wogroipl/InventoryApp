using Microsoft.AspNetCore.Components;

namespace InventoryBlazorHybrid.Components.Pages;

public partial class EditJob : ComponentBase
{
    /*    [Parameter]
        public Guid JobId { get; set; }

        [Inject]
        public DataService<Job>? JobService { get; set; }

        [Inject]
        public DataService<Transaction>? TransactionService { get; set; }

        public Job CurrentJob { get; set; } = new();

        public IQueryable<Transaction>? Transactions { get; set; }

        protected override async Task OnInitializedAsync() => await LoadJobAsync(JobId);

        private async Task LoadJobAsync(Guid? jobId)
        {
            if (!jobId.HasValue || jobId == Guid.Empty)
            {
                CurrentJob = new Job { Id = Guid.CreateVersion7() };
            }
            else
            {
                CurrentJob = await JobService.GetByIdAsync(jobId.Value);
            }
            await LoadTransactions();
        }

        private async Task LoadTransactions()
        {
            if (CurrentJob != null)
            {
                var allTransactions = await TransactionService!.GetAllAsync();
                Transactions = allTransactions.Where(t => t.JobId == CurrentJob.Id).AsQueryable();
            }
        }


        private async Task DeleteTransaction(Guid transactionId)
        {
            await TransactionService!.DeleteAsync(transactionId);
            await LoadTransactions(); // Reload transactions to reflect the changes
        }*/
}
