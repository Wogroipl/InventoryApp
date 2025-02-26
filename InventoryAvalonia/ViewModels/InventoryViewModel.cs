using Repository.Core;

namespace InventoryAppAvalonia.ViewModels;

public partial class InventoryViewModel : ViewModelBase
{
    public InventoryViewModel()
    {
        PageName = PageType.Inventory;
    }

}