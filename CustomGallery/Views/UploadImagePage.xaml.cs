using System;
using Xamarin.Forms;
using Plugin.Media;

namespace CustomGallery
{
    public partial class UploadImagePage : ContentPage
    {
        string _filePath;
        public UploadImagePage()
        {
            InitializeComponent();
            Title = Constants.AppUploadTitle;
            DateTimeEntry.Text = DateTime.Now.ToString();
            UploadButton.Clicked += async delegate
             {
                 if (string.IsNullOrEmpty(TittleEntry.Text))
                 {
                     await DisplayAlert(Constants.AppTitle, Constants.TitleRequired, Constants.Ok);
                     return;
                 }
                 var gallery = new GalleryClass
                 {
                     Title = TittleEntry.Text,
                     Path = _filePath,
                     Created = DateTime.Now
                 };
                 Constants.GalleryCollection.Add(gallery); 
                 await App.NavigationRef.PopAsync();
             }; 
            BrowseButton.Clicked += delegate
            {
                PickImage();
            }; 
        } 
        async void PickImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(Constants.AppTitle, Constants.PermissionDenied, Constants.Ok);
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            }); 
            if (file == null)
                return;
            image.Source = file.Path;
            _filePath = file.Path;
        }
    }
}
