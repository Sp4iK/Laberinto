using UnityEngine;
using System.Collections;

public class Salida : MonoBehaviour {

	private string mensaje = null;
	private CazaEnemigos cazados;
	private Animator animator;

	void Start() {
		cazados = GameObject.Find("Comecocos").GetComponent<CazaEnemigos>();
		animator = GameObject.Find("salida").GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Player") {
			if(cazados.enemigos >= 3){
				mensaje = "¡Enhorabuena! Ya puedes salir";
				animator.SetBool("Open", true);
			} else {
				mensaje = "¡No has cazado a todos los cocos!";
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			mensaje = null;
			animator.SetBool("Open", false);
		}
	}

	void OnGUI () {
		if (mensaje != null) {
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 30, 400, 60), mensaje);
		}
	}
}
