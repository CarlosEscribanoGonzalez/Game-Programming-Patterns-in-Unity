using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory
{
    private static Dictionary<string, FlyweightProjectile> flyweightProjectiles = new();

    public static FlyweightProjectile GetFlyweightProjectile(string type)
    {
        if (!flyweightProjectiles.ContainsKey(type))
        {
            flyweightProjectiles[type] = new FlyweightProjectile(type);
        }
        return flyweightProjectiles[type];
    }
}
