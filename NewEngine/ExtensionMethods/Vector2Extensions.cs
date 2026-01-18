using System;
using Microsoft.Xna.Framework;

namespace NewEngine.ExtensionMethods;

/// <summary>
/// Extension methods for <see cref="Vector2"/>.
/// </summary>
public static class Vector2Extensions
{
    /// <summary>
    /// Returns a formatted string representation of the vector.
    /// </summary>
    /// <param name="vector">The vector to format.</param>
    /// <param name="format">The numeric format string (e.g., "F2" for 2 decimal places).</param>
    /// <returns>A string in the format "{X:value Y:value}".</returns>
    public static string ToString(this Vector2 vector, string format)
    {
        return $"{{X:{vector.X.ToString(format)} Y:{vector.Y.ToString(format)}}}";
    }

    /// <summary>
    /// Adds the specified values to the vector components.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="x">The value to add to the X component.</param>
    /// <param name="y">The value to add to the Y component.</param>
    /// <returns>A new vector with the summed values.</returns>
    public static Vector2 Sum(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X + x, vector.Y + y);
    }

    /// <summary>
    /// Adds the specified value to both vector components.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="f">The value to add to both components.</param>
    /// <returns>A new vector with the summed values.</returns>
    public static Vector2 Sum(this Vector2 vector, float f)
    {
        return new Vector2(vector.X + f, vector.Y + f);
    }

    /// <summary>
    /// Subtracts the specified values from the vector components.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="x">The value to subtract from the X component.</param>
    /// <param name="y">The value to subtract from the Y component.</param>
    /// <returns>A new vector with the subtracted values.</returns>
    public static Vector2 Subtract(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X - x, vector.Y - y);
    }

    /// <summary>
    /// Subtracts the specified value from both vector components.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="f">The value to subtract from both components.</param>
    /// <returns>A new vector with the subtracted values.</returns>
    public static Vector2 Subtract(this Vector2 vector, float f)
    {
        return new Vector2(vector.X - f, vector.Y - f);
    }

    /// <summary>
    /// Subtracts another vector from this vector.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="v">The vector to subtract.</param>
    /// <returns>A new vector with the subtracted values.</returns>
    public static Vector2 Subtract(this Vector2 vector, Vector2 v)
    {
        return new Vector2(vector.X - v.X, vector.Y - v.Y);
    }

    /// <summary>
    /// Multiplies the vector components by the specified values.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="x">The multiplier for the X component.</param>
    /// <param name="y">The multiplier for the Y component.</param>
    /// <returns>A new vector with the multiplied values.</returns>
    public static Vector2 Multiply(this Vector2 vector, float x, float y)
    {
        return new Vector2(vector.X * x, vector.Y * y);
    }

    /// <summary>
    /// Multiplies both vector components by the specified value.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="f">The multiplier for both components.</param>
    /// <returns>A new vector with the multiplied values.</returns>
    public static Vector2 Multiply(this Vector2 vector, float f)
    {
        return new Vector2(vector.X * f, vector.Y * f);
    }

    /// <summary>
    /// Extends point A away from point B by the specified distance.
    /// </summary>
    /// <param name="a">The point to extend.</param>
    /// <param name="b">The point to extend away from.</param>
    /// <param name="extension">The distance to extend.</param>
    /// <returns>A new point extended from A in the direction opposite to B. Returns A if A equals B.</returns>
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