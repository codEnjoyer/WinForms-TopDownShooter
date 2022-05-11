using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Entities;

namespace GameProject.Physics
{
    internal class Vector
    {
		public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public readonly double X;
        public readonly double Y;
        public double Length => Math.Sqrt(X * X + Y * Y);
        public float Angle => (float)Math.Atan2(Y, X) * 180 / (float)Math.PI;

        public float AngleToPlayer(Player player)
        {
            var playerLocation = player.Location;
            var x = X - (playerLocation.X + player.Size.Width) / 2f;
            var y = Y - (playerLocation.Y + player.Size.Height) / 2f;
            return new Vector(x, y).Angle;
        }
        public static Vector Zero = new Vector(0, 0);

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        protected bool Equals(Vector other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector a, double k)
        {
            return new Vector(a.X * k, a.Y * k);
        }

        public static Vector operator /(Vector a, double k)
        {
            return new Vector(a.X / k, a.Y / k);
        }

        public static Vector operator *(double k, Vector a)
        {
            return a * k;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public Vector Normalize()
        {
            return Length > 0 ? this * (1 / Length) : this;
        }

        public Vector Rotate(double angle)
        {
            return new Vector(X * Math.Cos(angle) - Y * Math.Sin(angle), X * Math.Sin(angle) + Y * Math.Cos(angle));
        }

        public Point ToPoint()
        {
            return new Point((int)(X + 0.5f), (int)(Y + 0.5f));
        }
    }
}
