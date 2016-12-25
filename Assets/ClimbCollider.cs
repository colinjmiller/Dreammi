using UnityEngine;
using System.Collections;

public class ClimbCollider : MonoBehaviour {

	private bool isClimbing;

	void Start() {
		isClimbing = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ladder") {
			isClimbing = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Ladder") {
			isClimbing = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Ladder") {
			isClimbing = false;
		}
	}

	public bool isPlayerClimbing() {
		return isClimbing;
	}
}
