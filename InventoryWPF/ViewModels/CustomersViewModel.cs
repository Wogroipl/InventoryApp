using Repository.Core;

namespace InventoryWPF.ViewModels;

public partial class CustomersViewModel : ViewModelBase
{
    public CustomersViewModel()
    {
        PageName = PageType.Customers;
    }
}