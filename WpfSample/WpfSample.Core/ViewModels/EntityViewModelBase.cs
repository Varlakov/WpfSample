using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.Core.ViewModels
{
    public abstract class EntityViewModelBase : BindableBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set => SetProperty(ref _createdOn, value);
        }


        private string _createdBy;
        public string CreatedBy
        {
            get => _createdBy;
            set => SetProperty(ref _createdBy, value);
        }


        private DateTime? _lastmodifiedOn;
        public DateTime? LastModifiedOn
        {
            get => _lastmodifiedOn;
            set => SetProperty(ref _lastmodifiedOn, value);
        }
    }
}
