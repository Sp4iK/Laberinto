using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

	Controlador controlador;
	Animator anim;
	int boton;

	void Start (){
		if (Application.platform != RuntimePlatform.Android){
			gameObject.SetActive(false);
		}
		
		controlador = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Controlador>();
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
	}
	
	public void pulsado (int botonPulsado) {

		boton = botonPulsado;
	}

	void Update () {

		if (boton!=0){
			anim.SetInteger("movimiento", 1);

			switch(boton) {
				case 1:
					controlador.mov_vert(1);
					break;
				case 2:
					controlador.mov_vert(-1);
					break;
				case 3:
					controlador.mov_horiz(1);
					break;
				case 4:
					controlador.mov_horiz(-1);
					break;
			}
		} else {
			anim.SetInteger("movimiento", 0);
		}
	}
}
