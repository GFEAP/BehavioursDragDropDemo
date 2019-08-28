using System.Collections.ObjectModel;

namespace BehavioursDragDropDemo.MainWindowDomain
{
    public class MainWindowModel : Article
    {
        public ObservableCollection<Article> StockCollection { get; internal set; }
        public ObservableCollection<Article> CartCollection { get; internal set; }
        public Article SelectedStockItem { get; internal set; }
        public Article SelectedCartItem { get; internal set; }
        public DropCartItemPopupDomain.DropCartItemPopupViewModel<int> DropCartItemPopupVm { get; internal set; }
    }
}