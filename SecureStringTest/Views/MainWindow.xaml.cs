using System.Diagnostics;
using System.Security;
using System.Windows;
using Prism.Events;
using SecureStringTest.ViewModels;

namespace SecureStringTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Instance.GetEvent<PubSubEvent<SecureString>>().Subscribe(x =>
            {
                this.TestPassword.Password = SecureStringExtension.SecureStringToText(x);
                Debug.WriteLine(SecureStringExtension.SecureStringToText(x));
            });
        }
    }
}
