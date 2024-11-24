using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarInimigo : MonoBehaviour
{
    private EyeIA script;
    // Start is called before the first frame update
    void Start()
    {
        script = (EyeIA)GetComponentInParent(typeof(EyeIA));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            script.player = col.transform; // Atribui o jogador ao EyeIA
            script.lostPlayer = false;
            script.canFly = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            script.player = null; // Remove a referência ao jogador
            script.BackToHome();
            script.lostPlayer = true;
            script.canFly = true;
        }
    }
}
