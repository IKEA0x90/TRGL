using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

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
    }
}
