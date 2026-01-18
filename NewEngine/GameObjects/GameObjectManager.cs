using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewEngine.GameObjects;

/// <summary>
/// Manages the lifecycle of all GameObjects in the game.
/// Handles instantiation, updating, drawing, and removal of GameObjects.
/// </summary>
public static class GameObjectManager
{
    /// <summary>
    /// Reference to the main Game instance.
    /// </summary>
    internal static Game Game = null!;

    /// <summary>
    /// Gets the list of all active GameObjects in the game.
    /// </summary>
    public static List<GameObject> GameObjects { get; } = new();

    /// <summary>
    /// Queue of GameObjects waiting to be added at the end of the current frame.
    /// </summary>
    private static List<GameObject> GameObjectsToInstantiate { get; } = new();

    /// <summary>
    /// Initializes the GameObjectManager with a reference to the game.
    /// </summary>
    /// <param name="game">The main Game instance.</param>
    public static void Init(Game game)
    {
        Game = game;
    }

    /// <summary>
    /// Instantiates a GameObject, adding it to the game at the end of the current frame.
    /// </summary>
    /// <param name="gameObject">The GameObject to instantiate.</param>
    public static void Instantiate(GameObject gameObject)
    {
        GameObjectsToInstantiate.Add(gameObject);
        gameObject.Initialize(Game.Content);
    }

    /// <summary>
    /// Updates all enabled GameObjects, then removes marked objects and adds newly instantiated ones.
    /// Called internally by the engine each frame.
    /// </summary>
    internal static void UpdateGameObjects()
    {
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            gameObject.Update();
        }
        RemoveMarkedForRemoval();
        AddInstantiated();
    }

    /// <summary>
    /// Draws all visible GameObjects ordered by their Layer property.
    /// Called internally by the engine each frame.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch used for rendering.</param>
    internal static void DrawGameObjects(SpriteBatch spriteBatch)
    {
        foreach (GameObject gameObject in GetDrawableGameObjects())
        {
            gameObject.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// Gets all GameObjects that are currently enabled.
    /// </summary>
    /// <returns>An enumerable of enabled GameObjects.</returns>
    public static IEnumerable<GameObject> GetUpdatableGameObjects()
    {
        return GameObjects.Where(g => g.Enabled);
    }

    /// <summary>
    /// Removes all GameObjects that have been marked for removal.
    /// </summary>
    internal static void RemoveMarkedForRemoval()
    {
        GameObjects.RemoveAll(g => g.RemovalMark);
    }

    /// <summary>
    /// Adds all queued GameObjects to the active list.
    /// </summary>
    internal static void AddInstantiated()
    {
        if (GameObjectsToInstantiate.Count > 0)
        {
            GameObjects.AddRange(GameObjectsToInstantiate);
            GameObjectsToInstantiate.Clear();
        }
    }

    /// <summary>
    /// Gets all GameObjects that are visible, ordered by Layer (lower layers drawn first).
    /// </summary>
    /// <returns>An enumerable of visible GameObjects sorted by Layer.</returns>
    internal static IEnumerable<GameObject> GetDrawableGameObjects()
    {
        return GameObjects.Where(g => g.Visible).OrderBy(g => g.Layer);
    }
}
