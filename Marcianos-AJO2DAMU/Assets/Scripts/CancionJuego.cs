using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class CancionJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Reproducir canción entre diferentes escenas
    private static CancionJuego instance = null;
    public static CancionJuego Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
