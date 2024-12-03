using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    private SpriteRenderer sr;
    private PlayerController player;
    private Animator anim;   
   

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] public int vidas;
    private bool isDying = false;


    [SerializeField] private float invincibilityLength;
    private float invincibilityCounter;

    
    [SerializeField] private Color normalColor;
    [SerializeField] private Color fadeColor;

    //Variavel Posição Inicial
    [SerializeField] private Vector3 posInicial;

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();               
        player = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();        

        currentHealth = maxHealth;

        //Determinando posição inicial no começo do jogo
        posInicial = new Vector3(-11.53f, 0, transform.position.z);
        transform.position = posInicial;

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
                
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
            {
                sr.color = normalColor;
            }

        }             

    }

    void OnTriggerEnter2D(Collider2D gatilho)
    {       
        //Morte instatanea
        if (gatilho.gameObject.tag == "MorteImediata")
        {
            AudioController.instance.MorteBuraco();
            Morrer();
        }

        if (gatilho.gameObject.tag == "Checkpoint")
        {
            posInicial = gatilho.transform.position;
        }

        /*
        if (gatilho.gameObject.tag == "Dano")
        {
            DamagePlayer();
        }
        */

    }

    public void DamagePlayer()
    {
        if (invincibilityCounter <= 0 && !isDying)
        {

            currentHealth--;
            AudioController.instance.DanoPlayer();
            player.Knockback();

            //  Morre vida menor que 0
            if (currentHealth < 0)
            {
                isDying = true;
                anim.SetBool("isDeath", true); // Ativa a animação de morte                
                AudioController.instance.MortePlayer();
                StartCoroutine(IsDeath());              

            }
            // Ainda tem vida
            else
            {
                invincibilityCounter = invincibilityLength;

                sr.color = fadeColor;
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);                   

        }
    }
    
    IEnumerator IsDeath()
    {
        yield return new WaitForSeconds(1.5f); // pausa a rotina por X segundos
        anim.SetBool("isDeath", false);
        Morrer();
        isDying = false;
    }


    public void Morrer()
    {
        vidas--;
        
        if (vidas < 0)
        {
            
            Reiniciar();
            

        }
        else
        {
            
            Inicializar();
        }

        UIController.instance.UpdateVidasDisplay();        
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

    }

    void Inicializar()
    {
        
        //ponto inicial
        transform.position = posInicial;
        //recuperar HP
        currentHealth = 3;

        player.municao = 15;
        player.MunicaoTexto.text = player.municao.ToString();

    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(2);    
    }
    

}
