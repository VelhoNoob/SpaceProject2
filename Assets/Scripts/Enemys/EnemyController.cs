using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    private SpriteRenderer ImagemEnemy;        

    [SerializeField] private float velocidade;
    [SerializeField] private float distInicial;
    [SerializeField] private float distFinal;
    [SerializeField] public int vidaInimigo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

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

    public void TomarDano()
    {
        vidaInimigo--; // Reduz a vida do inimigo        

        if (vidaInimigo <= 0) // Verifica se a vida é menor ou igual a zero
        {
            //Chamar audio death
            AudioController.instance.AudioInimigo();

            StartCoroutine(Destroy()); // chama a pausa da corrotina

            // Notificar o sistema de conquistas   
            AchievementSystem.instance.EnemyDefeated();
        }   
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.3f); // pausa a rotina por 2 segundos
        Destroy(this.gameObject); // Destroi o inimigo
    }
}

