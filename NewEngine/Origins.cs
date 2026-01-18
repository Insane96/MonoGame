using Microsoft.Xna.Framework;
using NewEngine.ExtensionMethods;

namespace NewEngine;

/// <summary>
/// Provides predefined origin points as normalized coordinates (0~1).
/// Use these with <see cref="GameObjects.GameObject.Origin"/> for sprite alignment.
/// </summary>
public static class Origins
{
    /// <summary>Top-left corner (0, 0).</summary>
    public static readonly Vector2 TopLeft = Vector2.Zero;

    /// <summary>Top-center (0.5, 0).</summary>
    public static readonly Vector2 TopCenter = new(0.5f, 0f);

    /// <summary>Top-right corner (1, 0).</summary>
    public static readonly Vector2 TopRight = new(1f, 0f);

    /// <summary>Center-left (0, 0.5).</summary>
    public static readonly Vector2 CenterLeft = new(0f, 0.5f);

    /// <summary>Center (0.5, 0.5).</summary>
    public static readonly Vector2 Center = new(0.5f, 0.5f);

    /// <summary>Center-right (1, 0.5).</summary>
    public static readonly Vector2 CenterRight = new(1f, 0.5f);

    /// <summary>Bottom-left corner (0, 1).</summary>
    public static readonly Vector2 BottomLeft = new(0f, 1f);

    /// <summary>Bottom-center (0.5, 1).</summary>
    public static readonly Vector2 BottomCenter = new(0.5f, 1f);

    /// <summary>Bottom-right corner (1, 1).</summary>
    public static readonly Vector2 BottomRight = new(1f, 1f);

    /// <summary>
    /// Converts a normalized origin (0~1) to a screen position in virtual coordinates.
    /// </summary>
    /// <param name="origin">The normalized origin point.</param>
    /// <returns>The corresponding position in virtual screen coordinates.</returns>
    public static Vector2 GetScreenPosition(Vector2 origin)
    {
        return origin.Multiply(Graphics.ViewportWidth, Graphics.VirtualHeight);
    }
}
