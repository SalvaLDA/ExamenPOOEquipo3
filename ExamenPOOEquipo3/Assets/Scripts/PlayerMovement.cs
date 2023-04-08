using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public Transform movePoint;
    public LayerMask stopMovement;
    public LayerMask movableEntity;

    EnemyPushed enemyScript;

    public int movementsLeft;  //Los movimientos restantes son publicos para que las trampas lo reduzcan 1 más

    void Start()
    {
        movePoint.parent = null;
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPushed>();
    }

    /*
     * Queda por agregar el detector de enemigos, para poder ejecutar la animación de empujarlos o matarlos
     * cuando choquen contra un muro, la detección del objetivo para poder acabar el nivel y la animación de
     * muerte si se quedan sin movimientos, trampas 
     */

    void Update()
    {
        //Mover al personaje a la posición del controlador
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, playerSpeed * Time.deltaTime);

        if (movementsLeft > 0)  //Contador de movimientos
        {
            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)  //Movimiento horizontal
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, movableEntity))
                    {
                        //enemyScript.BePushed();
                        movementsLeft--;
                    }
                    else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, stopMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        movementsLeft--;
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)  //Movimiento vertical
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, movableEntity))
                    {
                        Debug.Log("Aqui se mueve el enemigo xD");
                        //Mover Enemigo
                    }
                    else if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, stopMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        movementsLeft--;
                    }
                }
            }
        }
    }
}
