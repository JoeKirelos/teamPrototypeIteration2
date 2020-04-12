using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public bool bossSpawned = false;
    public bool spawnTiming;
    public bool bossAlive = false;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTiming)
        {
            Spawn();
            spawnTiming = false;
        }
    }

    void Spawn()
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        bossSpawned = true;
        bossAlive = true;
    }
}
