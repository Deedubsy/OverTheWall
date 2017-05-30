using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverTheWall.EnemyController;

namespace OverTheWall.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        public enum ProjectileType
        {
            Straight = 0,
            SlightCurve = 1,
            HeavyCurve = 2,
        }

        private ProjectileType Type { get; set; }
        private Vector2 StartingPosition { get; set; }
        private Vector2 TargetPosition { get; set; }
        private float Speed { get; set; }
        private float Damage { get; set; }
        private Rigidbody2D rigidBody;

        // Use this for initialization
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage)
        {
            this.Type = Type;
            this.StartingPosition = sp;
            this.TargetPosition = tp;
            this.Speed = speed;
            this.Damage = damage;
        }

        // Update is called once per frame
        public void UpdatePosition()
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
            //Vector2 direction = TargetPosition - new Vector2(transform.position.x, transform.position.y);
            //direction.Normalize();
            ////GameObject projectile = (GameObject)Instantiate(projectilePrefab, myPos, Quaternion.identity);
            //rigidBody.velocity = direction * Speed;
        }
    }
}

