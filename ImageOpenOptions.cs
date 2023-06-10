using System;

namespace GraDeMarCo
{
    [Serializable]
    public class ImageOpenOptions : BindingBase
    {
        public string ImageFilePath
        {
            get
            {
                return _imageFilePath;
            }
            set
            {
                _imageFilePath = value;
                OnPropertyChanged(GetName.Of(() => ImageFilePath));
            }
        }

        private string _imageFilePath;

        public ImageOpenOptions()
        {
            this.ImageFilePath = "";
        }
    }
}
