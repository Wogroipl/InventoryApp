using Repository.Core;

namespace InventoryAppAvalonia.ViewModels;

public partial class CustomersViewModel : ViewModelBase
{
    public CustomersViewModel()
    {
        PageName = PageType.Customers;
    }
}