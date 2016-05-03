using System;
using System.Windows;

namespace MultiSelect
{
    public static class MouseDrag
    {
        public static bool IsMovementBigEnough(Point previousMousePosition, Point currentPosition)
        {
            return (Math.Abs(currentPosition.X - previousMousePosition.X) >= SystemParameters.MinimumHorizontalDragDistance
                || Math.Abs(currentPosition.Y - previousMousePosition.Y) >= SystemParameters.MinimumVerticalDragDistance);
        }
    }
}
