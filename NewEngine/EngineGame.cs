using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NewEngine.GameObjects;

namespace NewEngine;

/// <summary>
/// Extend this instead of <see cref="Game"/>
/// </summary>
public abstract class EngineGame : Game
{
    protected SpriteBatch SpriteBatch;
    private FontSystem _fontSystem;

    protected EngineGame(int winWidth, int winHeight, bool isMouseVisible = true)
    {
        Graphics.Init(this, winWidth, winHeight);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = isMouseVisible;

        Input.Init(this);
        Utils.Init();
        GameObjectManager.Init(this);
    }

    protected override void Initialize()
    {
        base.Initialize();
        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSansCode.ttf"));
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSansCode-Italic.ttf"));
    }

    protected override void LoadContent()
    {
        this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        Time.UpdateDeltaTime(gameTime);
        if (Input.IsKeyPressed(Keys.Escape) || Input.GamePadState.IsButtonDown(Buttons.Back))
            this.Exit();
        if (Input.IsKeyPressed(Keys.F11))
            Graphics.ToggleFullscreen();
        //if (Input.IsKeyPressed(Keys.F10))
        //    Graphics.SetResolution(1200, 900);

        if (Input.IsKeyPressed(Keys.F12))
            Time.TimeScale = Time.TimeScale >= 1f ? 1f : 3f;

        GameObjectManager.UpdateGameObjects();
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin(transformMatrix: Graphics.ScaleMatrix);
        GameObjectManager.DrawGameObjects(this.SpriteBatch);
        DrawScaled(gameTime);
        
        //SpriteFontBase font30 = _fontSystem.GetFont(30);
        //Vector2 mousePosition = Input.MouseState.Position.ToVector2();
        //Vector2 virtualMousePosition = Graphics.ScreenToVirtual(mousePosition);
        //SpriteBatch.DrawString(font30, $"X: {mousePosition.X}, Y: {mousePosition.Y}", new Vector2(80, 80), Color.White);
        //SpriteBatch.DrawString(font30, $"X: {virtualMousePosition.X}, Y: {virtualMousePosition.Y}", new Vector2(80, 100), Color.White);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// Renders game elements that should be scaled according to the virtual resolution.
    /// </summary>
    protected abstract void DrawScaled(GameTime gameTime);
}