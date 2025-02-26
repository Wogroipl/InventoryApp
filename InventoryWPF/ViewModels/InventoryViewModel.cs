using Repository.Core;

namespace InventoryWPF.ViewModels;

public partial class InventoryViewModel : ViewModelBase
{
    public InventoryViewModel()
    {
        PageName = PageType.Inventory;
    }

}