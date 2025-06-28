using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace TRGLC 
{
    public partial class MainWindow : Window {
        private Border last_clicked;
        private Dictionary<Border, StickyPanel> stickyPanels;

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
            public double restDepth;

            public StickyPanel(Border button) {
                this.button = button;
                if (button.Effect is DropShadowEffect ds)
                    this.restDepth = ds.ShadowDepth;

                switch (button.Name.ToString()) {
                    case "Common":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF555555";
                        return;

                    case "Uncommon":
                        this.panel = null;
                        this.forecolor = "#FF00FF00";
                        this.backcolor = "#FF008700";
                        return;

                    case "Legendary":
                        this.panel = null;
                        this.forecolor = "#FFFF0000";
                        this.backcolor = "#FF870000";
                        return;

                    case "Boss":
                        this.panel = null;
                        this.forecolor = "#FFFFFF00";
                        this.backcolor = "#FF979700";
                        return;

                    case "Void":
                        this.panel = null;
                        this.forecolor = "#FFCD00CD";
                        this.backcolor = "#FF6C006C";
                        return;

                    case "Lunar":
                        this.panel = null;
                        this.forecolor = "#FF0000FF";
                        this.backcolor = "#FF000095";
                        return;

                    case "Equipment":
                        this.panel = null;
                        this.forecolor = "#FFFF8000";
                        this.backcolor = "#FF8B4600";
                        return;

                    case "Utility":
                        this.panel = null;
                        this.forecolor = "#FF808080";
                        this.backcolor = "#FF2E2E2E";
                        return;

                    case "Profiles":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF555555";
                        return;

                    case "Lobbies":
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF555555";
                        return;

                    default:
                        this.panel = null;
                        this.forecolor = "#FFFFFF";
                        this.backcolor = "#FF555555";
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

            if (!reverse) {
                Color color = (Color)ColorConverter.ConvertFromString(stickyPanel.backcolor);
                stickyButton.Background = new SolidColorBrush(color);
            } else {
                Color color = (Color)ColorConverter.ConvertFromString(stickyPanel.forecolor);
                stickyButton.Background = new SolidColorBrush(color);

                double moveBy = stickyPanel.restDepth / 2;
            }

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
    }
}
