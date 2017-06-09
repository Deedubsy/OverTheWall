﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.TurretBase;
using OverTheWall.Projectile;

public class RepeaterCrossbow : OverTheWall.TurretBase.TurrentBase
{
    public Projectile projectile;

    private Vector2 launchPosition;
    private float Cooldown;
    private float AdditionalDamage;

    // Use this for initialization
    void Start()
    {

        launchPosition = new Vector2(transform.position.x + 2, transform.position.y);
        Cooldown = 1.0f;
        AdditionalDamage = 5.0f;

        InitializeTurret(OverTheWall.Enums.TurretType.RepeaterCrossbow,
            projectile,
            Cooldown,
            AdditionalDamage,
            OverTheWall.Enums.ProjectileType.Boulder,
            launchPosition);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
