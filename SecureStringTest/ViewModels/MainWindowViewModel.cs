using System;
using System.Diagnostics;
using System.Security;
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

        public string _plainPassword = string.Empty;

        public string PlainPassword
        {
            get
            {
                return _plainPassword;
            }
            set
            {
                _plainPassword = value;
            }
        }


        public MainWindowViewModel()
        {
            OkCommand.Subscribe(_ =>
            {
                Debug.WriteLine("OKボタンがクリックされました。");
                if(this.Password.Length > 0)
                {
                    Debug.Write(SecureStringExtension.SecureStringToText(this.Password));
                }
            });


            ResetCommand.Subscribe(_ =>
            {
                this.Password.Clear();
            });

    }
    }
}
