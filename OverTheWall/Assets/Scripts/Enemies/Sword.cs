using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EnemyController
{
    float EnemyHealth = 100;
    float MovementSpeed = 2;
    float AttackSpeed = 2;
    float AttackRange = 1;
    float AttackDamage = 10;
    float AttackCooldown = 1;

    // Use this for initialization
    void Start()
    {
        Initialize(Enemy_Type.Sword, EnemyHealth, MovementSpeed, AttackSpeed, AttackRange, AttackDamage, AttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateRoutine();
    }
}
