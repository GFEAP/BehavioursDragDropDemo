using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Framework;
using System.Windows;

namespace BehavioursDragDropDemo.DropCartItemPopupDomain
{
    public class DropCartItemPopupViewModel<T> : INotifyPropertyChanged
    {
        private bool _IsVisible = false;

        private string _Button1Content;
        private string _Button2Content;

        private ICommand _Button1Command;
        private ICommand _Button2Command;

        public Action Button1CommandAction;
        public Action Button2CommandAction;

        private string _Label1Content;

        private T _TextBox1Text;
       
        public Visibility PopupVisibility
        {
            get
            {
                if (IsVisible == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public bool IsVisible
        {
            get => _IsVisible;
            internal set
            {
                if (_IsVisible != value)
                {
                    _IsVisible = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("PopupVisibility");
                }
            }
        }
        public string Button1Content
        {
            get => _Button1Content;
            set
            {
                if (_Button1Content != value)
                {
                    _Button1Content = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Button2Content
        {
            get => _Button2Content;
            set
            {
                if (_Button2Content != value)
                {
                    _Button2Content = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Label1Content
        {
            get => _Label1Content;
            set
            {
                if (_Label1Content != value)
                {
                    _Label1Content = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public T TextBox1Text
        {
            get => _TextBox1Text;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_TextBox1Text, value) == false)
                {
                    _TextBox1Text = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region ICommand Invokers
        private void Button1CommandInvoker(object commandParameter)
        {
            Button1CommandAction?.Invoke();
            // TODO: create some sort of interceptor here:
            // ValidationAction, which sets a flag within this class
            // if that class 
            IsVisible = false;
        }

        private void Button2CommandInvoker(object commandParameter)
        {
            Button2CommandAction?.Invoke();
            IsVisible = false;
        }
        #endregion

        #region ICommand
        public ICommand Button1Command
        {
            get
            {
                if (_Button1Command == null)
                    _Button1Command = new RelayCommand(Button1CommandInvoker);
                return _Button1Command;
            }
        }

        public ICommand Button2Command
        {
            get
            {
                if (_Button2Command == null)
                    _Button2Command = new RelayCommand(Button2CommandInvoker);
                return _Button2Command;

            }
        }
        #endregion

        public void Show()
        {
            IsVisible = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}