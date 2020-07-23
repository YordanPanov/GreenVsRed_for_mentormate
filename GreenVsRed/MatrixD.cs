using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;

namespace GreenVsRed
{
    class MatrixD
    {
        public static int Height { get; set; }

        public static int Width { get; set; }

        public static string InputString { get; set; }

        public static int[,] MatrixZero { get; set; }

        public static int[,] MatrixNextGen { get; set; }

        public static int X1 { get; set; }

        public static int Y1 { get; set; }

        public static int IterN { get; set; }

        public static int Green { get; set; }
    }
}
