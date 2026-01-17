using System;

namespace NewEngine;

/// <summary>
/// Provides math utility methods
/// </summary>
public static class Mth
{
    /// <summary>
    /// Returns a random integer within the specified range.
    /// </summary>
    /// <param name="random">The random instance to use.</param>
    /// <param name="min">The inclusive lower bound.</param>
    /// <param name="max">The inclusive upper bound.</param>
    /// <returns>A random integer where min &lt;= value &lt;= max.</returns>
    public static int NextInt(Random random, int min, int max)
    {
        return random.Next(min, max + 1);
    }

    /// <summary>
    /// Returns a random double within the specified range.
    /// </summary>
    /// <param name="random">The random instance to use.</param>
    /// <param name="min">The inclusive lower bound.</param>
    /// <param name="max">The exclusive upper bound.</param>
    /// <returns>A random double where min &lt;= value &lt; max, or min if max &lt;= min.</returns>
    public static double NextDouble(Random random, double min, double max)
    {
        if (max < min || min == max) return min;
        return random.NextDouble() * (max - min) + min;
    }

    /// <summary>
    /// Returns a random float within the specified range.
    /// </summary>
    /// <param name="random">The random instance to use.</param>
    /// <param name="min">The inclusive lower bound.</param>
    /// <param name="max">The exclusive upper bound.</param>
    /// <returns>A random float where min &lt;= value &lt; max, or min if max &lt;= min.</returns>
    public static float NextFloat(Random random, float min, float max)
    {
        if (max < min || min == max) return min;
        return random.NextSingle() * (max - min) + min;
    }
}