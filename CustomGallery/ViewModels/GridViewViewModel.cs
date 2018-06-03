using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomGallery
{
    /// <summary>
    /// Grid view view model.
    /// </summary>
    public class GridViewViewModel : INotifyPropertyChanged
    {
        public   ObservableCollection<GalleryClass> GalleryCollection; 
        private int _maxColumns;
        private ObservableCollection<GalleryClass> _parentModels; 
        private float _tileHeight;
 
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyname">Propertyname.</param>
        private void RaisePropertyChanged([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                if (!string.IsNullOrEmpty(propertyname))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                }
            }
        }

        public GridViewViewModel()
        { 
            _parentModels=new ObservableCollection<GalleryClass>(); 
            ParentModels=new ObservableCollection<GalleryClass>(); 
            ItemTapCommand = new Command<GalleryClass>(OnParentTapped);
            MaxColumns = 2;
            TileHeight = 100;
        }

        public ICommand ItemTapCommand { get; private set; }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set
            {  
                _maxColumns = value; RaisePropertyChanged(); 
            }
        }

        public ObservableCollection<GalleryClass> ParentModels
        {
            get { return _parentModels; }
            set { _parentModels = value;
                RaisePropertyChanged();  
            }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; RaisePropertyChanged(); }
        }

        internal void LoadData()
        {
           // var galleryClass = _dbhelper.GetAllObjects<GalleryClass>();
            if (Constants.GalleryCollection != null)
            {
                ParentModels = Constants.GalleryCollection;
            }
        }
       
        private void OnParentTapped(GalleryClass item)
        {
            Application.Current.MainPage.DisplayAlert(Constants.AppTitle, "Selected " + item.Title,"Ok");
        }
    }
}