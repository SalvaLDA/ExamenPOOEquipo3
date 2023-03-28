using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPushed : MonoBehaviour
{
    public GameObject player;
    public LayerMask playerCollide;

    public bool canBePushedRight,canBePushedLeft,canBePushedUp,canBePushedDown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0f), 0.2f, playerCollide))
        {
            canBePushedRight = true;
        } 
        else
        {
            canBePushedRight = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(-1f, 0f), 0.2f, playerCollide))
        { 
            canBePushedLeft = true;
        }
        else
        {
            canBePushedLeft = false;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
