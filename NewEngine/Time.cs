using Microsoft.Xna.Framework;

namespace NewEngine;

/// <summary>
/// Provides static access to time-related values for frame-independent game logic.
/// </summary>
public static class Time
{
    private static double _deltaTime;

    /// <summary>
    /// Gets the time in seconds between the current and previous frame, scaled by <see cref="TimeScale"/>.
    /// Use this for gameplay logic that should be affected by slow-motion or pause.
    /// </summary>
    public static double DeltaTime => _deltaTime * TimeScale;

    /// <summary>
    /// Gets the time in seconds between the current and previous frame, unaffected by <see cref="TimeScale"/>.
    /// Use this for UI animations or logic that should run at normal speed regardless of time scale.
    /// </summary>
    public static double UnscaledDeltaTime => _deltaTime;

    /// <summary>
    /// The scale at which time passes. 1.0 is normal speed, 0.5 is half speed, 0.0 is paused.
    /// </summary>
    public static double TimeScale = 1d;

    /// <summary>
    /// Updates the delta time from the current frame's GameTime. Called internally by the engine each frame.
    /// </summary>
    /// <param name="gameTime">The GameTime from the current frame.</param>
    public static void UpdateDeltaTime(GameTime gameTime)
    {
        _deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
    }
}