using WpfSample.Data.Model;
using WpfSample.Modules.Activities.ViewModels;

namespace WpfSample.Modules.Activities.Helpers
{
    public static class Converters
    {
        public static ActivityViewModel ToViewModel(this Activity entity) =>
            new ActivityViewModel
            {
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                Id = entity.Id,
                Name = entity.Name,
                LastModifiedOn = entity.LastModifiedOn
            };


        public static Activity ToModel(this ActivityViewModel viewModel) =>
            new Activity
            {
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn,
                Id = viewModel.Id,
                Name = viewModel.Name,
                LastModifiedOn = viewModel.LastModifiedOn
            };
    }
}
