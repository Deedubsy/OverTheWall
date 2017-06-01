using UnityEngine;
using System.Collections;
using OverTheWall.EnemyController;
using System.Collections.Generic;
using OverTheWall.Enums;

public class Spawner : MonoBehaviour
{

    //public GameObject[] objects;
    public GameObject swordsman;
    public GameObject swordAndShieldsman;
    public GameObject archer;

    private float spawnerCountdown = 5.0f;
    private float gameTimer = 0.0f;
    private float countdownMax = 3.0f;
    private Vector3 SpawnPosition;

    private List<EnemyTypeToStore> enemies;
    private int maxEnemies = 1;
    private int currentEnemies = 0;

    private class EnemyTypeToStore
    {
        public EnemyTypeToStore() { }

        public EnemyTypeToStore(GameObject ObjectToStore, Enemy_Type EnemyType)
        {
            this.ObjectToStore = ObjectToStore;
            this.EnemyType = EnemyType;

            this.EnemyController = ObjectToStore.GetComponent<EnemyController>();
        }

        public EnemyController EnemyController { get; set; }
        public GameObject ObjectToStore { get; set; }
        public Enemy_Type EnemyType { get; set; }
    }

    // Use this for initialization
    void Start()
    {
        SpawnPosition = new Vector3(transform.position.x, -2, transform.position.z);
        enemies = new List<EnemyTypeToStore>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        //if spawner 0 spawn new then update the spawn countdown
        if (spawnerCountdown <= 0)
        {
            SpawnRandom();

            spawnerCountdown = Random.Range(0, countdownMax);
        }
        else
        {
            spawnerCountdown -= Time.deltaTime;
        }

        CheckIfDead();
    }

    void CheckIfDead()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].EnemyController.IsEnemyDead())
            {
                Destroy(enemies[i].ObjectToStore);
                enemies.Remove(enemies[i]);
                currentEnemies--;
            }
        }
    }

    public void SpawnRandom()
    {
        EnemyTypeToStore enemyToSpawn = new EnemyTypeToStore();


        if (currentEnemies < maxEnemies)
        {
            enemyToSpawn.ObjectToStore = Instantiate(archer, transform.position, new Quaternion());
            enemyToSpawn.EnemyType = Enemy_Type.Archer;
            
            //switch (GetUnitToSpawn())
            //{
            //    case Enemy_Type.Sword:
            //        enemyToSpawn.ObjectToStore = Instantiate(swordsman, transform.position, new Quaternion());
            //        enemyToSpawn.EnemyType = Enemy_Type.Sword;
            //        break;
            //    case Enemy_Type.SwordAndShield:
            //        enemyToSpawn.ObjectToStore = Instantiate(swordAndShieldsman, transform.position, new Quaternion());                
            //        enemyToSpawn.EnemyType = Enemy_Type.SwordAndShield;
            //        break;
            //    case Enemy_Type.Archer:
            //        enemyToSpawn.ObjectToStore = Instantiate(archer, transform.position, new Quaternion());
            //        enemyToSpawn.EnemyType = Enemy_Type.Archer;
            //        break;
            //    default:
            //        break;
            //}

            enemies.Add(new EnemyTypeToStore(enemyToSpawn.ObjectToStore, enemyToSpawn.EnemyType));

            currentEnemies++;
        }
    }

    private Enemy_Type GetUnitToSpawn()
    {
        Enemy_Type enemyTypes = Enemy_Type.Sword;

        float range = Random.Range(0, 100);

        if(range <= 50)
        {
            enemyTypes = Enemy_Type.Sword;
        }
        else if (range > 50 && range <= 70)
        {
            enemyTypes = Enemy_Type.SwordAndShield;
        }
        else if (range > 70)
        {
            enemyTypes = Enemy_Type.Archer;
        }

        return enemyTypes;
    }
}
