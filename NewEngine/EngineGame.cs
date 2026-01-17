using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewEngine;

/// <summary>
/// Extend this instead of <see cref="Game"/>
/// </summary>
public class EngineGame : Game
{
    private SpriteBatch _spriteBatch;
    private Vector2[] _scalingSpritePositions;
    private Texture2D _squareTexture;
    private Vector2 _spriteOrigin;
    private Matrix _spriteScaleMatrix;

    public EngineGame(int winWidth, int winHeight, bool isMouseVisible = true)
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
        _squareTexture = CreateTexture(GraphicsDevice, 50, 50);
        _spriteOrigin = new Vector2(_squareTexture.Width / 2f, _squareTexture.Height / 2f);

        _scalingSpritePositions = new Vector2[4];
        _scalingSpritePositions[0] = new Vector2(25, 25);
        _scalingSpritePositions[1] = new Vector2(25, Graphics.VirtualHeight - 25);
        _scalingSpritePositions[2] = new Vector2(Graphics.VirtualWidth - 25, 25);
        _scalingSpritePositions[3] = new Vector2(Graphics.VirtualWidth - 25, Graphics.VirtualHeight - 25);

        base.LoadContent();
    }

    public static Texture2D CreateTexture(GraphicsDevice device, int width, int height)
    {
        // Initialize a texture
        Texture2D texture = new(device, width, height);

        // The array holds the color for each pixel in the texture
        Color[] data = new Color[width * height];
        for (int pixel = 0; pixel < data.Length; pixel++)
            data[pixel] = Color.White;

        // Set the color
        texture.SetData(data);
        return texture;
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
        // Initialize the batch with the scaling matrix
        _spriteBatch.Begin(transformMatrix: Graphics.ScaleMatrix);
        // Draw a sprite at each corner
        for (int i = 0; i < _scalingSpritePositions.Length; i++)
        {
            _spriteBatch.Draw(_squareTexture, _scalingSpritePositions[i], null, Color.White,
                0f, _spriteOrigin, 1f, SpriteEffects.None, 0f);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}