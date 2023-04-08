using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPushed : MonoBehaviour
{
    public bool isBox;

    public LayerMask terrain;
    public bool terrainUp, terrainDown, terrainLeft, terrainRight;

    private void FixedUpdate()
    {
        CheckForTerrain();
    }

    public void BePushedUp() 
    {
        if (!terrainUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
        } 
        else if (terrainUp && !isBox)
        {
            Destroy(this.gameObject);
        }
    }
    public void BePushedDown()
    {
        if (!terrainDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f);
        }
        else if (terrainDown && !isBox)
        {
            Destroy(this.gameObject);
        }
    }
    public void BePushedLeft()
    {
        if (!terrainLeft)
        {
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y);
        } 
        else if (terrainLeft && !isBox)
        {
            Destroy(this.gameObject);
        }
    }
    public void BePushedRight()
    {
        if (!terrainRight)
        {
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y);
        }
        else if (terrainRight && !isBox)
        {
            Destroy(this.gameObject);
        }
    }
    private void CheckForTerrain()
    {
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1f), 0.2f, terrain))
        {
            terrainUp = true;
        }
        else
        {
            terrainUp = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, -1f), 0.2f, terrain))
        {
            terrainDown = true;
        }
        else
        {
            terrainDown = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(-1f, 0f), 0.2f, terrain))
        {
            terrainLeft = true;
        }
        else
        {
            terrainLeft = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0f), 0.2f, terrain))
        {
            terrainRight = true;
        }
        else
        {
            terrainRight = false;
        }
    }
}
