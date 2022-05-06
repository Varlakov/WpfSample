using Prism.Mvvm;
using Prism.Navigation;

namespace WpfSample.Core.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
