using System;
using WpfSample.Modules.Activities.ViewModels;
using Xunit;

namespace WpfSample.Modules.Tests.Activities.ViewModels
{
    public class EquipmentViewModelTests
    {
        [Fact]
        public void ActivityViewModel_Name_FailedValidation()
        {
            var vm = new ActivityViewModel
            {
                Name = "    "
            };

            Assert.NotNull(vm[nameof(ActivityViewModel.Name)]);
        }

        [Fact]
        public void ActivityViewModel_Name_SuccessValidation()
        {
            var vm = new ActivityViewModel
            {
                Name = "new name"
            };

            Assert.Null(vm[nameof(ActivityViewModel.Name)]);
        }



        [Fact]
        public void ActivityViewModel_CreatedBy_FailedValidation()
        {
            var vm = new ActivityViewModel
            {
                CreatedBy = "    "
            };

            Assert.NotNull(vm[nameof(ActivityViewModel.CreatedBy)]);
        }

        [Fact]
        public void ActivityViewModel_CreatedBy_FailedLengthValidation()
        {
            var vm = new ActivityViewModel
            {
                CreatedBy = new string('Y', 100)
            };

            Assert.NotNull(vm[nameof(ActivityViewModel.CreatedBy)]);
        }

        [Fact]
        public void ActivityViewModel_CreatedBy_SuccessValidation()
        {
            var vm = new ActivityViewModel
            {
                CreatedBy = "user name"
            };

            Assert.Null(vm[nameof(ActivityViewModel.CreatedBy)]);
        }



        [Fact]
        public void ActivityViewModel_Error_FailedNameValidation()
        {
            var vm = new ActivityViewModel
            {
                Name = "  ",
                CreatedBy = "  "
            };

            Assert.NotNull(vm.Error);
        }

        [Fact]
        public void ActivityViewModel_Error_FailedCreatedByValidation()
        {
            var vm = new ActivityViewModel
            {
                Name = "name",
                CreatedBy = "  "
            };

            Assert.NotNull(vm.Error);
        }

        [Fact]
        public void ActivityViewModel_Error_SuccessValidation()
        {
            var vm = new ActivityViewModel
            {
                Name = "activity name",
                CreatedBy = "user name"
            };

            Assert.Null(vm.Error);
        }


        [Fact]
        public void ActivityViewModel_NonValidatableProperty()
        {
            var vm = new ActivityViewModel
            {
                CreatedOn = DateTime.Now
            };

            Assert.Null(vm[nameof(ActivityViewModel.CreatedOn)]);
        }
    }
}
