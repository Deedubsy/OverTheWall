using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.Enums;
using OverTheWall.Projectile;

namespace OverTheWall.EnemyController
{
    public abstract class EnemyController : MonoBehaviour
    {
        public RectTransform castleRect;
        private CastleController castle;
        private ProjectileManager projectiles;

        public GameObject projectile;

        private Enemy_Type EnemyType;
        private float EnemyHealth;
        private float MovementSpeed;
        private float AttackSpeed;
        private float AttackRange;
        private float AttackDamage;
        private float AttackCooldown;
        private float CurrentHealth = 1000;
        private string Name;
        private Animator animator;

        private bool canAttack = true;
        private RectTransform enemyRect;

        private bool IsDead = false;
        private float leftSideEnemy = 0;
        private Vector2 leftSideEnemyVector2;

        //TODO: Add animator
        public void Initialize(Enemy_Type EnemyType, float EnemyHealth, float MovementSpeed, float AttackSpeed, float AttackRange, float AttackDamage, float AttackCooldown)
        {
            this.EnemyType = EnemyType;
            this.EnemyHealth = EnemyHealth;
            this.MovementSpeed = MovementSpeed;
            this.AttackSpeed = AttackSpeed;
            this.AttackRange = AttackRange;
            this.AttackDamage = AttackDamage;
            this.AttackCooldown = AttackCooldown;
            this.CurrentHealth = EnemyHealth;

            projectiles = new ProjectileManager();

            animator = new Animator();

            castle = FindObjectOfType<CastleController>();
            enemyRect = GetComponent<RectTransform>();

            leftSideEnemy = transform.position.x - (enemyRect.sizeDelta.x / 2);
            leftSideEnemyVector2 = new Vector2(leftSideEnemy, transform.position.y);
        }

        void Start()
        {
            castle = FindObjectOfType<CastleController>();
        }

        void Update()
        {

        }

        public virtual void UpdateRoutine()
        {
            if (WithinAttackRange())
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        public bool IsEnemyDead()
        {
            return IsDead;
        }

        private void Move()
        {
            //Movement
            transform.position = new Vector3(transform.position.x + (MovementSpeed * -1) * Time.deltaTime, transform.position.y);
        }

        private bool WithinAttackRange()
        {
            bool withinAttackRange = false;

            float rightSideCastle = castle.transform.position.x + (castleRect.sizeDelta.x / 2);
            float leftSideCastle = castle.transform.position.x - (castleRect.sizeDelta.x / 2);
            float leftSideEnemy = transform.position.x - (enemyRect.sizeDelta.x / 2);
            float distance = rightSideCastle - leftSideEnemy;

            if (Mathf.Abs(distance) <= AttackRange ||
                leftSideEnemy >= leftSideCastle && leftSideEnemy <= rightSideCastle)
                withinAttackRange = true;

            return withinAttackRange;
        }

        public virtual void Attack()
        {
            if (canAttack)
            {
                canAttack = false;

                //Play Attack animation

                switch (EnemyType)
                {
                    case Enemy_Type.Sword:
                        MeleeAttack();
                        break;
                    case Enemy_Type.SwordAndShield:
                        MeleeAttack();
                        break;
                    case Enemy_Type.Archer:
                        RangedAttack();
                        break;
                    default:
                        break;
                }

                StartCoroutine(AttackRoutine());

                Debug.Log("ATTACK! EnemyType" + EnemyType + ". Damage: " + AttackDamage);
            }
        }

        private void RangedAttack()
        {
            Vector2 castlePos = castle.GetPosition();

            ProjectileManager.AddProjectile(ProjectileType.Arrow, transform.position, new Vector2(castlePos.x, castlePos.y), 30, 5, ProjectileShotFrom.Enemy, ProjectilCurveType.SlightCurve);
        }

        private void MeleeAttack()
        {
            castle.OnHit(AttackDamage);
        }

        private IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(AttackCooldown);

            canAttack = true;
        }

        public void OnHit(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                IsDead = true;
            }


            //UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            float healthPercentage = CurrentHealth / EnemyHealth;
            //float newSizeDelta = healthPercentage * healthBar.sizeDelta.x;

            //healthBar.sizeDelta = new Vector2(newSizeDelta, healthBar.sizeDelta.y);
        }

        public void Die()
        {
            //Play Animation
        }
    }

}

