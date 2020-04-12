using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyB : MonoBehaviour
{
    public GameObject playerNuke;
    public float speed;
    public Transform playerRB;
    public Animator animator;
    public LineRenderer trackingLR;
    public LineRenderer shootingLR;
    public Transform target;
    public Transform firePoint;
    public Vector2 direction;
    public int randomizer;
    public bool tracking = false;
    public bool shooting = false;
    public GameObject self;
    public AudioClip gunshot;
    public AudioClip windup;
    public float trackingUpdate;
    public float tracker;

    // Start is called before the first frame update
    void Start()
    {

        playerRB = GameObject.FindWithTag("Player").transform;
        playerNuke = GameObject.FindWithTag("Player");
        trackingLR.enabled = false;
        StartCoroutine(Tracking());
        StartCoroutine(UpdateTrack());
        trackingLR.enabled = false;
        shootingLR.enabled = false;
        GetComponent<AudioSource>().clip = gunshot;
        GetComponent<AudioSource>().clip = windup;
        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(playerRB.position.x - transform.position.x, playerRB.position.y - transform.position.y);
        randomizer = Random.Range(-5, 5);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        StartCoroutine(Aim());
        if (playerNuke.GetComponent<Player>().nuked)
        {
            DestroySelf();
        }
    }

    IEnumerator Aim()
    {
        if (tracking == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, 300f ,1 << 10);
            if (hitInfo.collider != null)
            {
                trackingLR.SetPosition(0, firePoint.position);
                trackingLR.SetPosition(1, hitInfo.point);
            }
            else if (hitInfo.collider == null)
            {
                trackingLR.SetPosition(0, firePoint.position);
                trackingLR.SetPosition(1, firePoint.position + firePoint.up * 100);
                
            }

        }
        else if (tracking == false && shooting == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, 300f, 1 << 10);
            shootingLR.enabled = true;
            GetComponent<AudioSource>().PlayOneShot(gunshot);
            if (hitInfo.collider != null)
            {
                Player player = hitInfo.collider.GetComponent<Player>();
                if( player != null)
                {
                    player.GetComponent<Player>().TakeDamage();
                    shootingLR.SetPosition(0, firePoint.position);
                    shootingLR.SetPosition(1, hitInfo.point);
                }
            }

            else if (hitInfo.collider == null)
            {
                shootingLR.SetPosition(0, firePoint.position);
                shootingLR.SetPosition(1, firePoint.up * 100);
            }
            shooting = false;
            
            yield return 0.02f;
            shootingLR.enabled = false;
        }
    }

    IEnumerator Tracking()
    {
        while (true)
        {
            yield return new WaitForSeconds(tracker);
            GetComponent<AudioSource>().PlayOneShot(windup);
            tracking = !tracking;
            trackingLR.enabled = tracking;
            shooting = !tracking;
        }
    }

    IEnumerator UpdateTrack()
    {
        while (true)
        {
            yield return new WaitForSeconds(trackingUpdate);
            direction += new Vector2(randomizer, randomizer);
            if(tracking == true)
            {
                transform.up = direction;
            }
        }
    }


    public void DestroySelf()
    {
        Destroy(self);
    }
}
