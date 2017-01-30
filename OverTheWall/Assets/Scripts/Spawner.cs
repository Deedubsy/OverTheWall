using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject[] objects;

    private float spawnerCountdown = 2.0f;
    private float gameTimer = 0.0f;
    private float countdownMax = 5.0f;

    // Use this for initialization
    void Start()
    {

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
        Instantiate(objects[Random.Range(0, objects.Length - 1)]);
    }
}
