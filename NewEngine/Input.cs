using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NewEngine;

public static class Input
{
    private static Game _game = null!;
    private static KeyboardState _oldKeyboardState;
    public static KeyboardState KeyboardState { get; private set; }
    private static GamePadState _oldGamePadState;
    public static GamePadState GamePadState { get; private set; }

    private static MouseState _oldMouseState;
    public static MouseState MouseState { get; private set; }

    public static void Init(Game game)
    {
        _game = game;
    }

    public static void Update()
    {
        _oldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();
        _oldGamePadState = GamePadState;
        GamePadState = GamePad.GetState(PlayerIndex.One);
        _oldMouseState = MouseState;
        MouseState = Mouse.GetState();
    }

    /// <summary>
    /// Returns true if the key is down
    /// </summary>
    public static bool IsKeyDown(Keys key)
    {
        return KeyboardState.IsKeyDown(key) && _game.IsActive;
    }

    /// <summary>
    /// Returns true if the key has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsKeyPressed(Keys key)
    {
        return JustPressed(KeyboardState.IsKeyDown(key), _oldKeyboardState.IsKeyDown(key));
    }

    /// <summary>
    /// Returns true if left click has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsLeftClickPressed()
    {
        return JustPressed(MouseState.LeftButton == ButtonState.Pressed, _oldMouseState.LeftButton == ButtonState.Pressed);
    }

    /// <summary>
    /// Returns true if right click has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsRightClickPressed()
    {
        return JustPressed(MouseState.RightButton == ButtonState.Pressed, _oldMouseState.RightButton == ButtonState.Pressed);
    }

    /// <summary>
    /// Returns true if middle click has been pressed and wasn't previously pressed
    /// </summary>
    public static bool IsMiddleClickPressed()
    {
        return JustPressed(MouseState.MiddleButton == ButtonState.Pressed, _oldMouseState.MiddleButton == ButtonState.Pressed);
    }

    public static bool JustPressed(bool isPressed, bool wasPressed)
    {
        return isPressed && !wasPressed && _game.IsActive;
    }

    public static bool IsMouseInBounds()
    {
        return MouseState.X >= 0 && MouseState.X <= Graphics.VirtualWidth && MouseState.Y >= 0 && MouseState.Y <= Graphics.VirtualHeight;
    }
}