using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.EnemyController;
using OverTheWall.Enums;

namespace OverTheWall.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        
        private ProjectilCurveType CurveType { get; set; }
        private ProjectileType Type { get; set; }
        private ProjectileShotFrom ShotBy { get; set; }
        private Vector2 StartingPosition { get; set; }
        private Vector2 TargetPosition { get; set; }
        private float Speed { get; set; }
        private float Damage { get; set; }
        private Rigidbody2D rigidBody;
        private bool canCauseDamage = true;
        private bool IsDead = false;


        // Use this for initialization
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public virtual void Initialize(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
        {
            this.Type = Type;
            this.CurveType = CurveType;
            this.StartingPosition = sp;
            this.TargetPosition = tp;
            this.Speed = speed;
            this.Damage = damage;
            this.ShotBy = shotBy;

            RotateTowardsObject();
        }

        void RotateTowardsObject()
        {
            Vector2 vectorToTarget = TargetPosition - StartingPosition;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
        }

        // Update is called once per frame
        public void UpdatePosition()
        {
            switch (CurveType)
            {
                case ProjectilCurveType.Straight:
                    UpdatePosition_Straight();
                    break;
                case ProjectilCurveType.SlightCurve:
                    UpdatePosition_SlightCurve();
                    break;
                case ProjectilCurveType.HeavyCurve:
                    UpdatePosition_HeavyCurve();
                    break;
                default:
                    break;
            }
        }

        void UpdatePosition_Straight()
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }

        void UpdatePosition_SlightCurve()
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }

        void UpdatePosition_HeavyCurve()
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckCollision(collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckCollision(collision);
        }

        private void CheckCollision(Collider2D collision)
        {
            switch (ShotBy)
            {
                case ProjectileShotFrom.Player:
                    ShotByPlayer(collision);
                    break;
                case ProjectileShotFrom.Enemy:
                    ShotByEnemy(collision);
                    break;
                default:
                    break;
            }
        }

        private void ShotByPlayer(Collider2D collision)
        {
            if (collision.gameObject != null)
            {
                if (!string.IsNullOrEmpty(collision.gameObject.tag))
                {
                    if (collision.gameObject.tag.ToLower() == "enemy" && canCauseDamage)
                    {
                        EnemyController.EnemyController enemy = collision.gameObject.GetComponent<EnemyController.EnemyController>();

                        enemy.OnHit(Damage);

                        canCauseDamage = false;

                        IsDead = true;
                    }
                    else if (collision.gameObject.tag.ToLower() == "ground")
                    {
                        IsDead = true;
                    }
                }
            }
        }

        private void ShotByEnemy(Collider2D collision)
        {
            if (collision.gameObject != null)
            {
                if (!string.IsNullOrEmpty(collision.gameObject.tag))
                {
                    if (collision.gameObject.tag.ToLower() == "wall" && canCauseDamage)
                    {
                        IsDead = true;

                        CastleController enemy = collision.gameObject.GetComponent<CastleController>();

                        enemy.OnHit(Damage);

                        canCauseDamage = false;
                    }
                    else if (collision.gameObject.tag.ToLower() == "ground")
                    {
                        IsDead = true;
                    }
                }
            }
        }

        public bool IsDestroyed()
        {
            return IsDead;
        }
    }
}

