using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewEngine;

namespace MiningGame;

public class MiningGame() : EngineGame(1280, 720, false)
{
    private Vector2[] _scalingSpritePositions;
    private FontSystem _fontSystem;

    protected override void LoadContent()
    {
        base.LoadContent();
        
        _scalingSpritePositions = new Vector2[4];
        _scalingSpritePositions[0] = new Vector2(25, 25);
        _scalingSpritePositions[1] = new Vector2(25, Graphics.VirtualHeight - 25);
        _scalingSpritePositions[2] = new Vector2(Graphics.VirtualWidth - 25, 25);
        _scalingSpritePositions[3] = new Vector2(Graphics.VirtualWidth - 25, Graphics.VirtualHeight - 25);
        
        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSans.ttf"));
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/Fonts/GoogleSans-Italic.ttf"));
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

    }

    protected override void DrawScaled(GameTime gameTime)
    {
        foreach (Vector2 position in _scalingSpritePositions)
        {
            SpriteBatch.Draw(Utils.OneByOneTexture, position, null, Color.White,
                0f, Origins.Center, 100f, SpriteEffects.None, 0f);
        }
    }
}