using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrainDetector
{
    public class FormState
    {
        public enum ActionMode
        {
            None,
            ImageRangeSelect,
            CircleSelect,
            DotDraw
        }
    }
}
