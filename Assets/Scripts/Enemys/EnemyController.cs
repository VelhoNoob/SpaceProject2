using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer ImagemEnemy;

    [SerializeField] private float velocidade;
    [SerializeField] private float distInicial = -0.5f;
    [SerializeField] private float distFinal = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ImagemEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Andar();
    }

    void Andar()
    {
        transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
        // MUDAR VELOVIDADE
        //Andar para trás
        if (transform.position.x > distFinal)
        {
            velocidade = velocidade * -1;
            ImagemEnemy.flipX = true;
        }
        //Andar para frente
        if (transform.position.x < distInicial)
        {
            velocidade = velocidade * -1;
            ImagemEnemy.flipX = false;
        }
    }
}
