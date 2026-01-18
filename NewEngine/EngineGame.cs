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
        // TODO: Add your initialization logic here

        base.Initialize();
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

        if (Input.IsKeyPressed(Keys.F12))
            Time.TimeScale = Time.TimeScale >= 1f ? 1f : 3f;

        GameObjectManager.UpdateGameObjects();
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin(transformMatrix: Graphics.ScaleMatrix);
        GameObjectManager.DrawGameObjects(this.SpriteBatch);
        DrawScaled(gameTime);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    /// <summary>
    /// Renders game elements that should be scaled according to the virtual resolution.
    /// </summary>
    protected abstract void DrawScaled(GameTime gameTime);
}