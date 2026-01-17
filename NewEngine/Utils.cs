using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewEngine.ExtensionMethods;

namespace NewEngine;

public static class Utils
{
    /// <summary>
    /// Used to draw solid color rectangles
    /// </summary>
    public static readonly Texture2D OneByOneTexture = new(Graphics.DeviceManager.GraphicsDevice, 1, 1);

    internal static void Init()
    {
        OneByOneTexture.SetData([Color.White]);
    }

    /// <summary>
    /// Determines whether a line, defined by its start and end points, intersects with a rectangle.
    /// </summary>
    /// <param name="lineStart">The starting point of the line.</param>
    /// <param name="lineEnd">The ending point of the line.</param>
    /// <param name="rectangle">The rectangle to test for intersection with the line.</param>
    /// <returns>True if the line intersects with the rectangle; otherwise, false.</returns>
    public static bool Intersects(Vector2 lineStart, Vector2 lineEnd, Rectangle rectangle)
    {
        return Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(rectangle.Width, 0f))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2(), rectangle.PosToVector2().Sum(0f, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(0, rectangle.Height), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height))
               || Intersects(lineStart, lineEnd, rectangle.PosToVector2().Sum(rectangle.Width, 0), rectangle.PosToVector2().Sum(rectangle.Width, rectangle.Height));
    }

    /// <summary>
    /// Determines whether two line segments, defined by their start and end points, intersect.
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

    public static Vector2 RotateDirectionClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X);
    }

    public static Vector2 RotateDirectionCounterClockwise(Vector2 dir)
    {
        return new Vector2(dir.Y, dir.X * -1f);
    }

    public static Vector2 OppositeDirection(Vector2 dir)
    {
        return new Vector2(dir.Y * -1f, dir.X * -1f);
    }
}