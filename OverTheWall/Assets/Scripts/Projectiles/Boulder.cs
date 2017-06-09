using OverTheWall.Enums;
using OverTheWall.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Projectile
{
    private float damage = 50;
    private float speed = 5;
    private Rigidbody2D rigidBody2d;
    private ProjectileShotFrom shotBy;

    public void Initialize(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
    {
        this.shotBy = shotBy;
        base.InitializeProjectile(Type, sp, tp, speed, damage, shotBy, CurveType);
    }

    // Use this for initialization
    void Start ()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();

        InitializePrivateVariables(damage, speed);

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
