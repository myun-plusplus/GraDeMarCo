using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace GrainDetector
{
    public class GrainDetect : FunctionBase
    {
        public Bitmap OriginalImage;
        public Bitmap CircleImage;
        public Color CircleColor;

        private Bitmap binarizedImage;

        public int MinWhitePixel;

        private List<Point> dotLocationInCircle, dotLocationOnCircle;

        #region Options

        public bool detectsGrainInCircle, detectsGrainOnCircle;
        public Color dotColorInCircle, dotColorOnCircle;
        public int dotSizeInCircle, dotSizeOnCircle;

        #endregion

        public GrainDetect(ImageDisplay imageDisplay)
            : base(imageDisplay)
        {

        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        public override void DrawOnPaintEvent(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public void DrawOnImage(Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            {

            }
        }
    }
}
