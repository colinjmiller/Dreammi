using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[SerializeField] private LayerMask groundLayers;
	[SerializeField] private float maxHorizontalSpeed = 8f;
	[SerializeField] private float jumpSpeed = 8f;
	[SerializeField] private Camera mainCamera;

	private Rigidbody2D rigidbody;
	private bool isGrounded;
	private FloorCollider floorCollider;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	private void Awake() {
		floorCollider = gameObject.GetComponentInChildren<FloorCollider> ();
		rigidbody = GetComponent<Rigidbody2D>();
		isGrounded = false;
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		float horizontalSpeed = rigidbody.velocity.x;
		float verticalSpeed = rigidbody.velocity.y;

		bool facingLeft = spriteRenderer.flipX;
		if (horizontalSpeed < 0 && !facingLeft) {
			spriteRenderer.flipX = true;
		} else if (horizontalSpeed > 0 && facingLeft) {
			spriteRenderer.flipX = false; 
		}

		animator.SetBool ("HorizontalMotion", (horizontalSpeed != 0));
		animator.SetBool ("VerticalMotion", (verticalSpeed != 0));

		fixCamera (transform.position);
	}

	void FixedUpdate () {
		isGrounded = floorCollider.isPlayerGrounded ();
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");

		bool isJumping = false;
		if (isGrounded && Input.GetButtonDown("Jump") && rigidbody.velocity.y <= 0) {
			isJumping = true;
		}

		float newHorizontalVelocity = horizontalInput * maxHorizontalSpeed;
		float newVerticalVelocity = rigidbody.velocity.y + (isJumping ? jumpSpeed : 0);

		rigidbody.velocity = new Vector2 (newHorizontalVelocity, newVerticalVelocity);
	}

	private void fixCamera(Vector3 location) {
		Vector3 newLocation = new Vector3 (location.x, location.y, -10);
		mainCamera.transform.position = newLocation;
	}
}
