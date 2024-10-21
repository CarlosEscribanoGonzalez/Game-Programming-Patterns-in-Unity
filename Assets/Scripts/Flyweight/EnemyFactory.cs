using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private static Dictionary<string, FlyweightEnemy> flyweightEnemies = new();

    public static FlyweightEnemy GetFlyWeightEnemy(string type)
    {
        if (!flyweightEnemies.ContainsKey(type))
        {
            flyweightEnemies[type] = new FlyweightEnemy(type);
        }
        return flyweightEnemies[type];
    }
}
