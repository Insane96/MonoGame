using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using NewEngine;

namespace MiningGame;

public class MiningGame() : EngineGame(1280, 720, true)
{
    private Vector2[] _scalingSpritePositions;
    private FontSystem _fontSystem;

    private Desktop _desktop;

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

        /*MyraEnvironment.Game = this;

        var grid = new Grid
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };

        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.Padding = new Thickness(300);

        var helloWorld = new Label
        {
            Id = "label",
            Text = "Hello, World!"
        };
        grid.Widgets.Add(helloWorld);

        // ComboBox
        var combo = new ComboView();
        Grid.SetColumn(combo, 1);
        Grid.SetRow(combo, 0);

        combo.Widgets.Add(new Label { Text = "Red", TextColor = Color.Red });
        combo.Widgets.Add(new Label { Text = "Green", TextColor = Color.Green });
        combo.Widgets.Add(new Label { Text = "Blue", TextColor = Color.Blue });

        grid.Widgets.Add(combo);

        // Button
        var button = new Button
        {
            Content = new Label
            {
                Text = "Show"
            }
        };
        Grid.SetColumn(button, 0);
        Grid.SetRow(button, 1);

        button.Click += (s, a) =>
        {
            var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
            messageBox.ShowModal(_desktop);
        };

        grid.Widgets.Add(button);

        // Spin button
        var spinButton = new SpinButton
        {
            Width = 100,
            Nullable = true
        };
        Grid.SetColumn(spinButton, 1);
        Grid.SetRow(spinButton, 1);

        grid.Widgets.Add(spinButton);

        // Add it to the desktop
        _desktop = new Desktop();
        _desktop.Root = grid;*/
    }

    double color = 0;

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        color += 10000f * Time.DeltaTime;
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
    
    protected override void DrawScaled(GameTime gameTime)
    {
        foreach (Vector2 position in _scalingSpritePositions)
        {
            SpriteBatch.Draw(Utils.OneByOneTexture, position, null, Utils.IntToColor((int)color),
                0f, Origins.Center, 100f, SpriteEffects.None, 0f);
        }

        //_desktop.Scale = new Vector2(Graphics.Scale);
        //_desktop.Render();
    }
}