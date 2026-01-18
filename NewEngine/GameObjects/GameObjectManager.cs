using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewEngine.GameObjects;

public static class GameObjectManager
{
    internal static Game Game = null!;
    
    private static List<GameObject> GameObjects { get; } = new();
    private static List<GameObject> GameObjectsToInstantiate { get; } = new();

    public static void Init(Game game)
    {
        Game = game;
    }
    
    public static void Instantiate(GameObject gameObject)
    {
        GameObjectsToInstantiate.Add(gameObject);
        gameObject.Initialize(Game.Content);
    }

    internal static void UpdateGameObjects()
    {
        foreach (GameObject gameObject in GetUpdatableGameObjects())
        {
            gameObject.Update();
        }
        RemoveMarkedForRemoval();
        AddInstantiated();
    }

    internal static void DrawGameObjects(SpriteBatch spriteBatch)
    {
        foreach (GameObject gameObject in GetDrawableGameObjects())
        {
            gameObject.Draw(spriteBatch);
        }
    }

    public static IEnumerable<GameObject> GetUpdatableGameObjects()
    {
        return GameObjects.Where(g => g.Enabled);
    }

    internal static void RemoveMarkedForRemoval()
    {
        GameObjects.RemoveAll(g => g.RemovalMark);
    }

    internal static void AddInstantiated()
    {
        if (GameObjectsToInstantiate.Count > 0)
        {
            GameObjects.AddRange(GameObjectsToInstantiate);
            GameObjectsToInstantiate.Clear();
        }
    }

    internal static IEnumerable<GameObject> GetDrawableGameObjects()
    {
        return GameObjects.Where(g => g.Visible).OrderBy(g => g.Layer);
    }
}