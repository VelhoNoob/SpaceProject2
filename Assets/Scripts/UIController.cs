using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    

    [SerializeField] private Image[] heatsImage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

}
