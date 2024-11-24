using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EyeIA : MonoBehaviour
{
    public Transform eyehome;
    public Transform eye;
    public Transform player;
    private Vector3 positionPlayerLost;
    private Vector3 positionPlayerFind;

    public float speed;
    private float starttime;
    private float journeyLenght;
    public bool lostPlayer = true;
    public bool canFly = false;
    // Start is called before the first frame update
    void Start()
    {
        eye = GetComponent<Transform>();
        eyehome = eye.transform.parent;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        positionPlayerLost = eyehome.position;
        BackToHome ();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFly)
        {
            if (lostPlayer)
            {
                float dist = (Time.time - starttime) * speed;
                float journey = dist / journeyLenght;
                if (eye.position == eyehome.position)
                {
                    canFly = false;
                }
                eye.position = Vector3.Lerp(positionPlayerLost, eyehome.position, journey);
            }
            else if (player != null) // Verifica se o player foi atribuído
            {
                eye.position = Vector3.Lerp(eye.position, player.position, 0.02f);
            }
        }
    }
    public void BackToHome()
    { 
        starttime = Time.time;
        positionPlayerLost = eye.position;
        journeyLenght = Vector3.Distance(positionPlayerLost, eyehome.position);
    }
}
