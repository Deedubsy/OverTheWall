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
            float damage, ProjectileType projectileType, Vector2 launchPosition)
        {
            this.turretType = turretType;
            this.projectile = projectile;
            this.cooldown = cooldown;
            this.damage = damage;
            this.launchPosition = new Vector2(transform.position.x + 2, transform.position.y);
            this.projectileType = projectileType;

            gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddProjectile(Vector2 endDragPosition, float angle, float speed)
        {
            ProjectileManager.AddPlayerProjectile(projectileType, launchPosition, endDragPosition, angle, speed, damage);
        }
        
        public void TurretSelected()
        {
            gameObject.SetActive(true);
        }

        public void TurretChanged()
        {
            gameObject.SetActive(false);
        }

        public float GetCooldown()
        {
            return cooldown;
        }
    }
}
