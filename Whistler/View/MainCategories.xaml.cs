using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Whistler.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainCategories : Page
    {
        public MainCategoriesViewModel ViewModel
        {
            get { return (MainCategoriesViewModel)this.DataContext; }
        }
        public MainCategories()
        {
            this.InitializeComponent();
            //this.ViewModel.LoadAllCategoriesData();
            this.Loaded += MainCategories_Loaded;
        }

        void MainCategories_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.ViewModel.Categories = e.Parameter as CategoryModel;
            }
           
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Category category = ((FrameworkElement)sender).DataContext as Category;
            this.ViewModel.SelectedCategory = category ; 
            this.NavigateToDetailPage(category);
        }

        private void NavigateToDetailPage(Category category)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(CategoryDetails));
        }
    }
}
