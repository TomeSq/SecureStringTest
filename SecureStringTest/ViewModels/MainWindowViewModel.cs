using System;
using System.Diagnostics;
using System.Security;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;

namespace SecureStringTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();

        public ReactiveCommand ResetCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("Prism Application");

        public SecureString _password = new SecureString();

        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


        public MainWindowViewModel()
        {
            foreach (char c in "ABCDEFG")
            {
                this.Password.AppendChar(c);
            }

            this.OkCommand.Subscribe(_ =>
            {
                Debug.WriteLine("OKボタンがクリックされました。");
                if(this.Password.Length > 0)
                {
                    Debug.WriteLine(SecureStringExtension.SecureStringToText(this.Password));
                }
            });


            this.ResetCommand.Subscribe(_ =>
            {
                Messenger.Instance.GetEvent<PubSubEvent<SecureString>>().Publish(Password);
                this.Password.Clear();
            });
        }
    }
}
