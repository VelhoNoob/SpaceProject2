using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTiro : MonoBehaviour
{
    private float velocidade_bala = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoverBala();

    }

    void MoverBala()
    {
        //movimentação
        transform.position = new Vector3(transform.position.x + velocidade_bala, transform.position.y, transform.position.z);
    }

    public void DirecaoBala(float direcao)
    {
        velocidade_bala = direcao;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "Enemy")
        {
            //Outro objeto
            Destroy(colisao.gameObject);

            //Esse objeto
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.tag == "Ground")
        {
            //Esse objeto
            Destroy(this.gameObject);
        }

    }
}
