using OverTheWall.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverTheWall.TurretBase
{
    public class TurrentBase : MonoBehaviour
    {
        private TurretType turretType;
        private Projectile.Projectile projectile;
        private float cooldown;
        private float damage;
        private Vector2 launchPosition;
        private ProjectileType projectileType;

        public void InitializeTurret(TurretType turretType, Projectile.Projectile projectile, float cooldown,
            float damage, ProjectileType projectileType)
        {
            this.turretType = turretType;
            this.projectile = projectile;
            this.cooldown = cooldown;
            this.damage = damage;
            this.launchPosition = Vector2.zero;
            this.projectileType = projectileType;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddProjectile(Vector2 endDragPosition, float angle, float speed)
        {
            ProjectileManager.AddPlayerProjectile(projectileType, launchPosition, endDragPosition, angle, speed, damage);
        }

        public float GetCooldown()
        {
            return cooldown;
        }
    }
}
