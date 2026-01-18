using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NewEngine.GameObjects;

namespace NewEngine;

/// <summary>
/// Abstract base class for games using the NewEngine framework.
/// Extend this class instead of <see cref="Game"/> to get automatic setup of
/// Graphics, Input, Time, Utils, and GameObjectManager systems.
/// </summary>
public abstract class EngineGame : Game
{
    /// <summary>
    /// The SpriteBatch used for rendering.
    /// </summary>
    protected SpriteBatch SpriteBatch;

    /// <summary>
    /// Font system for text rendering.
    /// </summary>
    private FontSystem _fontSystem;

    /// <summary>
    /// Creates a new EngineGame with the specified window dimensions.
    /// Initializes all engine systems (Graphics, Input, Utils, GameObjectManager).
    /// </summary>
    /// <param name="winWidth">The virtual width of the game window.</param>
    /// <param name="winHeight">The virtual height of the game window.</param>
    /// <param name="isMouseVisible">Whether the mouse cursor is visible. Defaults to true.</param>
    protected EngineGame(int winWidth, int winHeight, bool isMouseVisible = true)
    {
        Graphics.Init(this, winWidth, winHeight);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = isMouseVisible;

        Input.Init(this);
        Utils.Init();
        GameObjectManager.Init(this);
    }

    /// <summary>
    /// Initializes the game and loads fonts.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSansCode.ttf"));
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSansCode-Italic.ttf"));
    }

    /// <summary>
    /// Loads game content and creates the SpriteBatch.
    /// </summary>
    protected override void LoadContent()
    {
        this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);

        base.LoadContent();
    }

    /// <summary>
    /// Updates the game state each frame. Handles input, time, and GameObject updates.
    /// Press Escape or Back to exit, F11 for fullscreen, F12 to toggle time scale.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
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

    /// <summary>
    /// Renders the game each frame. Clears the screen, draws all GameObjects, then calls DrawScaled.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
    /// Override this method to render game elements that should be scaled according to the virtual resolution.
    /// Called after GameObjects are drawn, within the scaled SpriteBatch context.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected abstract void DrawScaled(GameTime gameTime);
}
