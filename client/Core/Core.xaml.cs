using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TRGLC 
{
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Core_Loaded(object sender, RoutedEventArgs e) {
            var matchingElements = Services.Services.FindChildrenWithTag(this, "StickyButton");
            foreach (FrameworkElement element in matchingElements) {
                if (element is Border stickyButton) {
                    stickyButton.MouseLeftButtonDown += StickyButton_MouseLeftButtonDown;
                }
            }
        }

        private void StickyButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Border stickyButton = (Border)sender;
            stickyButton.Background = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
