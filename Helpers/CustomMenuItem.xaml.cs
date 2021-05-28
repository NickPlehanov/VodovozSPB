using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VodovozSPB.Helpers {
    /// <summary>
    /// Логика взаимодействия для CustomMenuItem.xaml
    /// </summary>
    public partial class CustomMenuItem : UserControl {
        public CustomMenuItem() {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }
        public string Header1 { get => (string)GetValue(HeaderProperty1); set => SetValue(HeaderProperty1, value); }
        public string Header2 { get => (string)GetValue(HeaderProperty2); set => SetValue(HeaderProperty2, value); }
        public string CommandParameter1 { get => (string)GetValue(CommandParameterProperty1); set => SetValue(CommandParameterProperty1, value); }
        public string CommandParameter2 { get => (string)GetValue(CommandParameterProperty2); set => SetValue(CommandParameterProperty2, value); }
        public ICommand CommandAction { get => (ICommand)GetValue(CommandActionProperty); set => SetValue(CommandActionProperty, value); }

        public static readonly DependencyProperty HeaderProperty1 = DependencyProperty.Register(nameof(Header1), typeof(string), typeof(CustomMenuItem), new PropertyMetadata(defaultValue: ""));
        public static readonly DependencyProperty HeaderProperty2 = DependencyProperty.Register(nameof(Header2), typeof(string), typeof(CustomMenuItem), new PropertyMetadata(defaultValue: ""));
        public static readonly DependencyProperty CommandParameterProperty1 = DependencyProperty.Register(nameof(CommandParameter1), typeof(string), typeof(CustomMenuItem), new PropertyMetadata(defaultValue: ""));
        public static readonly DependencyProperty CommandParameterProperty2 = DependencyProperty.Register(nameof(CommandParameter2), typeof(string), typeof(CustomMenuItem), new PropertyMetadata(defaultValue: ""));
        public static readonly DependencyProperty CommandActionProperty = DependencyProperty.Register(nameof(CommandAction), typeof(ICommand), typeof(CustomMenuItem), new PropertyMetadata(defaultValue: null));
    }
}
