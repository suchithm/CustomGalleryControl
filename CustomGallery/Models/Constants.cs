using System;
using System.Collections.ObjectModel;

namespace CustomGallery
{
    public class Constants
    {
        public Constants()
        {
        }
        public static ObservableCollection<GalleryClass> GalleryCollection; 
        public const string AppTitle = "GalleryControl";
        public const string TitleRequired = "Please enter image title text";
        public const string MobileNoRequired = "Please enter valid mobile number";
        public const string LoginFailed = "Login Failed! Invalid credential";
        public const string Ok = "Ok";
        public const string AppHomeTitle = "Gallery Home";
        public const string AppUploadTitle = "Upload Image"; 
        public const string AppDbName = "Gallery.db";
        public const string PermissionDenied = "Permission not granted to photos"; 

    }
}
