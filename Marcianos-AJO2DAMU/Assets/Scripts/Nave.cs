using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AARÓN JAMET ORGILÉS - 2ºDAM-U

public class Nave : MonoBehaviour
{
    // Velocidad de la nave
    [SerializeField] float velocidad = 6;
    [SerializeField] Transform prefabDisparo;
    private float velocidadDisparo = -2.5f;
    [SerializeField] Transform prefabExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se establece una dirección vertical y otra horizontal como movimiento de la nave
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Aquí se limitan los movimientos de nuestra nave, para que NO salga por los lados de la pantalla
        if (transform.position.y > 3.50)
        {
            if (vertical < 0)
            {
                transform.Translate(0, vertical * velocidad * Time.deltaTime, 0);
            }
        } else if (transform.position.y < -3.50)
        {
            if (vertical > 0)
            {
                transform.Translate(0, vertical * velocidad * Time.deltaTime, 0);

            }
        } else
            transform.Translate(0, vertical * velocidad * Time.deltaTime, 0);


        // Limitamos ahora las coordenadas horizontales en las que puede moverse la nave
        if (transform.position.x > 7.20)
        {
            if (horizontal < 0)
            {
                transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
            }
        }
        else if (transform.position.x < -7.20)
        {
            if (horizontal > 0)
            {
                transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

            }
        }
        else
            transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

        // Asociamos el disparo a la nave y lo activamos al pulsar tecla disparar
        if (Input.GetButtonDown("Fire1"))
        {
            Transform disparo = Instantiate(prefabDisparo, transform.position, Quaternion.identity);
            disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector3(velocidadDisparo, 0, 0);

            // Reproducir un sonido al realizar un disparo del jugador
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Perder vidas en nave de jugador al colisionar con naves enemigas
        if (other.tag == "Enemigo")
        {
            // Destruimos la nave del enemigo golpeada
            Destroy(other.gameObject);
            // Instanciamos la explosión (para luego poder destruirla cuando necesitemos)
            Transform explosion = Instantiate(prefabExplosion,
                other.transform.position, Quaternion.identity);
            // Destruimos también la explosión después de 1.2 segundos (el parámetro del final)
            Destroy(explosion.gameObject, 1.2f);

            Fondo fondo = FindObjectOfType<Fondo>();
            fondo.QuitarVidas();
        }
    }

    public void DestruirNave()
    {
        // Destruimos nuestra propia nave
        Destroy(gameObject);
        // Instanciamos la explosión (para luego poder destruirla cuando necesitemos)
        Transform explosion = Instantiate(prefabExplosion,
            gameObject.transform.position, Quaternion.identity);
        // Destruimos también la explosión después de 1.2 segundos (el parámetro del final)
        Destroy(explosion.gameObject, 1.2f);

        Fondo fondo = FindObjectOfType<Fondo>();
        fondo.MostrarGameOver();
    }
}
