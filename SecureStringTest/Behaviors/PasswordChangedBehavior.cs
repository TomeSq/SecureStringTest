﻿using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SecureStringTest.Behaviors
{
    class PasswordChangedBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
            base.OnAttached();
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.SecurePassword != null)
                AssociatedObject.DataContext = AssociatedObject.SecurePassword.Copy();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
            base.OnDetaching();
        }

        // Using a DependencyProperty as the backing store for TargetPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetPasswordProperty = DependencyProperty.Register("TargetPassword", typeof(SecureString), typeof(PasswordChangedBehavior), new PropertyMetadata(default(SecureString)));
    }
}
