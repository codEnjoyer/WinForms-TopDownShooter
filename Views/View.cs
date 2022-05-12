using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Domain;
using GameProject.Entities;

namespace GameProject
{
    internal class View
    {
        internal static void UpdateTextures(Graphics graphics)
        {
            Game.Player.Move();
            Game.Player.GetMouseRotation();
        }

        public static Bitmap RotateImageMatrix(Bitmap bitmap, float angle)
        {
            using (var g = Graphics.FromImage(bitmap))
            using (var matrix = new Matrix())
            {
                //rotate at image mid point
                matrix.RotateAt(angle, new PointF(bitmap.Width / 2, bitmap.Height / 2));
                g.Transform = matrix;
                //draw passed in image onto graphics object
                g.DrawImage(bitmap, new PointF(0, 0));
            }
            return bitmap;
        }
    }
}
