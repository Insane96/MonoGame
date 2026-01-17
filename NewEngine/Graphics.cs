using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewEngine;

public static class Graphics
{
    public static int ViewportWidth => DeviceManager.GraphicsDevice.Viewport.Width;

    public static int ViewportHeight => DeviceManager.GraphicsDevice.Viewport.Height;

    public static int VirtualWidth, VirtualHeight;
    
    public static float VirtualAspectRatio => (float)VirtualWidth / VirtualHeight;

    public static float ScaleX => (float) ViewportWidth / VirtualWidth;
    public static float ScaleY => (float) ViewportHeight / VirtualHeight;
    public static Matrix ScaleMatrix => Matrix.CreateScale(ScaleX, ScaleY, 1f);
    public static float Scale => Math.Min(ScaleX, ScaleY);

    public static int ScaledWidth => (int)(VirtualWidth * Scale);
    public static int ScaledHeight => (int)(VirtualHeight * Scale);
    
    public static int OffsetX => (ViewportWidth - ScaledWidth) / 2;
    public static int OffsetY => (ViewportHeight - ScaledHeight) / 2;

    public static GraphicsDeviceManager DeviceManager = null!;

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

    public static void SetResolution(int width, int height)
    {
        //Don't change resolution if fullscreen
        if (DeviceManager.IsFullScreen)
            return;
        DeviceManager.PreferredBackBufferWidth = width;
        DeviceManager.PreferredBackBufferHeight = height;
        DeviceManager.ApplyChanges();
        UpdateViewport();
    }

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
    /// Toggles fullscreen
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