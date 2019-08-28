using System.Windows;
using System.Windows.Input;

namespace Framework
{  
    public class DragDropBehaviours
    {
        #region Drop
        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.RegisterAttached("DropCommand",
            typeof(ICommand),
            typeof(DragDropBehaviours), new FrameworkPropertyMetadata(
            new PropertyChangedCallback(DropCommandChanged)));

        private static void DropCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.Drop += Element_Drop;
        }

        private static void Element_Drop(object sender, DragEventArgs e)
        {
            FrameworkElement sendingElement = (FrameworkElement)sender;
            var c = new DropEventParameters
            {
                element = sendingElement,
                EventArgs = e
            };
            ICommand command = GetDropCommand(sendingElement);
          
            command.Execute(c);
        }       

        public static void SetDropCommand(UIElement element, ICommand value)
        {
            element.SetValue(DropCommandProperty, value);
        }

        public static ICommand GetDropCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DropCommandProperty);
        }

        public class DropEventParameters
        {
            public FrameworkElement element;
            public DragEventArgs EventArgs;
        }
        #endregion

        #region DragEnter
        private static void DragEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.DragEnter += Element_DragEnter;
        }

        private static void Element_DragEnter(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            var c = new DragEnterEventParameters
            {
                element = element,
                EventArgs = e
            };

            ICommand command = GetDragEnterCommand(element);
            command.Execute(c);
        }

        public static readonly DependencyProperty DragEnterCommandProperty = DependencyProperty.RegisterAttached("DragEnterCommand",
           typeof(ICommand),
           typeof(DragDropBehaviours), new FrameworkPropertyMetadata(
           new PropertyChangedCallback(DragEnterCommandChanged)));

        public static void SetDragEnterCommand(UIElement element, ICommand value)
        {
            element.SetValue(DragEnterCommandProperty, value);
        }

        public static ICommand GetDragEnterCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DragEnterCommandProperty);
        }

        public class DragEnterEventParameters
        {
            public FrameworkElement element;
            public DragEventArgs EventArgs;
        }
        #endregion

        #region DragOver
        private static void DragOverCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.DragOver += Element_DragOver;
        }

        private static void Element_DragOver(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            var c = new DragOverEventParameters
            {
                element = element,
                EventArgs = e
            };

            ICommand command = GetDragOverCommand(element);
            command.Execute(c);
        }

        public static readonly DependencyProperty DragOverCommandProperty = DependencyProperty.RegisterAttached("DragOverCommand",
           typeof(ICommand),
           typeof(DragDropBehaviours), new FrameworkPropertyMetadata(
           new PropertyChangedCallback(DragOverCommandChanged)));

        public static void SetDragOverCommand(UIElement element, ICommand value)
        {
            element.SetValue(DragOverCommandProperty, value);
        }

        public static ICommand GetDragOverCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DragOverCommandProperty);
        }

        public class DragOverEventParameters
        {
            public FrameworkElement element;
            public DragEventArgs EventArgs;
        }
        #endregion
    }
}