using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
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
            AudioController.instance.AudioBat();
                      
            Destroy(this.gameObject);

            // Notificar o sistema de conquistas   
            AchievementSystem.instance.EnemyDefeated();
        }
    }

}


