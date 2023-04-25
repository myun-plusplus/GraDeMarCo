using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrainDetector
{
    internal class MainController : IDisposable
    {
        private MainForm mainForm;
        private ImageForm imageForm;

        public MainController(MainForm mf)
        {
            this.mainForm = mf;
            this.imageForm = new ImageForm();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.imageForm != null && !this.imageForm.IsDisposed)
                {
                    this.imageForm.Dispose();
                }
            }

            this.imageForm = null;
            this.mainForm = null;
        }
    }
}
