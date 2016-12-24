using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	[SerializeField] private LayerMask groundLayers;
	[SerializeField] private float maxHorizontalSpeed = 8f;
	[SerializeField] private float jumpSpeed = 8f;
	[SerializeField] private Camera mainCamera;

	private Rigidbody2D rigidbody;
	private bool isGrounded;
	private bool isClimbing;
	private FloorCollider floorCollider;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	private void Awake() {
		floorCollider = gameObject.GetComponentInChildren<FloorCollider> ();
		rigidbody = GetComponent<Rigidbody2D>();
		isGrounded = false;
		isClimbing = false;
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
		float verticalInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		rigidbody.gravityScale = 1f;

		if (isClimbing) {
			climbingBehavior (verticalInput, horizontalInput);
		} else {
			defaultMovementBehavior (verticalInput, horizontalInput);
		}
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

	void climbingBehavior(float verticalInput, float horizontalInput) {
		rigidbody.gravityScale = 0f;
		rigidbody.velocity = new Vector2 (horizontalInput * 2f, verticalInput * 4f);
	}

	void defaultMovementBehavior(float verticalInput, float horizontalInput) {
		isGrounded = floorCollider.isPlayerGrounded ();
		bool isJumping = false;
		if (isGrounded && Input.GetButtonDown("Jump")) {
			isJumping = true;
		}

		float newHorizontalVelocity = horizontalInput * maxHorizontalSpeed;
		float newVerticalVelocity = Mathf.Min(rigidbody.velocity.y + (isJumping ? jumpSpeed : 0), jumpSpeed);

		rigidbody.velocity = new Vector2 (newHorizontalVelocity, newVerticalVelocity);
	}

	private void fixCamera(Vector3 location) {
		Vector3 newLocation = new Vector3 (location.x, location.y, -10);
		mainCamera.transform.position = newLocation;
	}
}
