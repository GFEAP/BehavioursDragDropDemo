using System.Windows;
using System.Windows.Input;

namespace Framework
{
    /// <summary>
    /// https://www.codeproject.com/tips/478643/mouse-event-commands-for-mvvm
    /// Use MouseMoveEventParameters for passing UIelement and MouseArgs
    /// </summary>
    public class MouseBehaviour
    {

        #region MouseMove
        public static readonly DependencyProperty MouseMoveCommandProperty = DependencyProperty.RegisterAttached("MouseMoveCommand",
            typeof(ICommand),
            typeof(MouseBehaviour), new FrameworkPropertyMetadata(
            new PropertyChangedCallback(MouseMoveCommandChanged)));

        /// <summary>
        /// If mouse move command is attached by instantiating it in XAML
        /// the event will be registered
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.MouseMove += new MouseEventHandler(Element_MouseMove);
        }

        private static void Element_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            var c = new MouseMoveEventParameters
            {
                element = element,
                MouseArgs = e
            };
            ICommand command = GetMouseMoveCommand(element);

            command.Execute(c);
        }

        public static void SetMouseMoveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseMoveCommandProperty, value);
        }

        public static ICommand GetMouseMoveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseMoveCommandProperty);
        }

        public class MouseMoveEventParameters
        {
            public FrameworkElement element;
            public MouseEventArgs MouseArgs;
        }
        #endregion

        #region MouseWheel
        public static readonly DependencyProperty MouseWheelCommandProperty = DependencyProperty.RegisterAttached("MouseWheelCommand",
              typeof(ICommand),
              typeof(MouseBehaviour), new FrameworkPropertyMetadata(
              new PropertyChangedCallback(MouseWheelCommandChanged)));

        /// <summary>
        /// If mouse move command is attached by instantiating it in XAML
        /// the event will be registered
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void MouseWheelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.MouseWheel += Element_MouseWheel;
        }

        private static void Element_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            var c = new MouseWheelEventParameters
            {
                element = element,
                MouseArgs = e
            };

            ICommand command = GetMouseWheelCommand(element);

            command.Execute(c);
        }

        public static void SetMouseWheelCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseWheelCommandProperty, value);
        }

        public static ICommand GetMouseWheelCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseWheelCommandProperty);
        }

        public class MouseWheelEventParameters
        {
            public FrameworkElement element;
            public MouseEventArgs MouseArgs;
        }
    }
    #endregion
}
