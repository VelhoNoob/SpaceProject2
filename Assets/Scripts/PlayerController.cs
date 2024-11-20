using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform tr;
    private BoxCollider2D bc;
    private SpriteRenderer sr;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float activeSpeed;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool isCrouch;

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
    [SerializeField] private float DuraçãoBala;
    private float meuTempoTiro = 0;
    [SerializeField] private bool pode_atirar = true;

    [Header("Munição")]
    [SerializeField] private int municao;
    private Text MunicaoTexto;

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

        activeSpeed = moveSpeed;

        // CORRER
        if (Input.GetKey(KeyCode.LeftControl))
            activeSpeed = runSpeed;

        rb.velocity = new Vector2(xInput * activeSpeed, rb.velocity.y);

        //PULAR
        if (Input.GetButtonDown("Jump"))
        {
            if (isGounded)
            {
                Jump();
                canDoubleJump = true;

            }
            else
            {
                if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
                    anim.SetTrigger("isDoubleJump");
                }
            }
        }

        if (isGounded)
        {
            canDoubleJump = true;
            anim.SetBool("isDoubleJump", false);
        }

        //AGACHAR
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            isCrouch = true;
            bc.offset = new Vector2(0.09651661f, -0.9962413f); // Define o offset para o estado de agachado
            bc.size = new Vector2(1.380758f, 2.243932f); // Ajuste de altura do BoxCollider para agachado
        }
        else
        {
            isCrouch = false;
            bc.offset = new Vector2(-0.09158087f, -0.6695511f); // Define o offset para o estado normal
            bc.size = new Vector2(1.004563f, 2.897313f); // Altura normal do BoxCollider
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


        //Chamando anima��es
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGounded);
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("Crouch", isCrouch);



        //ATIRAR
        Atirar();



    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // PLATAFORMA
    public bool isOnPlatform { get; private set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // COLISÃO PLATAFORMA
        if (collision.gameObject.CompareTag("Platform"))
        {
            Invoke("DetachFromPlatform", 0.1f); // Atraso antes de remover o jogador como filho da plataforma
            isOnPlatform = false;
        }

    }


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
            CristaisTexto.text = cristais.ToString();
        }
        //MUNIÇÃO
        if (gatilho.gameObject.tag == "Munição")
        {
            Destroy(gatilho.gameObject);
            municao += 5;
            MunicaoTexto.text = municao.ToString();
        }
    }





    //TIRO
    void Atirar()
    {
        if (pode_atirar == true)
        {


            //Atirando
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Controle de munição
                if (municao > 0)
                {
                    municao--;
                    //CONTADOR DE MUNIÇÃO
                    MunicaoTexto.text = municao.ToString();
                    Disparo();
                    anim.SetBool("Atirando", true); // Ativa a animação                
                    Invoke(nameof(DesativarAnimacaoAtirando), 0.1f); // Desativa após 1 segundo
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
            Destroy(BalaDisparada, DuraçãoBala);
        }

        if (sr.flipX == true)
        {
            //direção <----
            //posição que a bala sai
            Vector3 pontoDisparo = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
            GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
            BalaDisparada.GetComponent<ControllerTiro>().DirecaoBala(-velocidadeDisparo);
            //destruir bala
            Destroy(BalaDisparada, DuraçãoBala);
        }

        pode_atirar = false;

    }

    void TemporizadorTiro()
    {
        meuTempoTiro += Time.deltaTime;
        if (meuTempoTiro > tempoEsperaTiro)
        {
            pode_atirar = true;
            meuTempoTiro = 0;
        }
    }


}
