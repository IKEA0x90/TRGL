using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace TRGLC 
{
    public partial class MainWindow : Window {
        private Border last_clicked;
        private Dictionary<Border, StickyPanel> stickyPanels;

        public Duration AnimationDuration { get; private set; }
        public TimeSpan AnimationTime { get; private set; }

        public MainWindow() {
            InitializeComponent();

            this.last_clicked = null;
            this.stickyPanels = new Dictionary<Border, StickyPanel>();
        }

        public class StickyPanel {
            public Border button;
            public UserControl panel;
            public string forecolor;
            public string backcolor;
            public string foreimage;
            public string backimage;
            public double restDepth;

            public StickyPanel(Border button) {
                this.button = button;
                if (button.Effect is DropShadowEffect ds)
                    this.restDepth = ds.ShadowDepth;

                switch (button.Name.ToString()) {
                    case "Common":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF919191";
                        this.foreimage = "BgCommonLight";
                        this.backimage = "BgCommonDark";
                        return;

                    case "Uncommon":
                        this.panel = null;
                        this.forecolor = "#FF00FF00";
                        this.backcolor = "#FF008700";
                        this.foreimage = "BgUncommonLight";
                        this.backimage = "BgUncommonDark";
                        return;

                    case "Legendary":
                        this.panel = null;
                        this.forecolor = "#FFFF0000";
                        this.backcolor = "#FF870000";
                        this.foreimage = "BgLegendaryLight";
                        this.backimage = "BgLegendaryDark";
                        return;

                    case "Boss":
                        this.panel = null;
                        this.forecolor = "#FFFFFF00";
                        this.backcolor = "#FF979700";
                        this.foreimage = "BgBossLight";
                        this.backimage = "BgBossDark";
                        return;

                    case "Void":
                        this.panel = null;
                        this.forecolor = "#FFCD00CD";
                        this.backcolor = "#FF6C006C";
                        this.foreimage = "BgVoidLight";
                        this.backimage = "BgVoidDark";
                        return;

                    case "Lunar":
                        this.panel = null;
                        this.forecolor = "#FF0000FF";
                        this.backcolor = "#FF000095";
                        this.foreimage = "BgLunarLight";
                        this.backimage = "BgLunarDark";
                        return;

                    case "Equipment":
                        this.panel = null;
                        this.forecolor = "#FFFF8000";
                        this.backcolor = "#FF8B4600";
                        this.foreimage = "BgEquipmentLight";
                        this.backimage = "BgEquipmentDark";
                        return;

                    case "Utility":
                        this.panel = null;
                        this.forecolor = "#FF808080";
                        this.backcolor = "#FF2E2E2E";
                        this.foreimage = "BgUntieredLight";
                        this.backimage = "BgUntieredDark";
                        return;

                    case "Profiles":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF919191";
                        this.foreimage = "BgCommonLight3";
                        this.backimage = "BgCommonDark3";
                        return;

                    case "Lobbies":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF919191";
                        this.foreimage = "BgCommonLight2";
                        this.backimage = "BgCommonDark2";
                        return;

                    default:
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF919191";
                        this.foreimage = "BgCommonLight";
                        this.backimage = "BgCommonDark";
                        return;
                }
            }
        }

        private void Core_Loaded(object sender, RoutedEventArgs e) {
            var matchingElements = Services.Services.FindChildrenWithTag(this, "StickyButton");
            foreach (FrameworkElement element in matchingElements) {
                if (element is Border stickyButton) {
                    this.stickyPanels.Add(stickyButton, new StickyPanel(stickyButton));
                    stickyButton.MouseLeftButtonDown += StickyButton_MouseLeftButtonDown;
                }
            }

            matchingElements = Services.Services.FindChildrenWithTag(this, "CreditsImage");
            foreach (FrameworkElement element in matchingElements) {
                if (element is Image creditsImage) {
                    creditsImage.Loaded += CreditsImage_Loaded;
                }
            }
        }

        private void StickyButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Border stickyButton = (Border)sender;            
            StickyPanel oldStickyPanel = null;

            if (this.last_clicked == stickyButton) return;

            if (this.last_clicked != null) {
                oldStickyPanel = this.stickyPanels[this.last_clicked];
                this.UpdateButton(this.last_clicked, true);
            }

            this.UpdateButton(stickyButton, false);
            this.last_clicked = stickyButton;
        }

        private void UpdateButton(Border stickyButton, bool reverse) {
            StickyPanel stickyPanel = null;

            try {
                stickyPanel = this.stickyPanels[stickyButton];
            }
            catch (KeyNotFoundException) {
                return;
            }

            stickyButton.Background = (ImageBrush)FindResource(reverse ? stickyPanel.foreimage : stickyPanel.backimage);

            /*
            Color fromColor = (Color)ColorConverter.ConvertFromString(reverse ? stickyPanel.backcolor : stickyPanel.forecolor);
            Color toColor = (Color)ColorConverter.ConvertFromString(reverse ? stickyPanel.forecolor : stickyPanel.backcolor);

            if (!(stickyButton.Background is SolidColorBrush brush) || brush.IsFrozen) {
                brush = new SolidColorBrush(fromColor);
                stickyButton.Background = brush;
            }

            var colorAnim = new ColorAnimation {
                From = fromColor,
                To = toColor,
                Duration = new Duration(AnimationTime),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnim);
            */

            if (stickyButton.Effect is DropShadowEffect shadow) {
                double originalDepth = shadow.ShadowDepth;
                double pressedDepth = 0;
                double moveBy = originalDepth / 2;

                if (!reverse)
                {
                    Services.Services.AnimateTransform(stickyButton, moveBy, moveBy);
                    Services.Services.AnimateShadowDepth(shadow, pressedDepth);
                } else
                  {
                    Services.Services.AnimateTransform(stickyButton, 0, 0);
                    Services.Services.AnimateShadowDepth(shadow, stickyPanel.restDepth);
                }
            }
        }

        private void ChangePanel(object sender) {
            
        }

        private void CreditsImage_Loaded(object sender, RoutedEventArgs e) {
            var image = sender as Image;
            if (image == null) return;

            image.Dispatcher.BeginInvoke(new Action(() =>
            {
                double actualWidth = image.ActualWidth;
                double actualHeight = image.ActualHeight;

                if (actualWidth > 0 && actualHeight > 0) {
                    image.Width = actualWidth * 0.5;
                    image.Height = actualHeight * 0.5;
                }
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }
    }
}
