using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChamarCena()
    {
        SceneManager.LoadScene(1); // numero da cena adicionada na build, configurações da unit
    }

    public void ChamarFase(int numerofase);
    {
        //SceneManager.LoadScene(numerofase);
    }
       
}
