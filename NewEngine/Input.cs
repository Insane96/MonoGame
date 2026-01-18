using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NewEngine;

/// <summary>
/// Provides static access to keyboard, mouse, and gamepad input state.
/// Tracks current and previous frame states to detect just-pressed inputs.
/// </summary>
public static class Input
{
    /// <summary>
    /// Reference to the main Game instance for checking IsActive.
    /// </summary>
    private static Game _game = null!;

    /// <summary>
    /// The keyboard state from the previous frame.
    /// </summary>
    private static KeyboardState _oldKeyboardState;

    /// <summary>
    /// Gets the current keyboard state.
    /// </summary>
    public static KeyboardState KeyboardState { get; private set; }

    /// <summary>
    /// The gamepad state from the previous frame.
    /// </summary>
    private static GamePadState _oldGamePadState;

    /// <summary>
    /// Gets the current gamepad state for player one.
    /// </summary>
    public static GamePadState GamePadState { get; private set; }

    /// <summary>
    /// The mouse state from the previous frame.
    /// </summary>
    private static MouseState _oldMouseState;

    /// <summary>
    /// Gets the current mouse state.
    /// </summary>
    public static MouseState MouseState { get; private set; }

    /// <summary>
    /// Initializes the Input system with a reference to the game.
    /// </summary>
    /// <param name="game">The main Game instance.</param>
    public static void Init(Game game)
    {
        _game = game;
    }

    /// <summary>
    /// Updates all input states. Called internally by the engine each frame.
    /// </summary>
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
    /// Returns true if the specified key is currently held down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is down and the game is active; otherwise, false.</returns>
    public static bool IsKeyDown(Keys key)
    {
        return KeyboardState.IsKeyDown(key) && _game.IsActive;
    }

    /// <summary>
    /// Returns true if the specified key was just pressed this frame (not held from previous frame).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was just pressed; otherwise, false.</returns>
    public static bool IsKeyPressed(Keys key)
    {
        return JustPressed(KeyboardState.IsKeyDown(key), _oldKeyboardState.IsKeyDown(key));
    }

    /// <summary>
    /// Returns true if the left mouse button was just pressed this frame.
    /// </summary>
    /// <returns>True if left click was just pressed; otherwise, false.</returns>
    public static bool IsLeftClickPressed()
    {
        return JustPressed(MouseState.LeftButton == ButtonState.Pressed, _oldMouseState.LeftButton == ButtonState.Pressed);
    }

    /// <summary>
    /// Returns true if the right mouse button was just pressed this frame.
    /// </summary>
    /// <returns>True if right click was just pressed; otherwise, false.</returns>
    public static bool IsRightClickPressed()
    {
        return JustPressed(MouseState.RightButton == ButtonState.Pressed, _oldMouseState.RightButton == ButtonState.Pressed);
    }

    /// <summary>
    /// Returns true if the middle mouse button was just pressed this frame.
    /// </summary>
    /// <returns>True if middle click was just pressed; otherwise, false.</returns>
    public static bool IsMiddleClickPressed()
    {
        return JustPressed(MouseState.MiddleButton == ButtonState.Pressed, _oldMouseState.MiddleButton == ButtonState.Pressed);
    }

    /// <summary>
    /// Helper method to detect a just-pressed state (pressed now but not in previous frame).
    /// </summary>
    /// <param name="isPressed">Whether the input is currently pressed.</param>
    /// <param name="wasPressed">Whether the input was pressed in the previous frame.</param>
    /// <returns>True if just pressed and the game is active; otherwise, false.</returns>
    public static bool JustPressed(bool isPressed, bool wasPressed)
    {
        return isPressed && !wasPressed && _game.IsActive;
    }

    /// <summary>
    /// Returns true if the mouse is within the virtual screen bounds.
    /// Accounts for viewport offset from letterboxing/pillarboxing.
    /// </summary>
    /// <returns>True if the mouse is within bounds; otherwise, false.</returns>
    public static bool IsMouseInBounds()
    {
        var pos = Graphics.ScreenToVirtual(new Vector2(MouseState.X, MouseState.Y));
        return pos.X >= 0 && pos.X <= Graphics.VirtualWidth && pos.Y >= 0 && pos.Y <= Graphics.VirtualHeight;
    }
}
