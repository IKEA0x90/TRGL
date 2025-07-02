using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TRGLC.Shop {
    public partial class ItemGrid : UserControl {
        public ItemGrid() {
            InitializeComponent();
        }

        private void ItemGrid_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            var matchingElements = Services.Services.FindChildrenWithTag(this, "ItemButton");
            foreach (FrameworkElement element in matchingElements) {
                if (element is Border itemButton) {
                    itemButton.MouseLeftButtonDown += ItemButton_MouseLeftButtonDown;
                }
            }
        }

        private void ItemButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Border stickyButton = (Border)sender;

            var parentGrid = VisualTreeHelper.GetParent(stickyButton) as Grid;
            if (parentGrid != null) {
                var effectBorder = parentGrid.FindName("ItemLight") as Border;
                if (effectBorder != null) {
                    effectBorder.Opacity = 0.7;
                }
            }
        }
    }
}
