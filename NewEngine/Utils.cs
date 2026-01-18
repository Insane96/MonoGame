using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewEngine.ExtensionMethods;

namespace NewEngine;

/// <summary>
/// Provides utility methods for geometry calculations and rendering helpers.
/// </summary>
public static class Utils
{
    /// <summary>
    /// A 1x1 white texture used for drawing solid color rectangles and primitives.
    /// </summary>
    public static readonly Texture2D OneByOneTexture = new(Graphics.DeviceManager.GraphicsDevice, 1, 1);

    /// <summary>
    /// Initializes the Utils class. Called internally by the engine.
    /// </summary>
    internal static void Init()
    {
        OneByOneTexture.SetData([Color.White]);
    }

    /// <summary>
    /// Determines whether a line segment intersects with a rectangle.
    /// </summary>
    /// <param name="lineStart">The starting point of the line.</param>
    /// <param name="lineEnd">The ending point of the line.</param>
    /// <param name="rectangle">The rectangle to test for intersection.</param>
    /// <returns>True if the line intersects with any edge of the rectangle; otherwise, false.</returns>
    public static bool Intersects(Vector2 lineStart, Vector2 lineEnd, Rectangle rectangle)
    {
        return Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(rectangle.Width, 0f))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(0f, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(0, rectangle.Height), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(rectangle.Width, 0), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height));
    }

    /// <summary>
    /// Determines whether two line segments intersect.
    /// </summary>
    /// <param name="lineStartA">The starting point of the first line segment.</param>
    /// <param name="lineEndA">The ending point of the first line segment.</param>
    /// <param name="lineStartB">The starting point of the second line segment.</param>
    /// <param name="lineEndB">The ending point of the second line segment.</param>
    /// <returns>True if the two line segments intersect; otherwise, false.</returns>
    public static bool Intersects(Vector2 lineStartA, Vector2 lineEndA, Vector2 lineStartB, Vector2 lineEndB)
    {
        Vector2 b = lineEndA - lineStartA;
        Vector2 d = lineEndB - lineStartB;
        float bDotDPerp = b.X * d.Y - b.Y * d.X;

        // if b dot d == 0, it means the lines are parallel so have infinite intersection points
        if (bDotDPerp == 0)
            return false;

        Vector2 c = lineStartB - lineStartA;
        float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
        if (t is < 0 or > 1)
            return false;

        float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
        return u is >= 0 and <= 1;
    }

    /// <summary>
    /// Rotates a direction vector 90 degrees clockwise.
    /// </summary>
    /// <param name="dir">The direction vector to rotate.</param>
    /// <returns>The rotated direction vector.</returns>
    public static Vector2 RotateDirectionClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X);
    }

    /// <summary>
    /// Rotates a direction vector 90 degrees counter-clockwise.
    /// </summary>
    /// <param name="dir">The direction vector to rotate.</param>
    /// <returns>The rotated direction vector.</returns>
    public static Vector2 RotateDirectionCounterClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y, dir.X * -1f);
    }

    /// <summary>
    /// Returns the opposite (180 degree rotated) direction vector.
    /// </summary>
    /// <param name="dir">The direction vector to invert.</param>
    /// <returns>The opposite direction vector.</returns>
    public static Vector2 OppositeDirection(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X * -1f);
    }
}
