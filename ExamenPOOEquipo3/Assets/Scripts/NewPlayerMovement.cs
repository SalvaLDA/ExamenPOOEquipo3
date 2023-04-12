using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.3f;

    [Header("Movement Constrains")]
    public LayerMask movableEntity;
    public bool enemyUp, enemyDown, enemyLeft, enemyRight;
    public LayerMask terrain;
    public bool terrainUp, terrainDown, terrainLeft, terrainRight;
    public LayerMask trap;
    public bool trapUp, trapDown, trapLeft, trapRight;

    [Header("Moving Conditions")]
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
                MoveUp();
            }
            if (Input.GetKey(KeyCode.A) && cdMove <= 0)
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.S) && cdMove <= 0)
            {
                MoveDown();
            }
            if (Input.GetKey(KeyCode.D) && cdMove <= 0)
            {
                MoveRight();
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
        CheckForTrap();
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

    private void CheckForTrap()
    {
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1f), 0.2f, trap))
        {
            trapUp = true;
        }
        else
        {
            trapUp = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, -1f), 0.2f, trap))
        {
            trapDown = true;
        }
        else
        {
            trapDown = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(-1f, 0f), 0.2f, trap))
        {
            trapLeft = true;
        }
        else
        {
            trapLeft = false;
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0f), 0.2f, trap))
        {
            trapRight = true;
        }
        else
        {
            trapRight = false;
        }
    }

    private void MoveUp()
    {
        if (!isMoving && !enemyUp && !terrainUp && !trapUp)
        {
            StartCoroutine(MovePlayer(Vector3.up));
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && !enemyUp && !terrainUp && trapUp)
        {
            StartCoroutine(MovePlayer(Vector3.up));
            movementsLeft--;
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && enemyUp && !terrainUp)
        {
            push.BePushedUp();
            cdMove = cooldownBetweenMovements;
        }
    }

    private void MoveDown()
    {
        if (!isMoving && !enemyDown && !terrainDown && !trapDown)
        {
            StartCoroutine(MovePlayer(Vector3.down));
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && !enemyDown && !terrainDown && trapDown)
        {
            StartCoroutine(MovePlayer(Vector3.down));
            movementsLeft--;
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && enemyDown && !terrainDown)
        {
            push.BePushedDown();
            cdMove = cooldownBetweenMovements;
        }
    }

    private void MoveLeft()
    {
        if (!isMoving && !enemyLeft && !terrainLeft && !trapLeft)
        {
            StartCoroutine(MovePlayer(Vector3.left));
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && !enemyLeft && !terrainLeft && trapLeft)
        {
            StartCoroutine(MovePlayer(Vector3.left));
            movementsLeft--;
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && enemyLeft && !terrainLeft)
        {
            push.BePushedLeft();
            cdMove = cooldownBetweenMovements;
        }
    }

    private void MoveRight()
    {
        if (!isMoving && !enemyRight && !terrainRight && !trapRight)
        {
            StartCoroutine(MovePlayer(Vector3.right));
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && !enemyRight && !terrainRight && trapRight)
        {
            StartCoroutine(MovePlayer(Vector3.right));
            movementsLeft--;
            cdMove = cooldownBetweenMovements;
        }
        else if (!isMoving && enemyRight && !terrainRight)
        {
            push.BePushedRight();
            cdMove = cooldownBetweenMovements;
        }
    }
}
