using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TRGLC.Shop {
    public partial class Item : UserControl {
        public Item() {
            InitializeComponent();
        }

        private void BG_SizeChanged(object sender, SizeChangedEventArgs e) {
            var image = sender as Image;
            image.Clip = new RectangleGeometry {
                Rect = new Rect(0, 0, image.ActualWidth, image.ActualHeight),
                RadiusX = 8,
                RadiusY = 8
            };
        }
    }
}
