using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class BotonJugar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LanzarJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }
}
