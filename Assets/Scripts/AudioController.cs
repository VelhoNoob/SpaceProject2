using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    private AudioSource audioSource;

    [Header("Player")]
    [SerializeField] private AudioClip audioJump;
    [SerializeField] private AudioClip audioDubleJump;
    [SerializeField] private AudioClip audioDano;
    [SerializeField] private AudioClip audioMorte;
    [SerializeField] private AudioClip audioMorteBuraco;

    [Header("Inimigos")]
    [SerializeField] private AudioClip audioInimigo;
    [SerializeField] private AudioClip audioBat;
    [SerializeField] private AudioClip audioBoss;

    [Header("Outros")]
    [SerializeField] private AudioClip audioConquista;
    [SerializeField] private AudioClip audioCristais;
    [SerializeField] private AudioClip audioMunicao;    
    [SerializeField] private AudioClip audioCheckpoint;
    [SerializeField] private AudioClip audioWin;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();              
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    //PLAYER
    public void JumpPlayer()
    {
        audioSource.PlayOneShot(audioJump);
    }

    public void JumpDublePlayer()
    {
        audioSource.PlayOneShot(audioDubleJump);
    }

    public void DanoPlayer()
    {
        audioSource.PlayOneShot(audioDano);
    }

    public void MortePlayer()
    {
        audioSource.PlayOneShot(audioMorte);
    }

    public void MorteBuraco()
    {
        audioSource.PlayOneShot(audioMorteBuraco);
    }

    //INIMIGOS
    public void AudioInimigo()
    {
        audioSource.PlayOneShot(audioInimigo);
    }

    public void AudioBat()
    {
        audioSource.PlayOneShot(audioBat);
    }

    public void AudioBoss()
    {
        audioSource.PlayOneShot(audioBoss);
    }

    //OUTROS
    public void AudioConquista()
    {
        audioSource.PlayOneShot(audioConquista);
    }

    public void AudioCristais()
    {
        audioSource.PlayOneShot(audioCristais);
    }

    public void AudioMunicao()
    {
        audioSource.PlayOneShot(audioMunicao);
    }  
    
    public void AudioCheckpoint()
    {        
        audioSource.PlayOneShot(audioCheckpoint);
    }

    public void AudioWin()
    {
        audioSource.PlayOneShot(audioWin);
    }
}
