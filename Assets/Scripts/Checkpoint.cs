using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isActive)
        {
            anim.SetBool("isFlagActive", true);
            isActive = true;
            AudioController.instance.AudioCheckpoint();
        }

    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool("isFlagActive", false);
        isActive = false;
    }

}
