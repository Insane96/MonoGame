using Microsoft.Xna.Framework;

namespace NewEngine;

public static class Time
{
    private static double _deltaTime;

    /// <summary>
    /// Returns the time passed between frames
    /// </summary>
    public static double DeltaTime
    {
        get { return _deltaTime * TimeScale; }
    }

    /// <summary>
    /// Returns the time passed between frames not affected by time scale
    /// </summary>
    public static double UnscaledDeltaTime
    {
        get { return _deltaTime; }
    }

    public static double TimeScale = 1d;

    public static void UpdateDeltaTime(GameTime gameTime)
    {
        _deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
    }
}