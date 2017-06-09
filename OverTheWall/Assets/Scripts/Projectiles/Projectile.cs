using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.EnemyController;
using OverTheWall.Enums;
using OverTheWall.SharedFunctions;

namespace OverTheWall.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        private ProjectilCurveType CurveType { get; set; }
        private ProjectileType Type { get; set; }
        private ProjectileShotFrom ShotBy { get; set; }
        private Vector2 StartingPosition { get; set; }
        private Vector2 TargetPosition { get; set; }
        private Rigidbody2D rigidBody2d;

        private bool canCauseDamage = true;
        private bool IsDead = false;

        //Turret Stats
        private float Speed { get; set; }
        private float BaseDamage { get; set; }
        private float AdditionalDamage;
        private float FiringAngle = 45;
        private float ElapsedTime = 0;
        private float Gravity = 9.8f;
        private float Angle = 0.0f;
        private float Velocity = 0.0f;
        private float Cooldown = 0.0f;


        float Vx = 0;
        float Vy = 0;

        // Use this for initialization
        void Start()
        {

        }

        public void InitializeProjectile(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float baseDamage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
        {
            this.Type = Type;
            this.CurveType = CurveType;
            this.StartingPosition = sp;
            this.TargetPosition = tp;
            this.Speed = speed;
            this.BaseDamage = baseDamage;
            this.ShotBy = shotBy;

            RotateTowardsObject();

            CalculateParabolicTrajetory();
        }

        public void InitializePlayerProjectile(ProjectileType Type, Vector2 sp, Vector2 tp, float angle, float speed, float AdditionalDamage)
        {
            this.Type = Type;
            this.CurveType = CurveType;
            this.StartingPosition = sp;
            this.TargetPosition = tp;
            this.Speed = speed;
            this.ShotBy = ProjectileShotFrom.Player;
            this.Angle = angle;
            this.AdditionalDamage = AdditionalDamage;
        }

        public void InitializePrivateVariables(float cooldown, float damage)
        {
            this.BaseDamage = damage;
            this.Cooldown = cooldown;
        }

        public void SetPlayerProjectileForce(Rigidbody2D rb2d = null)
        {
            rigidBody2d = rb2d;

            var direction = TargetPosition - StartingPosition;
            direction = direction.normalized;

            rb2d.AddForce(direction * (Speed * 10));
        }

        void RotateTowardsObject()
        {

            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 vectorToTarget = currentPos - StartingPosition;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // Update is called once per frame
        public void UpdatePosition()
        {
            if (ShotBy != ProjectileShotFrom.Player)
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

            RotateTowardsObject();
        }

        void UpdatePosition_Straight()
        {
            UpdateProjectilePosition();
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

        private void UpdateProjectilePosition()
        {
            transform.Translate(Vx * Time.deltaTime, (Vy - (Physics2D.gravity.magnitude * ElapsedTime)) * Time.deltaTime, 0);

            ElapsedTime += Time.deltaTime;

            //transform.Translate()
        }
        
        private void CalculateParabolicTrajetory()
        {
            // Short delay added before Projectile is thrown
            //yield return new WaitForSeconds(1.5f);

            // Move projectile to the position of throwing object + add some offset if needed.
            transform.position = transform.position + new Vector3(0, 0.0f, 0);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(transform.position, TargetPosition);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * FiringAngle * Mathf.Deg2Rad) / Gravity);

            // Extract the X  Y componenent of the velocity
            Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(FiringAngle * Mathf.Deg2Rad);
            Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(FiringAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            //Projectile.rotation = Quaternion.LookRotation(TargetPosition - Projectile.position);

            //transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            transform.Translate(Vx * Time.deltaTime, (Vy - (Gravity * ElapsedTime)) * Time.deltaTime, 0);
            ElapsedTime += Time.deltaTime;

            //while (elapse_time < flightDuration)
            //{


            //    yield return null;
            //}
        }

        private Vector2 GetParabola()
        {
            // What we know
            float gravity = Physics2D.gravity.magnitude;
            float height = Mathf.Abs(TargetPosition.y - transform.position.y);
            float dist = Mathf.Abs(TargetPosition.x - transform.position.x);

            // What we want to find
            float vertVelocity = 0.0f;
            float time = 0.0f;
            float horzVelocity = 0.0f;

            if (height < 0.1f) height = 0.1f;   // Prevents division by zero
            if (gravity < 0.1f) gravity = 0.1f; // Prevents division by zero

            // If we are going upward
            // we will use a direct parabolic trajectory
            // and reach the highest point
            if (TargetPosition.y - transform.position.y > 1.0f)
            {
                vertVelocity = Mathf.Sqrt(2.0f * gravity * height);
                time = vertVelocity / gravity;
                horzVelocity = dist / time;
            }
            else if (TargetPosition.y - transform.position.y < -1.0f)
            {
                // If we are going downward
                // we will use a direct parabolic trajectory
                // with no vertical velocity
                vertVelocity = 0.0f;
                time = Mathf.Sqrt(2 * height / gravity);
                horzVelocity = dist / time;
            }
            else
            {
                // Else we will follow a full parabolic trajectory
                // and determine the height of the jump
                // depending on the distance between the 2 points
                height = dist / 4;
                vertVelocity = Mathf.Sqrt(2.0f * gravity * height);
                time = 2 * vertVelocity / gravity;
                horzVelocity = dist / time;
            }

            // Jump right
            if (TargetPosition.x - transform.position.x > 0.0f &&
               !float.IsNaN(vertVelocity) && !float.IsNaN(horzVelocity))
            {
                return new Vector2(horzVelocity, vertVelocity);
            }
            // Jump left
            else if (!float.IsNaN(vertVelocity) && !float.IsNaN(horzVelocity))
            {
                return new Vector2(-horzVelocity, vertVelocity);
            }
            else
            {
                return Vector2.zero;
            }
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

        private float GetDamage()
        {
            return BaseDamage + AdditionalDamage;
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

                        enemy.OnHit(GetDamage());

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

                        enemy.OnHit(GetDamage());

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

