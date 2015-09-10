using UnityEngine;
using System.Collections;

public class ControladorRV : MonoBehaviour {

	public int velocidadAvance = 1;
	public bool showMenu = false;
	Animator anim;
	CharacterController controller;

	private float sensitivityX = 15F;
	private float sensitivityY = 15F;
	private float minimumX = -360F;
	private float maximumX = 360F;
	private float minimumY = -90F;
	private float maximumY = 90F;
	
	float rotationY = 0F;

	void Start () {
		Time.timeScale = 1.0f;
		anim = GetComponentInChildren<Animator>();
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate () {
	
		int m_vert = (int)Input.GetAxis ("Vertical");

		if (m_vert != 0) {
			anim.SetInteger("movimiento", 1);
		} else {
			anim.SetInteger("movimiento", 0);
		}
				
		if (m_vert!=0) {
			mov_vert (m_vert);
		}

		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu)) { showMenu = !showMenu; }
	}

	public void mov_vert(int m_vert){		
		transform.Translate(Vector3.forward*m_vert*velocidadAvance*Time.deltaTime);
		//controller.SimpleMove(Vector3.forward*m_vert*velocidadAvance*Time.deltaTime);
	}

	void OnGUI () {
		if (showMenu) {
			Time.timeScale = 0.0f;
			
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-65,200,130), "");
			if(GUI.Button(new Rect(Screen.width/2-80,Screen.height/2-45,160,40), "Reiniciar")) {
				Application.LoadLevel(0);
			}
			if(GUI.Button(new Rect(Screen.width/2-80,Screen.height/2+5,160,40), "Salir")) {
				Application.Quit();
				return;
			}
		} else {Time.timeScale = 1.0f;}
	}
}
