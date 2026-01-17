using Microsoft.Xna.Framework;

namespace NewEngine.ExtensionMethods;

public static class RectangleExtensions
{
    /// <summary>
    /// Converts the position of a <see cref="Rectangle"/> to a <see cref="Vector2"/>
    /// representing the X and Y coordinates of the rectangle's top-left corner.
    /// </summary>
    /// <param name="rectangle">The rectangle whose position is to be converted.</param>
    /// <returns>A <see cref="Vector2"/> containing the X and Y coordinates of the rectangle's top-left corner.</returns>
    public static Vector2 PosToVector2(this Rectangle rectangle)
    {
        return new Vector2(rectangle.X, rectangle.Y);
    }
}