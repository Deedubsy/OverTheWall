using OverTheWall.EnemyController;
using OverTheWall.Enums;
using OverTheWall.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float damage = 5;

    public override void Initialize(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
    {
        base.Initialize(Type, sp, tp, speed, damage, shotBy, CurveType);
    }

    //public override void Initialize(Vector2 sp, Vector2 tp, float speed, ProjectileShotFrom shotBy)
    //{
    //    base.Initialize(ProjectileType.Arrow, sp, tp, 30, damage, shotBy, ProjectilCurveType.Straight);
    //}

    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        this.UpdatePosition();
    }
}
