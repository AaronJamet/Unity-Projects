using UnityEngine;
using System.Collections;

// AARÓN JAMET ORGILÉS, 2DAM-U

public class Rotator : MonoBehaviour {

	// Propiedad pública para que sea accesible desde el editor
	// Rotación del game object (15 en eje x, 30 en eje y, 45 en eje z)
	public Vector3 giro = new Vector3(15, 30, 45);

	// Propiedad para manejar los colores de cada pick-up
	private Renderer rPickUp;
	// Atributo para signar el sonido al recoger los pickups
	[SerializeField] Transform sonidoPickUp;

    void Start()
    {
		// Preparamos el renderer de los pick-ups para cambiar su color
		rPickUp = GetComponent<Renderer>();
		// Damos comienza a la corutina que irá cambiando los colores de cada elemento por separado
		StartCoroutine(CambiarColor());

		// Hacemos que al iniciar juego se instancien unos valores de giro diferentes para cada pick-up,
		// los cuales se mantendrán durante la ejecución del programa
		giro = giro * Random.value*3;
    }

    // Before rendering each frame..
    void Update () 
	{
		// Rotate the game object that this script is attached to by 15 in the X axis,
		// 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
		// rather than per frame.
		transform.Rotate (giro * Time.deltaTime);
	}

	// Función para hacer que pick-ups cambien de color cada cierto tiempo de manera aleatoria
	IEnumerator CambiarColor()
    {
		float pausa = Random.Range(3.0f, 7.0f);
		// Pasamos valor aleatorio a función que indica el intervalo entre cada cambio de color
		yield return new WaitForSeconds(pausa);

		rPickUp.material.color = new Color(Random.value, Random.value, Random.value);
		StartCoroutine( CambiarColor() );
    }

	// Reproducir un sonido cuando conseguimos coger cada pickup
	// Esta función es llamada desde el script del player
	public void HacerSonidoRecoger()
    {
		GetComponent<AudioSource>().Play();
	}
}	