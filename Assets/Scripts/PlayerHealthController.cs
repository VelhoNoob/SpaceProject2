using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private Animator anim;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] public int vidas;


    [SerializeField] private float invincibilityLength;
    private float invincibilityCounter;

    private SpriteRenderer sr;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color fadeColor;

    //Variavel Posição Inicial
    [SerializeField] private Vector3 posInicial;

    private PlayerController player;

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
        //transform.position = posInicial;

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
            Morrer();
        }

        if (gatilho.gameObject.tag == "Checkpoint")
        {
            posInicial = gatilho.transform.position;
        }


    }

    public void DamagePlayer()
    {
        if (invincibilityCounter <= 0)
        {

            currentHealth--;

            //  Morre vida menor que 0
            if (currentHealth < 0)
            {
                anim.SetBool("isDeath", true); // Ativa a animação de morte                 
                Morrer();
            }
            // Ainda tem vida
            else
            {
                invincibilityCounter = invincibilityLength;

                sr.color = fadeColor;
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

            player.Knockback();


        }
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
                
    }

    void Inicializar()
    {
        
        //ponto inicial
        transform.position = posInicial;
        //recuperar HP
        currentHealth = 3;
        anim.SetBool("isDeath", false); // desativando a animação de morte 

    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(2);    }

    

}
