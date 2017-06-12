using OverTheWall.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverTheWall.TurretBase
{
    public class TurrentBase : MonoBehaviour
    {
        //PUBLIC


        //PRIVATE
        private float Cooldown;
        private float AdditionalDamage;
        private int TurretLevel;

        private TurretType turretType;
        private Projectile.Projectile projectile;
        private Vector2 launchPosition;
        private ProjectileType projectileType;

        // Call from inherited class
        public void InitializeTurret(TurretType turretType, Projectile.Projectile projectile, float cooldown,
            float additionalDamage, ProjectileType projectileType, Vector2 launchPosition)
        {
            this.turretType = turretType;
            this.projectile = projectile;
            this.Cooldown = cooldown;
            this.AdditionalDamage = additionalDamage;
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
            ProjectileManager.AddPlayerProjectile(projectileType, launchPosition, endDragPosition, angle, speed, AdditionalDamage);
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public void SetActive()
        {
            gameObject.SetActive(true);
        }

        public void TurretChanged()
        {
            gameObject.SetActive(false);
        }

        public float GetCooldown()
        {
            return Cooldown;
        }
    }
}
