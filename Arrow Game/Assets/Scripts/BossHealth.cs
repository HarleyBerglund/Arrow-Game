using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Attack")]
	[SerializeField] private float attackDamage = 10f;
	[SerializeField] private float attackSpeed = 1f;
	private float canAttack;


	[Header("Health")]
	private float health;
	[SerializeField] private float maxHealth;

    [SerializeField] private Slider bossHealthSlider;

    private void Start() {
		health = maxHealth;
        bossHealthSlider.maxValue = maxHealth;
	}

	public void TakeDamage(float dmg) {
		health -= dmg;

        Debug.Log("Enemy Health: " + health);

        if (health > maxHealth) {
            health = maxHealth;
        } else if (health <= 0) {
            bossHealthSlider.value = health;
			BossDied();
		}
	}


	private void BossDied() {
        Destroy(gameObject);
		LevelManager.instance.GameWin();
		gameObject.SetActive(false);
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


    private void OnGUI() {
        float t = Time.deltaTime / 1f;
        bossHealthSlider.value = Mathf.Lerp(bossHealthSlider.value, health, t);
    }
}
