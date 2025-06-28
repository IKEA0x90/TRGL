using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace TRGLC.Services
{
    class Services
    {
        public static List<FrameworkElement> FindChildrenWithTag(DependencyObject parent, object tag)
        {
            var matchingChildren = new List<FrameworkElement>();

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement element)
                {
                    if (Equals(element.Tag, tag))
                    {
                        matchingChildren.Add(element);
                    }

                    matchingChildren.AddRange(FindChildrenWithTag(element, tag));
                }
            }

            return matchingChildren;
        }

        public static void AnimateTransform(Border border, double toX, double toY) {
            var tf = border.RenderTransform as TranslateTransform
                     ?? new TranslateTransform();
            border.RenderTransform = tf;

            const double ms = 80;                   // quick, but not instant
            tf.BeginAnimation(TranslateTransform.XProperty,
                              new DoubleAnimation(toX, TimeSpan.FromMilliseconds(ms)));
            tf.BeginAnimation(TranslateTransform.YProperty,
                              new DoubleAnimation(toY, TimeSpan.FromMilliseconds(ms)));
        }

        public static void AnimateShadowDepth(DropShadowEffect shadow, double toDepth) {
            const double ms = 80;
            shadow.BeginAnimation(DropShadowEffect.ShadowDepthProperty,
                                  new DoubleAnimation(toDepth, TimeSpan.FromMilliseconds(ms)));
        }
    }
}
