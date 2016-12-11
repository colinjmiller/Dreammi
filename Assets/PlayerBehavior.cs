using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[SerializeField] private LayerMask groundLayers;
	[SerializeField] private float maxHorizontalSpeed = 8f;
	[SerializeField] private float jumpSpeed = 8f;

	private Rigidbody2D rigidbody;
	private bool isGrounded;
	private FloorCollider floorCollider;

	private void Awake() {
		floorCollider = gameObject.GetComponentInChildren<FloorCollider> ();
		rigidbody = GetComponent<Rigidbody2D>();
		isGrounded = false;
	}

	void FixedUpdate () {
		isGrounded = floorCollider.isPlayerGrounded ();
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");

		bool isJumping = false;
		if (isGrounded && Input.GetButtonDown("Jump") && rigidbody.velocity.y <= 0) {
			Debug.Log ("jump");
			isJumping = true;
		}

		rigidbody.velocity = new Vector2 (horizontalInput * maxHorizontalSpeed, rigidbody.velocity.y + (isJumping ? jumpSpeed : 0));
	}
}
