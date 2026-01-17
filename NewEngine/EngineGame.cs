using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewEngine;

/// <summary>
/// Extend this instead of <see cref="Game"/>
/// </summary>
public abstract class EngineGame : Game
{
    protected SpriteBatch _spriteBatch;

    protected EngineGame(int winWidth, int winHeight, bool isMouseVisible = true)
    {
        Graphics.Init(this, winWidth, winHeight);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = isMouseVisible;

        Input.Init(this);
        Utils.Init();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        this._spriteBatch = new SpriteBatch(this.GraphicsDevice);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        Time.UpdateDeltaTime(gameTime);
        if (Input.IsKeyPressed(Keys.Escape) || Input.GamePadState.IsButtonDown(Buttons.Back))
            this.Exit();
        if (Input.IsKeyPressed(Keys.F11))
        {
            Graphics.ToggleFullscreen();
        }

        if (Input.IsKeyPressed(Keys.F12))
            Time.TimeScale = Time.TimeScale >= 1f ? 1f : 3f;

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);

        base.Draw(gameTime);
    }
}