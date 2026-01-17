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
    /// Gets the actual viewport width in pixels.
    /// </summary>
    public static int ViewportWidth => DeviceManager.GraphicsDevice.Viewport.Width;

    /// <summary>
    /// Gets the actual viewport height in pixels.
    /// </summary>
    public static int ViewportHeight => DeviceManager.GraphicsDevice.Viewport.Height;

    /// <summary>
    /// The virtual (logical) resolution width used for game rendering.
    /// </summary>
    public static int VirtualWidth;

    /// <summary>
    /// The virtual (logical) resolution height used for game rendering.
    /// </summary>
    public static int VirtualHeight;

    /// <summary>
    /// Gets the aspect ratio of the virtual resolution.
    /// </summary>
    public static float VirtualAspectRatio => (float)VirtualWidth / VirtualHeight;

    /// <summary>
    /// Gets the horizontal scale factor from virtual to viewport resolution.
    /// </summary>
    public static float ScaleX => (float) ViewportWidth / VirtualWidth;

    /// <summary>
    /// Gets the vertical scale factor from virtual to viewport resolution.
    /// </summary>
    public static float ScaleY => (float) ViewportHeight / VirtualHeight;

    /// <summary>
    /// Gets a transformation matrix for scaling sprites from virtual to viewport resolution.
    /// </summary>
    public static Matrix ScaleMatrix => Matrix.CreateScale(ScaleX, ScaleY, 1f);

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
        int actualW = DeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
        int actualH = DeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight;

        float scale = Math.Min(
            (float)actualW / VirtualWidth,
            (float)actualH / VirtualHeight
        );

        int scaledW = (int)(VirtualWidth * scale);
        int scaledH = (int)(VirtualHeight * scale);

        Viewport view = new(
            (actualW - scaledW) / 2,
            (actualH - scaledH) / 2,
            scaledW,
            scaledH
        );

        DeviceManager.GraphicsDevice.Viewport = view;
        
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