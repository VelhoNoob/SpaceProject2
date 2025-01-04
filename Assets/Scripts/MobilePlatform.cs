using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    PlayerController pMove;

    public float speed;

    public Transform[] pointsToMove;
    public int startingPoint;

    public bool isSpecialPlatform; // Indica se esta plataforma deve ser ativada por colisão
    private bool isMoving; // Indica se a plataforma está se


    void Start()
    {
        pMove = pMove = PlayerController.instance;
        transform.position = pointsToMove[startingPoint].transform.position;
        isMoving = !isSpecialPlatform; // Plataformas não especiais começam a se mover automaticamente
    }

    private void FixedUpdate()
    {
        // Move();
        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointsToMove[startingPoint].transform.position, speed * Time.fixedDeltaTime);

        if (transform.position == pointsToMove[startingPoint]. transform.position)
        {
            startingPoint += 1;
        }

        if (startingPoint == pointsToMove.Length)
        {
            startingPoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            // Ativa o movimento para plataformas especiais
            if (isSpecialPlatform && !isMoving)
            {
                isMoving = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
