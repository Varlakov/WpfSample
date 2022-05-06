using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace WpfSample.Core.ViewModels
{
    public class PaginationViewModel : BindableBase
    {
        private readonly int _totalItemsCount;
        private readonly int _itemsPerPage;


        public event EventHandler<int> OnPageChanged;


        private DelegateCommand _previousCommand;
        public ICommand PreviousCommand => _previousCommand ??= new DelegateCommand(ShowPreviousPage, () => CurrentPageIndex != 0).ObservesProperty(() => CurrentPageIndex);


        private DelegateCommand _nextCommand;
        public ICommand NextCommand => _nextCommand ??= new DelegateCommand(ShowNextPage, () => CurrentPageIndex != TotalPages - 1).ObservesProperty(() => CurrentPageIndex);


        private DelegateCommand _firstCommand;
        public ICommand FirstCommand => _firstCommand ??= new DelegateCommand(ShowFirstPage, () => CurrentPageIndex != 0).ObservesProperty(() => CurrentPageIndex);


        private DelegateCommand _lastCommand;
        public ICommand LastCommand => _lastCommand ??= new DelegateCommand(ShowLastPage, () => CurrentPageIndex != TotalPages - 1).ObservesProperty(() => CurrentPageIndex);


        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set { SetProperty(ref _currentPageIndex, value); RaisePropertyChanged(nameof(CurrentPage)); }
        }


        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }


        public int CurrentPage => CurrentPageIndex + 1;


        public PaginationViewModel(int totalCount, int itemsPerPage = Constants.TotalItemsPerPage)
        {
            if (itemsPerPage == 0)
                throw new ArgumentOutOfRangeException(nameof(itemsPerPage));

            _totalItemsCount = totalCount;
            _itemsPerPage = itemsPerPage;

            CurrentPageIndex = 0;
            CalculateTotalPages();
        }


        public void ShowNextPage()
        {
            CurrentPageIndex++;
            OnPageChanged?.Invoke(this, CurrentPageIndex);
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            OnPageChanged?.Invoke(this, CurrentPageIndex);
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            OnPageChanged?.Invoke(this, CurrentPageIndex);
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            OnPageChanged?.Invoke(this, CurrentPageIndex);
        }


        private void CalculateTotalPages()
        {
            TotalPages = _totalItemsCount / _itemsPerPage;
            if (_totalItemsCount % _itemsPerPage != 0)
            {
                TotalPages += 1;
            }
        }
    }
}
