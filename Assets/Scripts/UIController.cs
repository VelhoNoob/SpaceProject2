using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    

    [SerializeField] private Image[] heatsImage;
    private int vidas;
    private Text vidas_texto;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        vidas = PlayerHealthController.instance.vidas;
        vidas_texto = GameObject.FindGameObjectWithTag("TextoVidasTag").GetComponent<Text>();
        vidas_texto.text = "Vidas: " + vidas.ToString();                
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateHealthDisplay(int health,int maxHealth)
    {        

        for (int i = 0; i < heatsImage.Length; i++)
        {
            heatsImage[i].enabled = true;

            if (health <= i)
            {
                heatsImage[i].enabled = false;
            }
                        
        }
    }

    public void UpdateVidasDisplay()
    {
        vidas_texto.text = "Vidas: " + PlayerHealthController.instance.vidas.ToString();
    }

}
