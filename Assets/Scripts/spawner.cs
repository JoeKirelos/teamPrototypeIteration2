using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBPrefab;
    public GameObject miniPrefab;
    public Transform spawnPoint;
    GameObject currentEnemy;
    public float shootingIntervalEnemyA;
    public float randomizerEnemyAInt;
    public float bounceEnemyInt;
    public float enemyInitialShoot;
    public float EnemyBshootingCooldown;
    public float enemyBAccuracy;
    public Vector3 movement = new Vector3(1, 1,0);
    public float speed;
    public GameObject self;
    public int sniperThreshold;
    public int miniThreshold;
    public Transform corner;
    public int waveCounter;
    public int waveMax = 10;
    public float spawnRate;
    public bool bossSpawned;
    public bool bossAlive;
    public GameObject bossSpawner;
    // Start is called before the first frame update
    void Start()
    {
        bossSpawner = GameObject.FindWithTag("bossSpawner");
        spawnRate = 5;
        StartCoroutine(Spawn());
        StartCoroutine(OneTwo());

    }

    // Update is called once per frame
    void Update()
    {
        bossSpawned = bossSpawner.GetComponent<BossSpawner>().bossSpawned;
        bossAlive = bossSpawner.GetComponent<BossSpawner>().bossAlive;
        self.transform.position += (movement * speed * Time.deltaTime);

    }

    IEnumerator WaveControl()
    {
        yield return new WaitForSeconds(10);
        bossSpawner.GetComponent<BossSpawner>().bossSpawned = false;
        if (waveCounter >= waveMax)
        {
            waveCounter = 0;
            waveMax += 2;
            if(spawnRate > 1)
            {
                spawnRate -= 0.5f;
            }
        }        
    }
   IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if ( currentEnemy == null && waveCounter < waveMax)
            {
                waveCounter++;
                int percent = Random.Range(0, 100);
                if (percent > sniperThreshold)
                {
                    currentEnemy = (Instantiate(enemyBPrefab, spawnPoint.position, spawnPoint.rotation));
                    currentEnemy.GetComponent<enemyB>().trackingUpdate = enemyBAccuracy;
                    currentEnemy.GetComponent<enemyB>().tracker = EnemyBshootingCooldown;
                    currentEnemy.GetComponent<enemyB>().target = corner;
                }
                else if( percent < sniperThreshold && percent > miniThreshold)
                {
                    currentEnemy = (Instantiate(miniPrefab, spawnPoint.position, spawnPoint.rotation));
                }
                else
                {
                    currentEnemy = (Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation));
                    currentEnemy.GetComponent<enemyA>().initialShoot = enemyInitialShoot;
                    currentEnemy.GetComponent<enemyA>().shootInterval = shootingIntervalEnemyA;
                    currentEnemy.GetComponent<enemyA>().randomizerInt = randomizerEnemyAInt;
                    currentEnemy.GetComponent<enemyA>().bounceInt = bounceEnemyInt;
                }

            }else if( waveCounter >= waveMax)
            {
                if(bossSpawned == false)
                {
                    bossSpawner.GetComponent<BossSpawner>().spawnTiming = true;
                }
                if (bossSpawned == true && bossAlive == false)
                {
                    StartCoroutine(WaveControl());
                }
            }

        }

        

    }
    IEnumerator OneTwo()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            speed = -speed;
        }
    }
}
