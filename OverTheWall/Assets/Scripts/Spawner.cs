using UnityEngine;
using System.Collections;
using OverTheWall.EnemyController;

public class Spawner : MonoBehaviour
{

    //public GameObject[] objects;
    public GameObject swordsman;
    public GameObject swordAndShieldsman;
    public GameObject archer;

    private float spawnerCountdown = 5.0f;
    private float gameTimer = 0.0f;
    private float countdownMax = 10.0f;
    private Vector3 SpawnPosition;

    // Use this for initialization
    void Start()
    {
        SpawnPosition = new Vector3(transform.position.x, -2, transform.position.z);
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
    }

    public void SpawnRandom()
    {
        switch (GetUnitToSpawn())
        {
            case Enemy_Type.Sword:
                Instantiate(swordsman, transform.position, new Quaternion());
                break;
            case Enemy_Type.SwordAndShield:
                Instantiate(swordAndShieldsman, transform.position, new Quaternion());
                break;
            case Enemy_Type.Archer:
                Instantiate(archer, transform.position, new Quaternion());
                break;
            default:
                break;
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
