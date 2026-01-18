using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewEngine;

/// <summary>
/// Provides static access to graphics device management and screen configuration.
/// Supports resolution-independent rendering with letterboxing/pillarboxing.
/// </summary>
public static class Graphics
{
    /// <summary>
    /// The virtual (logical) resolution width used for game rendering.
    /// </summary>
    public static int VirtualWidth;

    /// <summary>
    /// The virtual (logical) resolution height used for game rendering.
    /// </summary>
    public static int VirtualHeight;
    
    /// <summary>
    /// Gets the actual viewport width in pixels.
    /// </summary>
    public static int ViewportWidth => DeviceManager.GraphicsDevice.Viewport.Width;

    /// <summary>
    /// Gets the actual viewport height in pixels.
    /// </summary>
    public static int ViewportHeight => DeviceManager.GraphicsDevice.Viewport.Height;

    public static int ActualWidth => DeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
    public static int ActualHeight => DeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight;
    
    /// <summary>
    /// Gets the aspect ratio of the virtual resolution.
    /// </summary>
    public static float VirtualAspectRatio => (float)VirtualWidth / VirtualHeight;

    /// <summary>
    /// Gets a transformation matrix for scaling sprites from virtual to viewport resolution.
    /// </summary>
    public static Matrix ScaleMatrix => Matrix.CreateScale(Scale, Scale, 1f);
    
    public static float Scale =>
        Math.Min(
            (float)ActualWidth / VirtualWidth,
            (float)ActualHeight / VirtualHeight
        );

    /// <summary>
    /// The underlying MonoGame graphics device manager.
    /// </summary>
    public static GraphicsDeviceManager DeviceManager = null!;

    /// <summary>
    /// Initializes the graphics system with the specified virtual resolution.
    /// </summary>
    /// <param name="game">The game instance to attach the graphics device manager to.</param>
    /// <param name="width">The virtual width for game rendering.</param>
    /// <param name="height">The virtual height for game rendering.</param>
    public static void Init(Game game, int width, int height)
    {
        VirtualWidth = width;
        VirtualHeight = height;
        
        DeviceManager = new GraphicsDeviceManager(game)
        {
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height,
            SynchronizeWithVerticalRetrace = true
        };
        DeviceManager.HardwareModeSwitch = false;
        DeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        DeviceManager.ApplyChanges();
        
        DeviceManager.PreparingDeviceSettings += (_, args) =>
        {
            DeviceManager.PreferMultiSampling = true;
            var rasterizerState = new RasterizerState
            {
                MultiSampleAntiAlias = true,
            };

            game.GraphicsDevice.RasterizerState = rasterizerState;
            args.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 2;
        };
    }

    /// <summary>
    /// Sets the window resolution. Has no effect when in fullscreen mode.
    /// </summary>
    /// <param name="width">The desired window width in pixels.</param>
    /// <param name="height">The desired window height in pixels.</param>
    public static void SetResolution(int width, int height)
    {
        // Don't change resolution if fullscreen
        if (DeviceManager.IsFullScreen)
            return;
        DeviceManager.PreferredBackBufferWidth = width;
        DeviceManager.PreferredBackBufferHeight = height;
        DeviceManager.ApplyChanges();
        UpdateViewport();
    }

    /// <summary>
    /// Updates the viewport to maintain aspect ratio with letterboxing/pillarboxing.
    /// Call this after any resolution or fullscreen change.
    /// </summary>
    public static void UpdateViewport()
    {
        int scaledW = (int)(VirtualWidth * Scale);
        int scaledH = (int)(VirtualHeight * Scale);

        Viewport view = new(
            (ActualWidth - scaledW) / 2,
            (ActualHeight - scaledH) / 2,
            scaledW,
            scaledH
        );

        DeviceManager.GraphicsDevice.Viewport = view;
    }

    /// <summary>
    /// Converts screen coordinates to normalized virtual coordinates (0~1).
    /// </summary>
    public static Vector2 ScreenToVirtualNormalized(Vector2 screenPosition)
    {
        var viewport = DeviceManager.GraphicsDevice.Viewport;
        return new Vector2(
            (screenPosition.X - viewport.X) / viewport.Width,
            (screenPosition.Y - viewport.Y) / viewport.Height
        );
    }

    /// <summary>
    /// Converts screen coordinates to virtual coordinates (0~VirtualWidth, 0~VirtualHeight).
    /// </summary>
    public static Vector2 ScreenToVirtual(Vector2 screenPosition)
    {
        var viewport = DeviceManager.GraphicsDevice.Viewport;
        return new Vector2(
            (screenPosition.X - viewport.X) / Scale,
            (screenPosition.Y - viewport.Y) / Scale
        );
    }

    /// <summary>
    /// Toggles between fullscreen and windowed mode.
    /// In fullscreen, uses the display's native resolution. In windowed mode, restores the virtual resolution.
    /// </summary>
    public static void ToggleFullscreen()
    {
        if (!DeviceManager.IsFullScreen)
        {
            DeviceManager.PreferredBackBufferWidth  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            DeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        else
        {
            DeviceManager.PreferredBackBufferWidth  = VirtualWidth;
            DeviceManager.PreferredBackBufferHeight = VirtualHeight;
        }
        DeviceManager.IsFullScreen = !DeviceManager.IsFullScreen;
        DeviceManager.ApplyChanges();
        UpdateViewport();
    }
    
    
}