using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] public int vidaInimigo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void TomarDano()
    {
        vidaInimigo--; // Reduz a vida do inimigo        

        if (vidaInimigo <= 0) // Verifica se a vida é menor ou igual a zero
        {
            //Chamar audio death   
            AudioController.instance.AudioBoss();

            StartCoroutine(Destroy()); // chama a pausa da corrotina
                       
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f); // pausa a rotina por 2 segundos
        Destroy(this.gameObject); // Destroi o inimigo
    }
}

