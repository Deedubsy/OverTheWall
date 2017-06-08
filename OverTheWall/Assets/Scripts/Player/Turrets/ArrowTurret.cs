using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.Projectile;

public class ArrowTurret : OverTheWall.TurretBase.TurrentBase
{
    public Projectile projectile;

    // Use this for initialization
    void Start () {
        InitializeTurret(OverTheWall.Enums.TurretType.ArcherGroup,
            projectile,
            2.0f,
            5,
            OverTheWall.Enums.ProjectileType.Arrow,
            new Vector2(transform.position.x + 2, transform.position.y));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
