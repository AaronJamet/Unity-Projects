using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] Transform prefabExplosionNave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destruimos disparos cuando lleguen al límite de la pantalla
        if (transform.position.x > 8)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Nave")
        {
            // Instanciamos la explosión (para luego poder destruirla cuando necesitemos)
            Transform explosion = Instantiate(prefabExplosionNave,
                other.transform.position, Quaternion.identity);

            // Destruimos nuestra nave después de 3 vidas
            Fondo fondo = FindObjectOfType<Fondo>();
            fondo.QuitarVidas();
            // Destruimos también la explosión después de 1.2 segundos (el parámetro del final)
            Destroy(explosion.gameObject, 1.2f);
            // Destruimos también el propio disparo
            Destroy(gameObject);
        }
    }
}
