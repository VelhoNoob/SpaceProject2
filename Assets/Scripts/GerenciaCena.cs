using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciaCena : MonoBehaviour
{
    // Start is called before the first frame update
    public void CarregueCena(int numeroCena)
    {
        SceneManager.LoadScene(numeroCena);
    }

}
