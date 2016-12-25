using UnityEngine;
using System.Collections;

public class WaterCollider : MonoBehaviour {

	private bool isSwimming;

	void Start() {
		isSwimming = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Water") {
			isSwimming = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Water") {
			isSwimming = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Water") {
			isSwimming = false;
		}
	}

	public bool isPlayerSwimming() {
		return isSwimming;
	}
}
