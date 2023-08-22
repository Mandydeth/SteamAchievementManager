﻿using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using DevExpress.Mvvm.UI.Interactivity;

namespace SAM.WPF.Core.Behaviors
{
    public class AutoResizeBehavior : Behavior<UniformGrid>
    {
        public static readonly DependencyProperty MaxItemWidthProperty =
            DependencyProperty.Register(nameof(MaxItemWidth), typeof(double), typeof(AutoResizeBehavior),
            new FrameworkPropertyMetadata((double) 300, OnMaxItemWidthChanged));

        
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(nameof(ColumnCount), typeof(int), typeof(AutoResizeBehavior),
                new FrameworkPropertyMetadata(4));

        private static void OnMaxItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        
        public int ColumnCount
        {
            get { return (int) GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public double MaxItemWidth
        {
            get { return (double) GetValue(MaxItemWidthProperty); }
            set { SetValue(MaxItemWidthProperty, value); }
        }
        
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SizeChanged += OnSizeChanged;
            Update();
        }
        
        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= OnSizeChanged;
            base.OnDetaching();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.WidthChanged) return;

            Update();
        }
        
        private void Update()
        {
            var width = AssociatedObject.ActualWidth;
            
            if (width < MaxItemWidth)
            {
                AssociatedObject.Columns = 1;
                return;
            }
            
            var columns = (int) Math.Floor(width / MaxItemWidth);

            AssociatedObject.Columns = columns;

            AssociatedObject.InvalidateVisual();
        }
    }
}
