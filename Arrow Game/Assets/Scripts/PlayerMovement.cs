using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] Transform hand;

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	bool flip = false;

	void Update () {
		RotateHand();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}
	}

	public void OnLanding () {
		animator.SetBool("IsJumping", false);
	}

	public void OnFlip() {
		if(flip) {
			flip = false;
		} else {
			flip = true;
		}
	}

	void FixedUpdate ()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

	void RotateHand() {
		if (flip) {
			float angle = (Utility.AngleTowardsMouse(hand.position)+180f);
			hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		} else {
			float angle = Utility.AngleTowardsMouse(hand.position);
			hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		}
	}
}