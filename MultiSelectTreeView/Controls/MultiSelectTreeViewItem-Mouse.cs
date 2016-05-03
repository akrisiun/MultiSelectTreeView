using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; 

namespace MultiSelect
{
    public partial class MultiSelectTreeViewItem
    {
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            FrameworkElement itemContent = (FrameworkElement)this.Template.FindName("headerBorder", this);
            if (!itemContent.IsMouseOver)
            {
                // A (probably disabled) child item was really clicked, do nothing here
                return;
            }

            if (IsKeyboardFocused && e.ChangedButton == MouseButton.Left) IsExpanded = !IsExpanded;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!e.Handled)
            {
                Key key = e.Key;
                switch (key)
                {
                    case Key.Left:
                        if (IsExpanded)
                        {
                            IsExpanded = false;
                        }
                        else
                        {
                            ParentTreeView.Selection.SelectParentFromKey();
                        }
                        e.Handled = true;
                        break;
                    case Key.Right:
                        if (CanExpand)
                        {
                            if (!IsExpanded)
                            {
                                IsExpanded = true;
                            }
                            else
                            {
                                ParentTreeView.Selection.SelectNextFromKey();
                            }
                        }
                        e.Handled = true;
                        break;
                    case Key.Up:
                        ParentTreeView.Selection.SelectPreviousFromKey();
                        e.Handled = true;
                        break;
                    case Key.Down:
                        ParentTreeView.Selection.SelectNextFromKey();
                        e.Handled = true;
                        break;
                    case Key.Home:
                        ParentTreeView.Selection.SelectFirstFromKey();
                        e.Handled = true;
                        break;
                    case Key.End:
                        ParentTreeView.Selection.SelectLastFromKey();
                        e.Handled = true;
                        break;
                    case Key.PageUp:
                        ParentTreeView.Selection.SelectPageUpFromKey();
                        e.Handled = true;
                        break;
                    case Key.PageDown:
                        ParentTreeView.Selection.SelectPageDownFromKey();
                        e.Handled = true;
                        break;
                    case Key.A:
                        if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                        {
                            ParentTreeView.Selection.SelectAllFromKey();
                            e.Handled = true;
                        }
                        break;
                    case Key.Add:
                        if (CanExpandOnInput && !IsExpanded)
                        {
                            IsExpanded = true;
                        }
                        e.Handled = true;
                        break;
                    case Key.Subtract:
                        if (CanExpandOnInput && IsExpanded)
                        {
                            IsExpanded = false;
                        }
                        e.Handled = true;
                        break;
                    case Key.F2:
                        if (ParentTreeView.AllowEditItems && ContentTemplateEdit != null && IsFocused && IsEditable)
                        {
                            IsEditing = true;
                        }
                        e.Handled = true;
                        break;
                    case Key.Escape:
                        StopEditing();
                        e.Handled = true;
                        break;
                    case Key.Return:
                        FocusHelper.Focus(this, true);
                        IsEditing = false;
                        e.Handled = true;
                        break;
                    case Key.Space:
                        ParentTreeView.Selection.SelectCurrentBySpace();
                        e.Handled = true;
                        break;
                }
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            // Do not call the base method because it would bring all of its children into view on
            // selecting which is not the desired behaviour.
            //base.OnGotFocus(e);
            ParentTreeView.LastFocusedItem = this;
            //System.Diagnostics.Debug.WriteLine("MultiSelectTreeViewItem.OnGotFocus(), DisplayName = " + DisplayName);
            //System.Diagnostics.Debug.WriteLine(Environment.StackTrace);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsEditing = false;
            //System.Diagnostics.Debug.WriteLine("MultiSelectTreeViewItem.OnLostFocus(), DisplayName = " + DisplayName);
            //System.Diagnostics.Debug.WriteLine(Environment.StackTrace);
        }

        //private Point _dragStartPoint;  

        //private bool IsDragGesture(Point point)
        //{
        //    var horizontalMove = Math.Abs(point.X - _dragStartPoint.X);
        //    var verticalMove = Math.Abs(point.Y - _dragStartPoint.Y);
        //    var hGesture = horizontalMove >
        //            SystemParameters.MinimumHorizontalDragDistance;
        //    var vGesture = verticalMove >
        //            SystemParameters.MinimumVerticalDragDistance;
        //    return (hGesture | vGesture);
        //}  

        //static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        //{
        //    var parent = VisualTreeHelper.GetParent(dependencyObject);
        //    if (parent == null) return null;
        //    return parent as T ?? FindAncestor<T>(parent);
        //}  

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("MultiSelectTreeViewItem.OnMouseDown(Item = " + this.DisplayName + ", Button = " + e.ChangedButton + ")");
            base.OnMouseDown(e);

            FrameworkElement itemContent = (FrameworkElement)this.Template.FindName("headerBorder", this);
            if (!itemContent.IsMouseOver)
            {
                // A (probably disabled) child item was really clicked, do nothing here
                return;
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                ParentTreeView.Selection.Select(this, false);
                e.Handled = true;
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                if (!IsSelected)
                {
                    ParentTreeView.Selection.Select(this, true);
                }
                e.Handled = true;
            }

            this.ParentTreeView.Selection.OnMouseButtonDown(this, e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            FrameworkElement itemContent = (FrameworkElement)this.Template.FindName("headerBorder", this);
            if (!itemContent.IsMouseOver)
            {
                // A (probably disabled) child item was really clicked, do nothing here
                return;
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                (ParentTreeView.Selection as SelectionMultiple).Select(this, true);
                e.Handled = true;
            }
        }

        #region Internal methods

        internal void InvokeMouseDown()
        {
            var e = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Right);
            e.RoutedEvent = Mouse.MouseDownEvent;
            this.OnMouseDown(e);
        }

        #endregion Internal methods
    }
}