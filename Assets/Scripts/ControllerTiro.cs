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
        /*
        if (colisao.gameObject.CompareTag("Enemy"))
        {
            // Obtém o script do inimigo atingido
            EnemyController enemy = colisao.gameObject.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);            
        }

        if (colisao.gameObject.CompareTag("Bat"))
        {
            // Obtém o script do inimigo atingido
            BatController bat = colisao.gameObject.GetComponent<BatController>();

            if (bat != null)
            {
                bat.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.CompareTag("Boss"))
        {
            // Obtém o script do inimigo atingido
            BossController boss = colisao.gameObject.GetComponent<BossController>();

            if (boss != null)
            {
                boss.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);            
        }

        if (colisao.gameObject.CompareTag("Ground") && colisao.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destroi a bala ao colidir com o chão
            Debug.Log("Colisão com Ground detectada!");
            Destroy(this.gameObject);
        }   
        */

    }

        

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Obtém o script do inimigo atingido
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bat"))
        {
            // Obtém o script do inimigo atingido
            BatController bat = other.GetComponent<BatController>();

            if (bat != null)
            {
                bat.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            // Obtém o script do inimigo atingido
            BossController boss = other.GetComponent<BossController>();

            if (boss != null)
            {
                boss.TomarDano(); // Aplica o dano ao inimigo
            }

            // Destroi a bala
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Ground") && other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destroi a bala ao colidir com o chão
            Debug.Log("Trigger com Ground detectado!");
            Destroy(this.gameObject);
        }
    }
}
