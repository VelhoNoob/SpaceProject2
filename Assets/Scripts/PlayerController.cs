using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform tr;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private GameObject boss;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float activeSpeed;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool isCrouch;
    private bool isKnockingback;
    public bool isOnPlatform { get; private set; }

    [Header("Groud Info")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlatform; // Novo LayerMask para plataformas
    [SerializeField] private LayerMask whatIsEnemy; // Novo LayerMask para enemy
    [SerializeField] private bool isGounded;

    [Header("cristais")]
    public int cristais = 0;
    private Text CristaisTexto;

    [Header("Tiro")]
    [SerializeField] public GameObject Bala;
    [SerializeField] private float tempoEsperaTiro;
    [SerializeField] private float velocidadeDisparo;
    [SerializeField] private float duraçãoBala;
    private float meuTempoTiro = 0;
    [SerializeField] private bool pode_atirar = true;

    [Header("Munição")]
    [SerializeField] public int municao;
    public Text MunicaoTexto;
         

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();


        CristaisTexto = GameObject.FindGameObjectWithTag("TextoCristalTag").GetComponent<Text>();
        MunicaoTexto = GameObject.FindGameObjectWithTag("TextoMunicaoTag").GetComponent<Text>();

        //INICIANDO CONTADOR DE MUNIÇÃO
        MunicaoTexto.text = municao.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se o Player está em contato com o solo ou com uma plataforma
        isGounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround) ||
                 Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsPlatform) ||
                 Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsEnemy);                    


        // MOVIMENTO
        var xInput = Input.GetAxisRaw("Horizontal");

        if (isCrouch)
        {
            xInput = 0; // Impede o movimento horizontal enquanto agachado
        }

        activeSpeed = moveSpeed;


        // CORRER
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouch)
        {
            activeSpeed = runSpeed;
            anim.SetBool("Correndo", true);
        }
        else
        {
            anim.SetBool("Correndo", false);
        }

        rb.velocity = new Vector2(xInput * activeSpeed, rb.velocity.y);
                

        //PULAR
        if (Input.GetButtonDown("Jump") &&!isCrouch)
        {
            if (isGounded)
            {
                Jump();
                canDoubleJump = true;
                AudioController.instance.JumpPlayer();

            }
            else
            {
                if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
                    anim.SetTrigger("isDoubleJump");
                    AudioController.instance.JumpDublePlayer();
                    Invoke(nameof(DesativarAnimacaoPuloDuplo), 0.2f); // Intervalo para desativar animação
                }
            }
        }

        if (isGounded)
        {
            canDoubleJump = true;
            anim.SetBool("isDoubleJump", false);
        }

        //Configurando dire��es
        if (rb.velocity.x > 0)
        {
            //transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            sr.flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            //transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            sr.flipX = true;
        }

        //AGACHAR
        Agachar();

        //ATIRAR
        Atirar();

        //Chamando anima��es
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGounded);
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("Crouch", isCrouch);

    }
    //AGACHAR
    private void Agachar()
    {
        
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            isCrouch = true;
            bc.offset = new Vector2(0.09651661f, -0.9962413f); // Define o offset para o estado de agachado
            bc.size = new Vector2(1.380758f, 2.243932f); // Ajuste de altura do BoxCollider para agachado
                                                         // ativando animação
        }
        else
        {
            isCrouch = false;
            bc.offset = new Vector2(-0.09158087f, -0.6695511f); // Define o offset para o estado normal
            bc.size = new Vector2(1.004563f, 2.897313f); // Altura normal do BoxCollider
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void DesativarAnimacaoPuloDuplo()
    {
        anim.SetBool("isDoubleJump", false); // Desativa a animação
    }

    //COLISÕES
    // ENTRANDO EM COLISÃO
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // COLISÃO Plataforma
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);

            isOnPlatform = true;
        }

        // COLISÃO INIMIGO
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        // COLISÃO BAT
        if (collision.gameObject.CompareTag("Bat"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }


        // COLISÃO BOSS
        if (collision.gameObject.CompareTag("Boss"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
    // SAINDO DA COLISÃO
    private void OnCollisionExit2D(Collision2D collision)
    {
        // COLISÃO PLATAFORMA
        if (collision.gameObject.CompareTag("Platform"))
        {
            Invoke("DetachFromPlatform", 0.1f); // Atraso antes de remover o jogador como filho da plataforma
            isOnPlatform = false;
        }

    }

    //Transforma o Player em parente da Plataforma
    private void DetachFromPlatform()
    {
        transform.SetParent(null);

    }

    //COLIÇÃO COLETA
    void OnTriggerEnter2D(Collider2D gatilho)
    {
        //CRISTAIS
        if (gatilho.gameObject.tag == "Cristais")
        {
            Destroy(gatilho.gameObject);
            cristais++;
            // Atualizar UI
            CristaisTexto.text = cristais.ToString();
            // Notificar o sistema de conquistas
            AchievementSystem.instance.CristalCollected();
            // Cahamando audio 
            AudioController.instance.AudioCristais();
        }
        //MUNIÇÃO
        if (gatilho.gameObject.tag == "Munição")
        {
            Destroy(gatilho.gameObject);
            municao += 5;
            // Atualizar UI
            MunicaoTexto.text = municao.ToString();
            // Cahamando audio 
            AudioController.instance.AudioMunicao();
        }
        //END GAME
        if (gatilho.gameObject.tag == "EndGame")
        {
            // Verifica se o Boss ainda existe na cena
            boss = GameObject.FindGameObjectWithTag("Boss");

            if (boss == null)
            {                
                // O Boss foi derrotado, pode ativar o efeito do EndGame
                SceneManager.LoadScene(5); // Carrega a próxima cena ou efeito desejado
                
            }

        }

    }

    //TIRO
    void Atirar()
    {
        if (pode_atirar == true)
        {
            if (isCrouch) return;

            //Atirando
            if (Input.GetMouseButtonDown(0))
            {
                //Controle de munição
                if (municao > 0)
                {
                    municao--;
                    //CONTADOR DE MUNIÇÃO
                    MunicaoTexto.text = municao.ToString();
                    Disparo();
                    anim.SetBool("Atirando", true); // Ativa a animação                
                    Invoke(nameof(DesativarAnimacaoAtirando), 0.2f); // Intervalo para desativar animação
                }
            }
        }
        else
        {
            TemporizadorTiro();
        }

    }

    void DesativarAnimacaoAtirando()
    {
        anim.SetBool("Atirando", false); // Desativa a animação
    }

    void Disparo()
    {

        if (sr.flipX == false)
        {
            //direção ---->
            //posição que a bala sai
            Vector3 pontoDisparo = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
            GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
            BalaDisparada.GetComponent<ControllerTiro>().DirecaoBala(velocidadeDisparo);
            //destruir bala
            Destroy(BalaDisparada, duraçãoBala);
        }

        if (sr.flipX == true)
        {
            //direção <----
            //posição que a bala sai
            Vector3 pontoDisparo = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
            GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
            BalaDisparada.GetComponent<ControllerTiro>().DirecaoBala(-velocidadeDisparo);
            //destruir bala
            Destroy(BalaDisparada, duraçãoBala);
        }

        pode_atirar = false;

    }

    //INTERVALO ENTRE TIROS
    void TemporizadorTiro()
    {
        meuTempoTiro += Time.deltaTime;
        if (meuTempoTiro > tempoEsperaTiro)
        {
            pode_atirar = true;
            meuTempoTiro = 0;
        }
    }

    //KNOCKBACK
    
    public void Knockback()
    {
        isKnockingback = true;

        Vector2 knockbackDir = Vector2.zero;
        float knockbakJump = jumpForce * .5f;

        // Adicione aqui um impeditivo para que o player não mova sua posição se estiver em colição com a pare
                
            if (rb.velocity.x > 0)
            {
                knockbackDir = new Vector2(rb.velocity.x * -.5f, knockbakJump);
            }

            if (rb.velocity.x < 0)
            {
                knockbackDir = new Vector2(rb.velocity.x * -.5f, knockbakJump);
            }      
             

        rb.velocity = knockbackDir;
        anim.SetTrigger("isKnockback");

        StartCoroutine(EndKnockback()); // chama a pausa da corrotina

    }   
    

    IEnumerator EndKnockback()
    {
        yield return new WaitForSeconds(.2f); // pausa a rotina por 2 segundos
        isKnockingback = false;
    }
    
}
