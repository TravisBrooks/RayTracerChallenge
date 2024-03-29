﻿using System;

namespace RayTracerChallenge
{
    /// <summary>
    /// Like the built in Drawing.Color but instead of 0-255 for color range it is 0.0-1.0
    /// </summary>
    public readonly struct FColor : IEquatable<FColor>
    {
        public FColor(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static FColor FromRgb(int r, int g, int b)
        {
            var c = new FColor(r/255.0, g/255.0, b/255.0);
            return c;
        }

        public double Red { get; }
        public double Green { get; }
        public double Blue { get; }

        private int _ToRgbComponent(in double colorPart)
        {
            var ppmPart = (int)Math.Round(255 * colorPart);
            if (ppmPart < 0)
            {
                ppmPart = 0;
            }
            if (ppmPart > 255)
            {
                ppmPart = 255;
            }
            return ppmPart;
        }

        public (int red, int green, int blue) ToPpmColor()
        {
            var r = _ToRgbComponent(Red);
            var g = _ToRgbComponent(Green);
            var b = _ToRgbComponent(Blue);
            var color = (red: r, green: g, blue: b);
            return color;
        }

        public FColor ComplementaryColor()
        {
            var max = Math.Max(Math.Max(Red, Blue), Green);
            var min = Math.Min(Math.Min(Red, Blue), Green);
            var r2 = max + min - Red;
            var g2 = max + min - Green;
            var b2 = max + min - Blue;
            return new FColor(r2, g2, b2);
        }

        public override string ToString()
        {
            var s = $"(Red:{Red}, Green:{Green}, Blue:{Blue})";
            return s;
        }

        bool IEquatable<FColor>.Equals(FColor other)
        {
            return Equals(other);
        }

        public bool Equals(in FColor other)
        {
            return Red.AboutEqual(other.Red) && Green.AboutEqual(other.Green) && Blue.AboutEqual(other.Blue);
        }

        public override bool Equals(object obj)
        {
            return obj is FColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Red.GetHashCode();
                hashCode = (hashCode * 397) ^ Green.GetHashCode();
                hashCode = (hashCode * 397) ^ Blue.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(in FColor left, in FColor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in FColor left, in FColor right)
        {
            return !left.Equals(right);
        }

        public static FColor operator +(in FColor lhs, in FColor rhs)
        {
            var r = lhs.Red + rhs.Red;
            var g = lhs.Green + rhs.Green;
            var b = lhs.Blue + rhs.Blue;
            return new FColor(r, g, b);
        }

        public static FColor operator -(in FColor lhs, in FColor rhs)
        {
            var r = lhs.Red - rhs.Red;
            var g = lhs.Green - rhs.Green;
            var b = lhs.Blue - rhs.Blue;
            return new FColor(r, g, b);
        }

        public static FColor operator *(in FColor c, in double scalar)
        {
            var r = c.Red * scalar;
            var g = c.Green * scalar;
            var b = c.Blue * scalar;
            return new FColor(r, g, b);
        }

        public static FColor operator *(in FColor lhs, in FColor rhs)
        {
            var r = lhs.Red * rhs.Red;
            var g = lhs.Green * rhs.Green;
            var b = lhs.Blue * rhs.Blue;
            return new FColor(r, g, b);
        }
    }
}
