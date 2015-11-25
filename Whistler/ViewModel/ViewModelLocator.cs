using System.Diagnostics.CodeAnalysis;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

using Whistler.Model;

namespace Whistler.ViewModel
{
    /// <summary>
    /// This class contains static references to the most relevant view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// The key used by the NavigationService to go to the second page.
        /// </summary>
        public const string MainPage = "MainPages";

        public MainCategoriesViewModel MainCategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainCategoriesViewModel>();
            }
        }


        public MainViewModel MainPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public LookingForViewModel LookingForViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LookingForViewModel>();
            }
        }

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


           SimpleIoc.Default.Register<INavigationService>(() => CreateNavigationService());
           SimpleIoc.Default.Register<MainCategoriesViewModel>();
           SimpleIoc.Default.Register<MainViewModel>();
           SimpleIoc.Default.Register<SettingsViewModel>();
           SimpleIoc.Default.Register<LookingForViewModel>();
            
           
        }
        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("MainCategories", typeof(View.MainCategories));
            return navigationService;
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}