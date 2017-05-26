using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    //public GameObject[] objects;
    public GameObject testObject;

    private float spawnerCountdown = 5.0f;
    private float gameTimer = 0.0f;
    private float countdownMax = 10.0f;

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
        //Instantiate(objects[Random.Range(0, objects.Length - 1)]);

        Instantiate(testObject, transform.position, new Quaternion());
    }
}
