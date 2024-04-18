using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokeView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isImage1Visible = true;
        public bool IsImage1Visible
        {
            get { return _isImage1Visible; }
            set
            {
                _isImage1Visible = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public BitmapImage Image1 { get; set; }
        public BitmapImage Image2 { get; set; }

        public BitmapImage ImageSource => IsImage1Visible ? Image1 : Image2;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
