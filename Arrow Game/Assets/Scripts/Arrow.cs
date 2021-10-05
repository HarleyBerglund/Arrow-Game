using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    [HideInInspector] public float ArrowVelocity;
	[HideInInspector] public float ArrowDamage;
	[SerializeField] Rigidbody2D rb;


	private void Start() {
		Destroy(gameObject, 4f);
	}

	private void FixedUpdate() {
		rb.velocity = transform.up * ArrowVelocity;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			enemy.TakeDamage(ArrowDamage);
		} 

		if (other.gameObject.tag == "Boss") {
			BossHealth boss = other.gameObject.GetComponent<BossHealth>();
			boss.TakeDamage(ArrowDamage);
		} 

		if (other.gameObject.tag == "Other") {
			GoToBoss level = other.gameObject.GetComponent<GoToBoss>();
			string nextLevel = "Boss";
			level.changeScene(nextLevel);
		}
		Destroy(gameObject);		
	}
}