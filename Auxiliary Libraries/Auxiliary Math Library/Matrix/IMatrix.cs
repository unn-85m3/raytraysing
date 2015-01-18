using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    interface IMatrix
    {
        float[] ToArray();
        float this[int row, int col] { get; set; }
    }
}
