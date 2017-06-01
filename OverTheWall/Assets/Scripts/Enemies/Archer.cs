using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.EnemyController;
using OverTheWall.Enums;

public class Archer : EnemyController
{
    float m_EnemyHealth = 70;
    float m_MovementSpeed = 2;
    float m_AttackSpeed = 1.5f;
    float m_AttackRange = 10;
    float m_AttackDamage = 5;
    float m_AttackCooldown = 1;
    
    // Use this for initialization
    void Start()
    {
        Initialize(Enemy_Type.Archer, m_EnemyHealth, m_MovementSpeed, m_AttackSpeed, m_AttackRange, m_AttackDamage, m_AttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateRoutine();
    }
}
