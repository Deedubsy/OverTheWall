using OverTheWall.EnemyController;
using OverTheWall.Enums;
using OverTheWall.Projectile;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static Projectile arrow;

    private static List<ProjectilesToStore> listOfProjectiles { get; set; }

    private class ProjectilesToStore
    {
        public ProjectilesToStore() { }

        public ProjectilesToStore(Projectile ObjectToStore, Enemy_Type EnemyType)
        {
            this.ObjectToStore = ObjectToStore;

            this.ProjectileController = ObjectToStore.GetComponent<Projectile>();
        }

        public Projectile ProjectileController { get; set; }
        public Projectile ObjectToStore { get; set; }
    }

    void Start()
    {
        arrow = FindObjectOfType<Arrow>();
        listOfProjectiles = new List<ProjectilesToStore>();
    }

    public static void AddProjectile(ProjectileType Type, Vector2 sp, Vector2 tp, float speed, float damage, ProjectileShotFrom shotBy, ProjectilCurveType CurveType)
    {
        ProjectilesToStore projectileToSpawn = new ProjectilesToStore();

        projectileToSpawn.ObjectToStore = Instantiate(GetObjectToShoot(Type), new Vector3(sp.x, sp.y, 0), new Quaternion());
        projectileToSpawn.ProjectileController = projectileToSpawn.ObjectToStore.GetComponent<Projectile>();
        projectileToSpawn.ProjectileController.InitializeProjectile(Type, sp, tp, speed, damage, shotBy, CurveType);

        listOfProjectiles.Add(projectileToSpawn);
    }

    public static void AddPlayerProjectile(ProjectileType Type, Vector2 sp, Vector2 tp, float angle, float speed, float damage)
    {
        ProjectilesToStore projectileToSpawn = new ProjectilesToStore();

        projectileToSpawn.ObjectToStore = Instantiate(GetObjectToShoot(Type), new Vector3(sp.x, sp.y, 0), new Quaternion());
        projectileToSpawn.ProjectileController = projectileToSpawn.ObjectToStore.GetComponent<Projectile>();

        projectileToSpawn.ProjectileController.InitializePlayerProjectile(Type, sp, tp, angle, speed, damage);

        listOfProjectiles.Add(projectileToSpawn);
    }

    private static Projectile GetObjectToShoot(ProjectileType Type)
    {
        switch (Type)
        {
            case ProjectileType.Arrow:
                return arrow;
            default:
                break;
        }

        return null;
    }

    

    void Update()
    {
        if(listOfProjectiles != null && listOfProjectiles.Any())
        {
            for (int i = 0; i < listOfProjectiles.Count; i++)
            {
                //listOfProjectiles[i].ProjectileController.UpdatePosition();

                CheckIfDestroyed(i);
            }
        }
    }

    void CheckIfDestroyed(int i)
    {
        if (listOfProjectiles[i].ProjectileController.IsDestroyed())
        {
            Destroy(listOfProjectiles[i].ObjectToStore.gameObject);
            listOfProjectiles.Remove(listOfProjectiles[i]);
        }
    }
}
