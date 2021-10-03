using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[Header("Movement")]
	public float speed = 3f;
	[Header("Attack")]
	[SerializeField] private float attackDamage = 10f;
	[SerializeField] private float attackSpeed = 1f;
	private float canAttack;


	[Header("Health")]
	private float health;
	[SerializeField] private float maxHealth;

	private Transform target;

	public Animator animator;

	private bool m_FacingRight = true;

	private void Start() {
		health = maxHealth;
	}

	public void TakeDamage(float dmg) {
		health -= dmg;
		Debug.Log("Enemy Health: " + health);

		if (health <= 0) {
			animator.SetBool("EnemyDead", true);
			Destroy(gameObject);
		}
	}

	private void FixedUpdate() {
		if (target != null) {
			float step = speed * Time.deltaTime;
			animator.SetBool("IsWalking", true);

			transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), step);
		} else {
			animator.SetBool("IsWalking", false);
		}
		if (transform.position.x - target.position.x > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (transform.position.x - target.position.x < 0 && m_FacingRight) 
			{
				Flip();
			}
		//Debug.Log(transform.position.x - target.position.x);

	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			if (attackSpeed <= canAttack) {
				other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
				canAttack = 0f;
			} else {
				canAttack += Time.deltaTime;
			}
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			if (attackSpeed <= canAttack) {
				other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
				canAttack = 0f;
			} else {
				canAttack += Time.deltaTime;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			target = other.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			target = null;
		}
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}