using InventoryBlazorHybrid.DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace InventoryBlazorHybrid.Components.Pages;

public partial class EditJob : ComponentBase
{
    [Parameter]
    public Guid JobId { get; set; }

    [Inject]
    private InventoryDbContext? DbContext { get; set; }

    public Job CurrentJob { get; set; } = new();

    public IQueryable<Transaction>? Transactions { get; set; }

    protected override async Task OnInitializedAsync() => await LoadJobAsync(JobId);

    private async ValueTask LoadJobAsync(Guid? jobId)
    {
        if (!jobId.HasValue || jobId == Guid.Empty)
        {
            CurrentJob = new Job();
        }
        else
        {
            if (DbContext != null)
            {
                var result = await DbContext!.Jobs
                     .Include(j => j.Customer)
                     .Include(j => j.Venue)
                     .Include(j => j.Transactions)
                        .ThenInclude(tr => tr.Product)
                     .FirstOrDefaultAsync(j => j.Id == JobId);

                if (result != null)
                {
                    CurrentJob = result;
                    await LoadTransactions();
                }
            }
        }
    }

    private async ValueTask LoadTransactions()
    {
        if (CurrentJob != null)
        {
            var result = await DbContext!.Transactions
                .Where(t => t.JobId == CurrentJob.Id).ToListAsync();
            Transactions = result.AsQueryable();
        }
    }


    private async Task DeleteTransaction(Guid transactionId)
    {
        var transaction = await DbContext!.Transactions.FindAsync(transactionId);
        if (transaction != null)
        {
            DbContext.Transactions.Remove(transaction);
            await DbContext.SaveChangesAsync();
            await LoadTransactions(); // Reload transactions to reflect the changes
        }
    }
}
