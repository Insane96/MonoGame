using System;
using Microsoft.Xna.Framework;

namespace NewEngine.ExtensionMethods;

public static class Vector2Extensions
{
    public static string ToString(this Vector2 vector, string format)
    {
        return $"{{X:{vector.X.ToString(format)} Y:{vector.Y.ToString(format)}}}";
    }

    public static Vector2 Sum(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X + x, vector.Y + y);
    }

    public static Vector2 Sum(this Vector2 vector, float f)
    {
        return new Vector2(vector.X + f, vector.Y + f);
    }

    public static Vector2 Subtract(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X - x, vector.Y - y);
    }

    public static Vector2 Subtract(this Vector2 vector, float f)
    {
        return new Vector2(vector.X - f, vector.Y - f);
    }

    public static Vector2 Subtract(this Vector2 vector, Vector2 v)
    {
        return new Vector2(vector.X - v.X, vector.Y - v.Y);
    }

    public static Vector2 Multiply(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X * x, vector.Y * y);
    }

    public static Vector2 Multiply(this Vector2 vector, float f)
    {
        return new Vector2(vector.X * f, vector.Y * f);
    }

    public static Vector2 ExtendFrom(this Vector2 a, Vector2 b, float extension)
    {
        double len = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        return len == 0 
            ? a 
            : new Vector2((float)(a.X + (a.X - b.X) / len * extension), (float)(a.Y + (a.Y - b.Y) / len * extension));
    }

    /// <summary>
    /// Calculates the normalized direction vector pointing from one vector to another.
    /// </summary>
    /// <param name="from">The starting vector.</param>
    /// <param name="to">The target vector.</param>
    /// <returns>A normalized Vector2 representing the direction from the starting vector to the target vector.</returns>
    public static Vector2 Direction(this Vector2 from, Vector2 to)
    {
        return Vector2.Normalize(to - from);
    }
}