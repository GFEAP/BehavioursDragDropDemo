using BehavioursDragDropDemo.DropCartItemPopupDomain;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BehavioursDragDropDemo.MainWindowDomain
{
    public class MainWindowViewModel : ViewModelBase<MainWindowModel>
    {
        public MainWindowViewModel() : base(new MainWindowModel())
        {
            StockCollection = MainWindowMockViewModel.StockCollectionMock;
            CartCollection = new ObservableCollection<Article>();

            DropCartItemPopupVm = new DropCartItemPopupViewModel<int>()
            {
                Button1Content = "OK",
                Button2Content = "Cancel",
                Button1CommandAction = new Action(OK_ButtonAction),
                Label1Content = "Quantity",
                TextBox1Text = 10,
            };
        }

        // how to close a Window using MVVM without dying on the attempt...
        private Action CloseWindowAction;

        //Action assigned to the "Popup" button
        #region Actions
        public void OK_ButtonAction()
        {
            for (int i = 0; i < DropCartItemPopupVm.TextBox1Text; i++)
                AddToCart(SelectedStockItem);
        }
        
        public void SetCloseWindowAction(Action closeAction)
        {
            if (CloseWindowAction == null)
                CloseWindowAction = new Action(closeAction);
        }
        #endregion

        #region Model Properties     
        public DropCartItemPopupViewModel<int> DropCartItemPopupVm
        {
            get => Model.DropCartItemPopupVm;
            set
            {
                if (Model.DropCartItemPopupVm != value)
                {
                    Model.DropCartItemPopupVm = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Article SelectedStockItem
        {
            get => Model.SelectedStockItem;
            set
            {
                if (Model.SelectedStockItem != value)
                {
                    Model.SelectedStockItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Article> StockCollection
        {
            get => Model.StockCollection;
            set
            {
                if (Model.StockCollection != value)
                {
                    Model.StockCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Article> CartCollection
        {
            get => Model.CartCollection;
            set
            {
                if (Model.CartCollection != value)
                {
                    Model.CartCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Article SelectedCartItem
        {
            get => Model.SelectedCartItem;
            set
            {
                if (Model.SelectedCartItem != value)
                {
                    Model.SelectedCartItem = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Command Implementation
        /// <summary>
        /// uses a custom parameter, 
        /// <see cref="MouseMoveEventParameters"/>see
        /// </summary>
        /// <param name="parameter">MouseMoveEventParameters</param>
        private void MouseMoveAction(object parameter)
        {
            if (SelectedStockItem == null)
                return;

            var p = parameter as MouseBehaviour.MouseMoveEventParameters;

            if (p.MouseArgs.LeftButton == MouseButtonState.Pressed)
            {
                DragDropEffects dragDropEffects = DragDrop.DoDragDrop((DependencyObject)p.element, (object)SelectedStockItem, DragDropEffects.Copy);

                if (dragDropEffects != DragDropEffects.None)
                {
                    //... any actions to be performed after successful drop goes here
                    // NotifyPropertyChanged("StockCollection");
                }
            }
        }

        private void AddToCart(Article articleToBeAddedToCart)
        {
            if (articleToBeAddedToCart != null && articleToBeAddedToCart.Quantity > 0)
            {
                try
                {
                    var a = CartCollection.SingleOrDefault<Article>(x => x.ArticleId == articleToBeAddedToCart.ArticleId);

                    if (a == null)
                    {
                        var newarticle = new Article()
                        {
                            ArticleId = articleToBeAddedToCart.ArticleId,
                            Description = articleToBeAddedToCart.Description,
                            Quantity = 1
                        };
                        CartCollection.Add(newarticle);
                    }
                    else
                        a.Quantity++;

                    if (articleToBeAddedToCart.Quantity > 0)
                        articleToBeAddedToCart.Quantity--;

                    if (articleToBeAddedToCart.Quantity == 0 && StockCollection.Contains(articleToBeAddedToCart))
                        StockCollection.Remove(articleToBeAddedToCart);
                    //NotifyPropertyChanged("CartCollection");
                    //NotifyPropertyChanged("StockCollection");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void RemoveFromCart(Article articleToBeRemovedFromCart)
        {
            if (articleToBeRemovedFromCart != null && articleToBeRemovedFromCart.Quantity > 0)
            {
                try
                {
                    var a = StockCollection.SingleOrDefault<Article>(x => x.ArticleId == articleToBeRemovedFromCart.ArticleId);

                    if (a == null)
                    {
                        var newarticle = new Article()
                        {
                            ArticleId = articleToBeRemovedFromCart.ArticleId,
                            Description = articleToBeRemovedFromCart.Description,
                            Quantity = articleToBeRemovedFromCart.Quantity
                        };
                        StockCollection.Add(newarticle);
                    }
                    else
                        a.Quantity+= articleToBeRemovedFromCart.Quantity;
                                       
                    // the condition should always be true
                    if (CartCollection.Contains(articleToBeRemovedFromCart))
                        CartCollection.Remove(articleToBeRemovedFromCart);                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void DropAction(object commandParameter)
        {
            if (commandParameter is DragDropBehaviours.DropEventParameters d)
            {
                if (d.element is ListBox lb)
                {
                    if (d.EventArgs.Data.GetDataPresent(typeof(Article)))
                    {
                        SelectedStockItem = (Article)d.EventArgs.Data.GetData(typeof(Article));
                        ShowAddToCartPopup();
                        d.EventArgs.Handled = true;
                    }
                }
            }
        }

        private void ShowAddToCartPopup()
        {
            DropCartItemPopupVm.TextBox1Text = SelectedStockItem.Quantity;
            DropCartItemPopupVm.Show();
        }

        private void DragEnterAction(object obj)
        {
            if (obj is DragDropBehaviours.DragEnterEventParameters e)
            {
                try
                {
                    e.EventArgs.Effects = DragDropEffects.Copy;
                }
                catch (Exception)
                { }
            }
        }

        private void DragOverAction(object obj)
        {
            if (obj is DragDropBehaviours.DragOverEventParameters e)
            {
                try
                {
                    e.EventArgs.Effects = DragDropEffects.Copy;
                }
                catch (Exception)
                { }
            }
        }
        private void AddToCartCommandInvoker(object commandParameter)
        {
            if (SelectedStockItem != null)
            {
                ShowAddToCartPopup();
            }
        }
        private void RemoveFromCartCommandInvoker(object obj)
        {
            if(SelectedCartItem!=null)
            {
                RemoveFromCart(SelectedCartItem);
            }
        }

        private void CloseWindowCommandInvoker(object commandParameter)
        {
            CloseWindowAction?.Invoke();
        }

        #endregion

        #region ICommand

        private ICommand _CloseWindowCommand;
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_CloseWindowCommand == null)
                    _CloseWindowCommand = new RelayCommand(CloseWindowCommandInvoker);
                return _CloseWindowCommand;
            }
        }
        private ICommand _MouseMoveCommand;
        public ICommand MouseMoveCommand
        {
            get
            {
                if (_MouseMoveCommand == null)
                    _MouseMoveCommand = new RelayCommand(MouseMoveAction);
                return _MouseMoveCommand;
            }
        }

        private ICommand _DropItemIntoCartCommand;
        public ICommand DropItemIntoCartCommand
        {
            get
            {
                if (_DropItemIntoCartCommand == null)
                    _DropItemIntoCartCommand = new RelayCommand(DropAction);
                return _DropItemIntoCartCommand;
            }
        }

        private ICommand _DragEnterCommand;
        public ICommand DragEnterCommand
        {
            get
            {
                if (_DragEnterCommand == null)
                    _DragEnterCommand = new RelayCommand(DragEnterAction);
                return _DragEnterCommand;
            }
        }

        private ICommand _DragOverCommand;
        public ICommand DragOverCommand
        {
            get
            {
                if (_DragOverCommand == null)
                    _DragOverCommand = new RelayCommand(DragOverAction);
                return _DragOverCommand;
            }
        }

        private ICommand _AddToCartCommand;
        public ICommand AddToCartCommand
        {
            get
            {
                if (_AddToCartCommand == null)
                    _AddToCartCommand = new RelayCommand(AddToCartCommandInvoker);
                return _AddToCartCommand;
            }
        }

        private ICommand _RemoveFromCartCommand;
        public ICommand RemoveFromCartCommand
        {
            get
            {
                if (_RemoveFromCartCommand == null)
                    _RemoveFromCartCommand = new RelayCommand(RemoveFromCartCommandInvoker);
                return _RemoveFromCartCommand;
            }
        }
        #endregion
    }
}