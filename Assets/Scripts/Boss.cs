using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rippleController;
    public GameObject player;
    public Transform playerRB;
    Vector3 target;
    public GameObject self;
    public float speed;
    public bool moving = false;
    public int hitPoints = 40;
    public GameObject mySpawner;
    // Start is called before the first frame update
    void Start()
    {
        mySpawner = GameObject.FindWithTag("bossSpawner");
        player = GameObject.FindWithTag("Player");
        playerRB = player.transform;
        target = (transform.position - playerRB.position).normalized;
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().nuked)
        {
            TakeDamage(10);
        }
        if (moving)
        {
            transform.position -= target * speed * Time.deltaTime;
        }
        if (hitPoints <= 0)
        {
            DestroySelf();
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            target = (transform.position - playerRB.position).normalized;
            yield return null;
        }
    }
    void Move()
    {
        moving = true;
    }

    void Stop()
    {
        rippleController.GetComponent<BossRippleController>().animator.SetTrigger("Ripple");
        moving = false;
    }
    public void TakeDamage( int damage)
    {
        hitPoints -= damage;
    }
    public void DestroySelf()
    {
        mySpawner.GetComponent<BossSpawner>().bossAlive = false;
        Destroy(self);
    }
}
