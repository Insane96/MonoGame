using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewEngine;

namespace MiningGame;

public class MiningGame : EngineGame
{
    private Vector2[] _scalingSpritePositions;
    
    public MiningGame() : base(1280, 720, false)
    {
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        
        _scalingSpritePositions = new Vector2[4];
        _scalingSpritePositions[0] = new Vector2(25, 25);
        _scalingSpritePositions[1] = new Vector2(25, Graphics.VirtualHeight - 25);
        _scalingSpritePositions[2] = new Vector2(Graphics.VirtualWidth - 25, 25);
        _scalingSpritePositions[3] = new Vector2(Graphics.VirtualWidth - 25, Graphics.VirtualHeight - 25);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        // Initialize the batch with the scaling matrix
        _spriteBatch.Begin(transformMatrix: Graphics.ScaleMatrix);
        // Draw a sprite at each corner
        foreach (Vector2 position in _scalingSpritePositions)
        {
            _spriteBatch.Draw(Utils.OneByOneTexture, position, null, Color.White,
                0f, Origins.Center, 100f, SpriteEffects.None, 0f);
        }
        _spriteBatch.End();
    }
}