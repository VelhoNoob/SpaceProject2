using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    

    [SerializeField] private float invincibilityLength;
    private float invincibilityCounter;

    private SpriteRenderer sr;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color fadeColor;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;

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

    public void DamagePlayer()
    {
        if (invincibilityCounter <= 0)
        {

            currentHealth--;

            if (currentHealth < 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
            }

            else
            {
                invincibilityCounter = invincibilityLength;

                sr.color = fadeColor;
            }
            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

        }
    }
}
