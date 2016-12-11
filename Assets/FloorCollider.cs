using UnityEngine;
using System.Collections;

public class FloorCollider : MonoBehaviour {

	private bool isGrounded;

	void Start() {
		isGrounded = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		isGrounded = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		isGrounded = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		isGrounded = false;
	}

	public bool isPlayerGrounded() {
		return isGrounded;
	}
}
