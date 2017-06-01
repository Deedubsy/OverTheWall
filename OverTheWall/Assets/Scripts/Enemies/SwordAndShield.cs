using OverTheWall.EnemyController;
using OverTheWall.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAndShield : EnemyController
{
    float m_EnemyHealth = 150;
    float m_MovementSpeed = 2;
    float m_AttackSpeed = 2;
    float m_AttackRange = 1;
    float m_AttackDamage = 10;
    float m_AttackCooldown = 1;

    // Use this for initialization
    void Start()
    {
        Initialize(Enemy_Type.SwordAndShield, m_EnemyHealth, m_MovementSpeed, m_AttackSpeed, m_AttackRange, m_AttackDamage, m_AttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateRoutine();
    }
}
