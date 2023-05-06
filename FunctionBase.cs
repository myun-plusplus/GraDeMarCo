using System.ComponentModel;
using System.Drawing;

namespace GrainDetector
{
    public abstract class FunctionBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ImageDisplay imageDisplay;

        protected FunctionBase(ImageDisplay imageDisplay)
        {
            this.imageDisplay = imageDisplay;
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void DrawOnPaintEvent(Graphics graphics);
        public abstract void Draw(Graphics graphics);

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
