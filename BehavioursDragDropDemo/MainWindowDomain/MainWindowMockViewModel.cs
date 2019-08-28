using System.Collections.ObjectModel;

namespace BehavioursDragDropDemo.MainWindowDomain
{
    public static class MainWindowMockViewModel
    {
        public static ObservableCollection<Article> StockCollectionMock
            => new ObservableCollection<Article>()
            {
                new Article()
                {
                    ArticleId = "ABX001",
                    Description = "DPDT Switch 300V 5A",
                    Quantity = 20
                },
                new Article()
                {
                    ArticleId = "CRE205",
                    Description = "LED, 6400K, 3A, 11.4V",
                    Quantity = 7
                },
                new Article()
                {
                    ArticleId = "KIT687",
                    Description = "LED HeatSink Ass'y Kit",
                    Quantity = 2
                },
                new Article()
                {
                    ArticleId = "HDP045",
                    Description = "Heat Distribution Paste, White, 30g Squeeze Tube",
                    Quantity = 2
                }
            };         

        public static MainWindowViewModel MainWindowViewModelMockup
        {
            get => new MainWindowViewModel()
            {
                StockCollection = StockCollectionMock,
                CartCollection = new ObservableCollection<Article>()
            };
        }
    }
}