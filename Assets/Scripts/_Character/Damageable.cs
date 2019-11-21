using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Damageable : MonoBehaviour
{
	[System.Serializable]
	public class HealthEvent : UnityEvent<Damageable>
	{ }
	[System.Serializable]
	public class DamageEvent : UnityEvent<Damageable>
	{ }
	[System.Serializable]
	public class HealEvent : UnityEvent<float, Damageable>
	{ }
	[SerializeField] private HealthEvent OnHealthSet = null;
	[SerializeField] private DamageEvent OnTakeDamage = null;
	[SerializeField] private HealEvent OnGainHealth = null;
	[SerializeField] private DamageEvent OnDie = null;
	[SerializeField] private bool disableOnDeath = false;
	[SerializeField] private bool destroyOnDeath = false;
	[SerializeField] private bool invulnerable = false;
	[HideInInspector] public float currentHealth = 120;
	[HideInInspector] public float maxHealth = 120;

	public void TakeDamage(float damage)
	{
		if (invulnerable || currentHealth <= 0)
			return;

		currentHealth -= damage;
		OnTakeDamage.Invoke(this);
		OnHealthSet.Invoke(this);

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			OnDie.Invoke(this);
			if (disableOnDeath)
				gameObject.SetActive(false);
			if (destroyOnDeath)
				Destroy(gameObject);
		}
	}
	public void GainHealth(float amount)
	{
		currentHealth += amount;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		OnHealthSet.Invoke(this);
		OnGainHealth.Invoke(amount, this);
	}
	public void SetHealth(int amount)
	{
		currentHealth = amount;
		OnHealthSet.Invoke(this);
	}

	public void gainInvulnerability()
	{
		invulnerable = true;
	}

	public void loseInvulnerability()
	{
		invulnerable = false;
	}

	public void toggleInvulnerability()
	{
		invulnerable = invulnerable ? false : true;
	}
}
