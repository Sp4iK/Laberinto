using UnityEngine;
using System.Collections;

public class Fin : MonoBehaviour {

	private string mensaje = null;

	void OnTriggerEnter(Collider other){
		
		if(other.gameObject.tag == "Player"){
			mensaje = "¡FIN!";
			StartCoroutine("DelayedQuit");
		}
	}

	IEnumerator DelayedQuit() {
		Time.timeScale = 0.25f;
		yield return new WaitForSeconds(1);
		mensaje = null;
		GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Controlador>().showMenu = true;
	}

	void OnGUI () {
		if (mensaje != null) {
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
			GUI.Label (new Rect (Screen.width / 2 - 15, Screen.height / 2 - 12, 30, 24), mensaje);
		}
	}
}
