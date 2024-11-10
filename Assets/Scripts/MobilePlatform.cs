using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    PlayerController pMove;

    public float speed;

    public Transform[] pointsToMove;
    public int startingPoint;
    
    void Start()
    {
        pMove = pMove = PlayerController.instance;
        transform.position = pointsToMove[startingPoint].transform.position;
    }

    private void FixedUpdate()
    {
        Move();
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
        if (collision.gameObject.tag == "Player" && pMove.isOnPlatform)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void onCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }    
}
