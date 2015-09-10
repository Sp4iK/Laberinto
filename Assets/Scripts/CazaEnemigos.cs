using UnityEngine;
using System.Collections;

public class CazaEnemigos : MonoBehaviour {

	public int enemigos = 0;
	public float timer = 0f;
	public bool huntMode;
	float blinkTime = 0f;
	Light luz;
	Animator anim;
	GameObject player;

	void Start () {
		anim = GetComponentInChildren<Animator>();
		luz = GameObject.Find("Luz_salida").GetComponent<Light>();
		player = GameObject.FindGameObjectWithTag("Player");
		huntMode = false;
	}

	void Update () {
		timer -= Time.deltaTime;

		if (timer > 0f && timer <= 3f) {
			parpadeo ();
		} else if (timer <= 0f) {
			huntMode = false;
			player.GetComponent<Renderer>().material.color = Color.white;
		}

		if (enemigos >= 3) {luz.color = Color.green;}
	}

	IEnumerator OnTriggerEnter (Collider col) {
		
		if (col.gameObject.tag == "Enemy" && huntMode == true) {
			anim.SetBool("pillado", true);
			Object.Destroy(col.gameObject);
			enemigos++;
			yield return new WaitForSeconds(0.5f);
			anim.SetBool("pillado", false);
		} else if (col.gameObject.tag == "Enemy" && !huntMode) {
			anim.Play("Die");
			yield return new WaitForSeconds(2f);
			this.gameObject.GetComponent<Controlador>().showMenu = true;
		} else if (col.gameObject.tag == "pildora") {
			anim.SetBool("pillado", true);
			Object.Destroy(col.gameObject);
			yield return new WaitForSeconds(0.5f);
			anim.SetBool("pillado", false);
			huntMode = true;
			timer = 10f;
			player.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	void parpadeo () {
		blinkTime += Time.deltaTime;

		if(blinkTime < 0.25f){player.GetComponent<Renderer>().material.color = Color.white;}
		if(blinkTime > 0.25f){player.GetComponent<Renderer>().material.color = Color.red;}
		if(blinkTime > 0.5f){blinkTime = 0f;}
	}

	void OnGUI () {
		GUI.Box (new Rect (10f, 10f, 115f, 30f), "COCOS: " + enemigos.ToString ());
	}
}
