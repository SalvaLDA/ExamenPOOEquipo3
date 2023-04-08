using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    NewPlayerMovement player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMovement>();
    }

    void Update()
    {
        //player.movementsLeft--;
    }
}
