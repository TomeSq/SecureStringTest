using System;
using System.Diagnostics;
using Prism.Mvvm;
using Reactive.Bindings;

namespace SecureStringTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("Prism Application");

        public MainWindowViewModel()
        {
            OkCommand.Subscribe(_ =>
            {
                Debug.WriteLine("OKボタンがクリックされました。");
            });
        }
    }
}
