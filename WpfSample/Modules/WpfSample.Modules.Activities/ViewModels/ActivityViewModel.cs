using System.ComponentModel;
using WpfSample.Core.ViewModels;

namespace WpfSample.Modules.Activities.ViewModels
{
    public class ActivityViewModel : EntityViewModelBase, IDataErrorInfo
    {
        public string Error
        {
            get
            {
                var error = NameValidation();
                if (error != null)
                    return error;

                error = CreatedByValidation();
                if (error != null)
                    return error;

                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Name):
                        result = this.NameValidation();
                        break;

                    case nameof(CreatedBy):
                        result = this.CreatedByValidation();
                        break;

                    default:
                        break;
                }

                return result;
            }
        }



        private string NameValidation()
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(Name))
                result = "Please enter activity name";

            return result;
        }

        private string CreatedByValidation()
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(CreatedBy))
                result = "Please enter a Creator name";
            else if (CreatedBy.Length > 20)
                result = "The Creator name is too long";

            return result;
        }
    }
}
