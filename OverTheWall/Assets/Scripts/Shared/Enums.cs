using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverTheWall.Enums
{
    public enum Enemy_Type
    {
        Sword = 0,
        SwordAndShield = 1,
        Archer = 2
    }

    public enum Ranged_Enemy_Type
    {
        Archer = 0
    }

    public enum Movement
    {
        Left,
        Right,
        Stationary
    }

    public enum ProjectileType
    {
        Arrow = 0
    }

    public enum ProjectilCurveType
    {
        Straight = 0,
        SlightCurve = 1,
        HeavyCurve = 2,
    }

    public enum ProjectileShotFrom
    {
        Player = 0,
        Enemy = 1,
    }
}

