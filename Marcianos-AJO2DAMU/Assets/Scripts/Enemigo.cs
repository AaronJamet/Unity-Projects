using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class Enemigo : MonoBehaviour
{
    [SerializeField] float velocidadY = 2.1f;
    // Variable que determina distancia a la que deben ir acercándose los enemigos a nuestra nave
    private Vector3 pasoEnemigo = new Vector3(0.7f, 0, 0);
    [SerializeField] Transform prefabDisparoEnemigo;
    private float velocidadDisparoEnemigo = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( Disparar() );
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, velocidadY * Time.deltaTime, 0);
        // Impedir que salgan del borde y movimiento vertical constante
        if ((transform.position.y < -3.8) || (transform.position.y > 3.8))
        {
            velocidadY = -velocidadY;
            // Enemigos se acercan un poco a nave de jugador cada vez que tocan la pared
            // transform.Translate(40 * Time.deltaTime, 0, 0);
            DarUnPaso();
        }
            
        // Destruir si llegan al final de pantalla
        if (transform.position.x > 7.10)
        {
            Destroy(gameObject);

            Fondo fondo = FindObjectOfType<Fondo>();
            fondo.RestarNumeroEnemigos();
        }


    }

    void DarUnPaso()
    { 
        transform.Translate(pasoEnemigo);
    }

    // Hacer que los enemigos realicen disparos de manera aleatoria
    IEnumerator Disparar()
    {
        float pause = Random.Range(5.0f, 11.0f);
        // Pasamos el numero aleatorio calculado a la función que indica el intervalo entre cada disparo
        yield return new WaitForSeconds(pause);

        Transform disparo = Instantiate(prefabDisparoEnemigo, transform.position, Quaternion.identity);
        disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector3(velocidadDisparoEnemigo, 0, 0);

        StartCoroutine( Disparar() );
        // Reproducir un sonido al realizar un disparo del enemigo
        GetComponent<AudioSource>().Play();
    }
}
