using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class Fondo : MonoBehaviour
{
    public UnityEngine.UI.Text textoPuntos;
    public UnityEngine.UI.Text textoVidas;
    public UnityEngine.UI.Text textoGameOver;
    public UnityEngine.UI.Text textoNumeroEnemigos;
    private int pointCounter = 0;
    private int lifeCounter = 3;
    private int numeroEnemigos = 19;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumarPuntos()
    {
        pointCounter += 10;

        textoPuntos.text = "Puntos: " + pointCounter;
    }

    public void RestarNumeroEnemigos()
    {
        numeroEnemigos--;
        textoNumeroEnemigos.text = "Enemigos: " + numeroEnemigos;

        if (numeroEnemigos == 0)
        {
            MostrarHasGanado();
        }
    }

    public void QuitarVidas()
    {
        lifeCounter--;

        textoVidas.text = "Vidas: " + lifeCounter;

        if (lifeCounter == 0)
        {
            Nave nave = FindObjectOfType<Nave>();
            nave.DestruirNave();

            MostrarGameOver();
        }
    }

    public void MostrarGameOver()
    {
        // Mostrar Game Over si jugador pierde todas sus vidas
        textoGameOver.text = "¡Game Over!\nHas perdido";
    }

    public void MostrarHasGanado()
    {
        // Mostrar texto indicando a jugador que ha ganado la partida
        textoGameOver.text = "¡Has ganado!\nEnemigos eliminados";
    }
    
}
