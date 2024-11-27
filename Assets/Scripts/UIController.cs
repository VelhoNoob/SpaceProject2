using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    

    [SerializeField] private Image[] heatsImage;
    [SerializeField] private Text achievementText;
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

    public void ShowAchievementMessage(string message)
    {
        // Exemplo: Mostrar texto na tela
        achievementText.text = message; // `achievementText` seria um componente Text ou TMP na tela
        StartCoroutine(HideAchievementMessage());
    }

    private IEnumerator HideAchievementMessage()
    {
        yield return new WaitForSeconds(3f);
        achievementText.text = ""; // Limpar mensagem após 3 segundos
    }

}
