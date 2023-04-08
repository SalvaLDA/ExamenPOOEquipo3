using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private bool isMoving;
    public bool collide;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.3f;

    public LayerMask movableEntity;
    public bool enemyUp, enemyDown, enemyLeft, enemyRight;
    public LayerMask terrain;
    public bool terrainUp, terrainDown, terrainLeft, terrainRight;

    public float cooldownBetweenMovements = 0.5f;
    private float cdMove;
    public int movementsLeft;

    EnemyPushed push;

    void Start()
    {
        push = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPushed>();
    }

    void Update()
    {
        cdMove -= Time.deltaTime;
        if(movementsLeft > 0) 
        {
            if (Input.GetKey(KeyCode.W) && cdMove <= 0) 
            { 
                if (!isMoving && !enemyUp && !terrainUp)
                {
                    StartCoroutine(MovePlayer(Vector3.up));
                    cdMove = cooldownBetweenMovements;
                } else if (!isMoving && enemyUp && !terrainUp)
                {
                    push.BePushedUp();
                    cdMove = cooldownBetweenMovements;
                }
            }
            if (Input.GetKey(KeyCode.A) && cdMove <= 0)
            {
                if (!isMoving && !enemyLeft && !terrainLeft)
                {
                    StartCoroutine(MovePlayer(Vector3.left));
                    cdMove = cooldownBetweenMovements;
                }
                else if (!isMoving && enemyLeft && !terrainLeft)
                {
                    push.BePushedLeft();
                    cdMove = cooldownBetweenMovements;
                }
            }
            if (Input.GetKey(KeyCode.S) && cdMove <= 0)
            {
                if (!isMoving && !enemyDown && !terrainDown)
                {
                    StartCoroutine(MovePlayer(Vector3.down));
                    cdMove = cooldownBetweenMovements;
                }
                else if (!isMoving && enemyDown && !terrainDown)
                {
                    push.BePushedDown();
                    cdMove = cooldownBetweenMovements;
                }
            }
            if (Input.GetKey(KeyCode.D) && cdMove <= 0)
            {
                if (!isMoving && !enemyRight && !terrainRight)
                {
                    StartCoroutine(MovePlayer(Vector3.right));
                    cdMove = cooldownBetweenMovements;
                }
                else if (!isMoving && enemyRight && !terrainRight)
                {
                    push.BePushedRight();
                    cdMove = cooldownBetweenMovements;
                }
            }
        }
        else
        {
            Debug.Log("Animación de muerte y se reinicia el nivel");
        }
    }

    private void FixedUpdate()
    {
        CheckForEnemy();
        CheckForTerrain();
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;
        
        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
        movementsLeft--;
    }

    private void CheckForEnemy()
    {
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1f), 0.2f, movableEntity))
        {
            enemyUp = true;
        }
        else
        {
            enemyUp = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, -1f), 0.2f, movableEntity))
        {
            enemyDown = true;
        }
        else
        {
            enemyDown = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(-1f, 0f), 0.2f, movableEntity))
        {
            enemyLeft = true;
        }
        else
        {
            enemyLeft = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0f), 0.2f, movableEntity))
        {
            enemyRight = true;
        }
        else
        {
            enemyRight = false;
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
