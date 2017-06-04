using OverTheWall.EnemyController;
using OverTheWall.Enums;
using OverTheWall.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float damage = 5;
    private Rigidbody2D rigidBody2d;
    private ProjectileShotFrom shotBy;

    public void Initialize(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
    {
        this.shotBy = shotBy;
        base.InitializeProjectile(Type, sp, tp, speed, damage, shotBy, CurveType);
    }
    
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();

        if (shotBy == ProjectileShotFrom.Player)
            SetPlayerProjectileVelocity();
    }

    void SetPlayerProjectileVelocity()
    {
        base.SetPlayerProjectileForce(rigidBody2d);
    }

    private void Update()
    {
        this.UpdatePosition();
    }
}
