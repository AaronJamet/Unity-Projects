using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

// AARÓN JAMET ORGILÉS, 2DAM-U

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed = 3.7f;
	public Text countText;
	public Text winText;
	// Variable pública para controlar la fuerza del salto de la bola
	public float fuerzaSalto = 5;

	// Variable privada para controlar de forma interna el tamaño de la bola cuando choque con paredes externas
	private Vector3 cambioEscala = new Vector3(-0.1f, -0.1f, -0.1f);

	// Atributos accesibles desde editor que nos permitan añadir las explosiones al chocar la bola del jugador
	[SerializeField] Transform explosionPared;
	[SerializeField] Transform explosionPickUp;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;

	// Creamos un miembro de tipo Renderer que nos permita acceder al color del material de la bola
	private Renderer r;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Asigna el componente Renderer a nuestra variable privada r
		r = GetComponent<Renderer>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
	}

    private void Update()
    {
        // Se aplica fuerza de salto en Update ya que es una fuerza de impulso y no gradual (como las de FixedUpdate)
		if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
			rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

			// Reproducir el sonido del salto de la bola
			GetComponent<AudioSource>().Play();
		}
    }

    // Each physics step..
    void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();

			// Hacer a la bola más grande cada vez que recoja uno de los pickups
			transform.localScale -= cambioEscala;

			// Instanciamos la explosión contra los pickups
			Transform explosion1 = Instantiate(explosionPickUp, other.transform.position,
										Quaternion.identity);
			// Destruimos el objeto de la explosión
			Destroy(explosion1.gameObject, 0.8f);

			// Llamamos al AudioSource del pickup para reproducir un sonido cuando lo cogemos
			Rotator pickup = FindObjectOfType<Rotator>();
			pickup.HacerSonidoRecoger();
		}
	}

    // Detecta la colisión del GameObject, esta vez al usar OnCollisionEnter teniendo en cuenta las consecuencias físicas
    // (y sin usar IsTrigger en las paredes porque si no las atravesariamos)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ParedNorte") || collision.gameObject.CompareTag("ParedSur") ||
			  (collision.gameObject.CompareTag("ParedEste") || collision.gameObject.CompareTag("ParedOeste")))
        {
			// Cambia el color de la bola cada vez que detecte una colisión contra una pared,
			// mediante un valor random para cada uno de los componentes r, g, v (red, green, yellow)
			r.material.color = new Color(Random.value, Random.value,
								Random.value);
		}

		// Reducir el tamaño de la bola si colisiona contra las paredes Norte o Sur, o aumentarlo si
		// choca contra las paredes Oeste/Este
		if (collision.gameObject.CompareTag("ParedNorte") || collision.gameObject.CompareTag("ParedSur"))
        {
			transform.localScale += cambioEscala; 
        }
		else if (collision.gameObject.CompareTag("ParedOeste") || collision.gameObject.CompareTag("ParedEste"))
		{
			transform.localScale -= cambioEscala;
        }

		// Controlamos aquí la explosión de la bola contra cualquiera de las paredes
		if (collision.gameObject.CompareTag("ParedNorte") || collision.gameObject.CompareTag("ParedSur") ||
			  (collision.gameObject.CompareTag("ParedEste") || collision.gameObject.CompareTag("ParedOeste") ||
			   collision.gameObject.CompareTag("Pared")))
		{
			// Instanciamos la explosión contra las paredes
			Transform explosion1 = Instantiate(explosionPared, this.transform.position,
										Quaternion.identity);
			// Destruimos el objeto de la explosión
			Destroy(explosion1.gameObject, 0.5f);
		}

	}

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}
}