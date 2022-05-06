using Prism.Mvvm;

namespace WpfSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "WpfSample Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
