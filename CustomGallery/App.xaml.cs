using Xamarin.Forms;

namespace CustomGallery
{
    public partial class App : Application
    {
        public static INavigation NavigationRef;
        public App()
        {
            InitializeComponent();

           // MainPage = new NavigationPage(new CustomGalleryPage());
            MainPage = new NavigationPage(new HomePage());
            NavigationRef = MainPage.Navigation;
 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
