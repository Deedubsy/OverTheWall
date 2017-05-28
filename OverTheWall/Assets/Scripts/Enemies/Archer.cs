using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.EnemyController;

public class Archer : EnemyController
{
    float EnemyHealth = 700;
    float MovementSpeed = 2;
    float AttackSpeed = 1.5f;
    float AttackRange = 10;
    float AttackDamage = 5;
    float AttackCooldown = 1;

    // Use this for initialization
    void Start()
    {
        Initialize(Enemy_Type.SwordAndShield, EnemyHealth, MovementSpeed, AttackSpeed, AttackRange, AttackDamage, AttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateRoutine();
    }
}
