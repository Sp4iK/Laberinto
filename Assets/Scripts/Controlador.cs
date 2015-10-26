using UnityEngine;
using System.Collections;

public class Controlador : MonoBehaviour {

	public int velocidadAvance = 1;
	public bool showMenu = false;
	public bool showToast = true;
	Animator anim;

	void Start () {
		//Time.timeScale = 0.0f;
		anim = GetComponentInChildren<Animator>();
	}

	void FixedUpdate () {
	
		int m_horiz = (int)Input.GetAxis ("Horizontal");
		int m_vert = (int)Input.GetAxis ("Vertical");

		if (m_horiz != 0 || m_vert != 0) {
			anim.SetInteger("movimiento", 1);
		} else {
			anim.SetInteger("movimiento", 0);
		}
		
		if (m_horiz != 0) {
			mov_horiz(m_horiz);
		}
		
		if (m_vert != 0) {
			mov_vert(m_vert);
		}

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu)) showMenu = !showMenu;
	}

	public void mov_horiz (int m_horiz) {
		transform.eulerAngles = new Vector3 (0, 90 * m_horiz, 0);
		transform.Translate (Vector3.forward * velocidadAvance * Time.deltaTime);
	}

	public void mov_vert (int m_vert) {
		if (m_vert == 1) {
			transform.eulerAngles = new Vector3(0,0,0);
		} else if (m_vert == -1) {
			transform.eulerAngles = new Vector3(0,180,0);
		}
		
		transform.Translate(Vector3.forward * velocidadAvance * Time.deltaTime);
	}

	void OnGUI () {
		GUI.skin.box.fontSize = GUI.skin.button.fontSize = 20; // Por defecto es 13

		if (showToast) {
			Time.timeScale = 0.0f;

			GUI.Box(new Rect(Screen.width/2-260, Screen.height/2-60,520,120),"Captura a todos los cocos para poder salir del laberinto.\nPara ello encuentra las pildoras que estan escondidas.");
			if (GUI.Button(new Rect(Screen.width/2-120,Screen.height/2,240,40), "OK")) {
				showToast = false;
				Time.timeScale = 1.0f;
			}
		}

		if (showMenu) {
			Time.timeScale = 0.0f;
			
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-65,200,130), "");

			if (GUI.Button(new Rect(Screen.width/2-80,Screen.height/2-45,160,40), "REINICIAR")) {
				Application.LoadLevel(0);
			}

			if (GUI.Button(new Rect(Screen.width/2-80,Screen.height/2+5,160,40), "SALIR")) {
				Application.Quit();
				return;
			}
		} else {
			Time.timeScale = 1.0f;
		}
	}
}
