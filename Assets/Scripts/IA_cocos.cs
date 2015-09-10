using UnityEngine;
using System.Collections;

public class IA_cocos : MonoBehaviour {

	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
	public float viewDistance = 5f;						// If not using a trigger collider radius, set a sight distance.
	public Transform[] wayPoints;
	public bool debugInfo = true;
	bool playerInSight;
	bool huntMode;
	bool firstTime = true;
	Transform target;
	GameObject player;
	NavMeshAgent nav;
	int currentPoint;
	Color originalColor;
	Renderer enemyRenderer;

	void Start () {
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		enemyRenderer = GetComponent<Renderer>();
		originalColor = enemyRenderer.material.color;
		currentPoint = 0;
	}

	void Update () {
		Debug.DrawRay(transform.position, transform.forward * viewDistance);

		// Get 'huntMode' state from CazaEnemigos class so to set 'cocos' on alert mode
		huntMode = player.GetComponentInParent<CazaEnemigos>().huntMode;

		// If 'huntMode' is true set this 'coco' to alert, else continue with the IA
		if (huntMode) {
			// Check if this is the first time this check is done so to not be constantly changing nav.destination
			if (firstTime) {
				enemyRenderer.material.color = Color.blue;
				
				// Create a escape direction and add it to current position, then as a destination point
				Vector3 escapeDirection = transform.position - player.transform.position;
				nav.destination = transform.position + escapeDirection;
				firstTime = false;
			}
		} else {
			firstTime = true;
			enemyRenderer.material.color = originalColor;

			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = player.transform.position - transform.position;
			float distance = Vector3.Distance (player.transform.position, transform.position);
			float angle = Vector3.Angle (direction, transform.forward);

			// If the player has entered the trigger zone...
			if (distance <= 5f) {
					// By default the player is not in sight.
					playerInSight = false;

					// If the angle between forward and where the player is, is less than half the angle of view...
					if (angle < (fieldOfViewAngle * 0.5f)) {
							RaycastHit hit;

							// ... and if a raycast towards the player hits something...
							if (Physics.Raycast (transform.position, direction, out hit, viewDistance)) {
									// ... and if the raycast hits the player...
									if (hit.collider.gameObject == player.gameObject) {
											// ... the player is in sight.
											playerInSight = true;
											target = player.transform;
									}
							}
					}

					if (!playerInSight) {
						Patrol();
					}

					//nav.destination = wayPoints[currentPoint].position;
					nav.destination = target.position;
			} else {
				// The player leaves the trigger zone so it's not in sight.
				playerInSight = false;
				Patrol();
			}
		}
	}

	void Patrol () {
		if (nav.remainingDistance < nav.stoppingDistance) {
			if (currentPoint == wayPoints.Length - 1)
				currentPoint = 0;
			else
				currentPoint++;
			
			target = wayPoints [currentPoint];
		} else {
			target = wayPoints [currentPoint];
		}

		nav.destination = target.position;
	}

//	void OnTriggerEnter(Collider other){
//		
//		if (other.gameObject.tag == "Player" && !player.GetComponentInParent<Controlador>().huntMode) {
//			anim.SetBool("muerto", true);
//			//yield return new WaitForSeconds(2f);
//			Time.timeScale = 0.0f;
//			player.GetComponentInParent<Controlador>().showMenu = true;
//			Object.Destroy(other.gameObject);
//		}
//	}
	
	void OnGUI (){
		if (debugInfo){
			switch (this.name){
				case "Coco_rojo":
					GUI.Box (new Rect (10, Screen.height-20, 300, 25), this.name + " --> Target: " + target);
					break;
				case "Coco_naranja":
					GUI.Box (new Rect (10, Screen.height-40, 300, 25), this.name + " --> Target: " + target);
					break;
				case "Coco_verde":
					GUI.Box (new Rect (10, Screen.height-60, 300, 25), this.name + " --> Target: " + target);
					break;
			}
		}
	}
}
