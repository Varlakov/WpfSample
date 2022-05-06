using System;
using WpfSample.Core.ViewModels;
using Xunit;

namespace WpfSample.Core.Tests.ViewModels
{
    public class PaginationViewModelTests
    {
        [Fact]
        public void PaginationViewModel_Initialization_NullParameters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaginationViewModel(1, 0));
        }

        [Fact]
        public void PaginationViewModel_Initialization()
        {
            var vm = new PaginationViewModel(17, 5);

            Assert.Equal(0, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(1, vm.CurrentPage);
        }


        [Fact]
        public void PaginationViewModel_ShowNextPage()
        {
            var vm = new PaginationViewModel(17, 5);

            AssetRaisesEvent(1, h => vm.OnPageChanged += h, h => vm.OnPageChanged -= h, () => vm.ShowNextPage());

            Assert.Equal(1, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(2, vm.CurrentPage);
        }

        [Fact]
        public void PaginationViewModel_ShowPreviousPage()
        {
            var vm = new PaginationViewModel(17, 5);

            AssetRaisesEvent(-1, h => vm.OnPageChanged += h, h => vm.OnPageChanged -= h, () => vm.ShowPreviousPage());

            Assert.Equal(-1, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(0, vm.CurrentPage);
        }

        [Fact]
        public void PaginationViewModel_ShowFirstPage()
        {
            var vm = new PaginationViewModel(17, 5);

            AssetRaisesEvent(0, h => vm.OnPageChanged += h, h => vm.OnPageChanged -= h, () => vm.ShowFirstPage());

            Assert.Equal(0, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(1, vm.CurrentPage);
        }

        [Fact]
        public void PaginationViewModel_ShowLastPage()
        {
            var vm = new PaginationViewModel(17, 5);

            AssetRaisesEvent(3, h => vm.OnPageChanged += h, h => vm.OnPageChanged -= h, () => vm.ShowLastPage());

            Assert.Equal(3, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(4, vm.CurrentPage);
        }


        [Fact]
        public void PaginationViewModel_TotalPages_INotifyPropertyChangedCalled()
        {
            var vm = new PaginationViewModel(17, 5);
            Assert.PropertyChanged(vm, nameof(vm.TotalPages), () => vm.TotalPages = 10);
        }

        [Fact]
        public void PaginationViewModel_CurrentPageIndex_INotifyPropertyChangedCalled()
        {
            var vm = new PaginationViewModel(17, 5);
            Assert.PropertyChanged(vm, nameof(vm.CurrentPageIndex), () => vm.CurrentPageIndex = 3);
            Assert.PropertyChanged(vm, nameof(vm.CurrentPage), () => vm.CurrentPageIndex = 2);
        }


        [Fact]
        public void PaginationViewModel_LastCommand_Execute()
        {
            var vm = new PaginationViewModel(17, 5);

            Assert.NotNull(vm.LastCommand);

            Assert.True(vm.LastCommand.CanExecute(null));

            vm.LastCommand.Execute(null);

            Assert.Equal(3, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(4, vm.CurrentPage);

            Assert.False(vm.LastCommand.CanExecute(null));
        }

        [Fact]
        public void PaginationViewModel_FirstCommand_Execute()
        {
            var vm = new PaginationViewModel(17, 5);
            vm.CurrentPageIndex = 3;

            Assert.NotNull(vm.FirstCommand);

            Assert.True(vm.FirstCommand.CanExecute(null));

            vm.FirstCommand.Execute(null);

            Assert.Equal(0, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(1, vm.CurrentPage);

            Assert.False(vm.FirstCommand.CanExecute(null));
        }

        [Fact]
        public void PaginationViewModel_NextCommand_Execute()
        {
            var vm = new PaginationViewModel(17, 5);

            Assert.NotNull(vm.NextCommand);

            Assert.True(vm.NextCommand.CanExecute(null));

            vm.NextCommand.Execute(null);

            Assert.Equal(1, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(2, vm.CurrentPage);

            Assert.True(vm.NextCommand.CanExecute(null));
        }

        [Fact]
        public void PaginationViewModel_PreviousCommand_Execute()
        {
            var vm = new PaginationViewModel(17, 5);
            vm.CurrentPageIndex = 1;

            Assert.NotNull(vm.PreviousCommand);

            Assert.True(vm.PreviousCommand.CanExecute(null));

            vm.PreviousCommand.Execute(null);

            Assert.Equal(0, vm.CurrentPageIndex);
            Assert.Equal(4, vm.TotalPages);
            Assert.Equal(1, vm.CurrentPage);

            Assert.False(vm.PreviousCommand.CanExecute(null));
        }


        private static void AssetRaisesEvent<T>(T expectedValue, Action<EventHandler<T>> attach, Action<EventHandler<T>> detach, Action testCode)
        {
            T? actualValue = default;
            EventHandler<T> obj = delegate (object? sender, T args)
            {
                actualValue = args;
            };
            attach(obj);
            testCode();
            detach(obj);

            Assert.Equal(expectedValue, actualValue);
        }
    }
}