using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blankTimer : MonoBehaviour
{

    public Text Blast;
    // pass blankCD (float variable) from player object to this variable
    public float cooldown;
    // pass blankStart (float variable) from player object to this variable
    public float blankStart;
    // calculate the two variables above and store result in this variable
    public float cooldownRefresh; 
    // calculate the diffrence between time.time and the cooldownRefresh variable and store it in this variable (for UI purpose)
    public float display;

    public GameObject player;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        blankStart = player.GetComponent<Player>().blankStart;
        cooldown = player.GetComponent<Player>().blankCD;
        cooldownRefresh = blankStart + cooldown;
        
        if (Time.time < blankStart + cooldown)
        {
            display = cooldownRefresh - Time.time;
        }

        Blast.text = "Blank Cooldown (Space):   " + display.ToString("f0");
    }
}
