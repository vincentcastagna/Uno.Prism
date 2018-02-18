using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Prism.Behaviors
{
    public sealed class AutowirePartialViewBehavior : BehaviorBase<View>
    {
        protected override void OnAttachedTo(View bindable)
        {
            if(!AutowirePartialView())
            {
                bindable.PropertyChanged += OnViewPropertyChanged;
            }
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(View.Parent) && AssociatedObject.Parent != null && AutowirePartialView())
            {
                // Once we set the View we should make sure we don't set it again...
                AssociatedObject.PropertyChanged -= OnViewPropertyChanged;
            }
        }

        private bool AutowirePartialView()
        {
            var page = Common.PageUtilities.GetPageFromElement(AssociatedObject);
            if(page != null)
            {
                ViewModelLocator.SetAutowireViewModel(AssociatedObject, true);
                // TODO: Add the AssociatedObject to some sort of AttachedProperty on the Page


                return true;
            }

            return false;
        }
    }
}
