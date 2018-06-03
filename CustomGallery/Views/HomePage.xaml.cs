using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomGallery
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
         GridViewViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();
            Title = "Custom Gallery";
            _viewModel = new GridViewViewModel();
            this.BindingContext = _viewModel;
            NavigationPage.SetHasBackButton(this,false);
            Constants.GalleryCollection = new ObservableCollection<GalleryClass>();
            SearchEntry.TextChanged += (sender, e) => SearchProjects(SearchEntry.Text);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
            if(Constants.GalleryCollection!=null && Constants.GalleryCollection.Count>0)
             customGrid.ItemsSource = Constants.GalleryCollection;
        }

        void UploadClicked(object sender, System.EventArgs e)
        {
             Application.Current.MainPage.Navigation.PushAsync(new UploadImagePage());
        }

        public void SearchProjects(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                _viewModel.ParentModels = Constants.GalleryCollection;
            }
            else
            {
                var collection= Constants.GalleryCollection;
                //var _collection = Constants.GalleryCollection.Where(x => (x.Title.ToLower().Contains(filter.ToLower())));
                var _collection = collection.Where(x => (x.Title.ToLower().Contains(filter.ToLower())));
                _viewModel.ParentModels = new ObservableCollection<GalleryClass>(_collection);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
