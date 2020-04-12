using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NukeTimer : MonoBehaviour
{
    public Text Nuke;

    public float nukeCooldown;
    public float nukeStart;
    public float nukeRefresh;
    public float nukeDisplay;

    public GameObject player;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        nukeStart = player.GetComponent<Player>().nukeStart;
        nukeCooldown = player.GetComponent<Player>().nukeCD;
        nukeRefresh = nukeStart + nukeCooldown;

        if (Time.time < nukeStart + nukeCooldown)
        {
            nukeDisplay = nukeRefresh - Time.time;
        }

        Nuke.text = "Nuke Cooldown (E):   " + nukeDisplay.ToString("f0");
    }
}


