using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] Transform prefabExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8)
            Destroy(gameObject);
    }

    // Controlamos la colisión entre nuestro disparo y las naves enemigas para poder destruirlas
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            // Instanciamos la explosión (para luego poder destruirla cuando necesitemos)
            Transform explosion = Instantiate(prefabExplosion,
                other.transform.position, Quaternion.identity);

            // Destruimos la nave del enemigo golpeada
            Destroy(other.gameObject);
            // Destruimos también la explosión después de 1.2 segundos (el parámetro del final)
            Destroy(explosion.gameObject, 1.2f);
            // Destruimos también el propio disparo
            Destroy(gameObject);

            // Llamamos al objeto Fondo, y a su método para sumar y mostrar puntos en la pantalla de juego
            Fondo fondo = FindObjectOfType<Fondo>();
            fondo.SumarPuntos();
            fondo.RestarNumeroEnemigos();
        }
    }
}
