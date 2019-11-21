using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
	[SerializeField] private Damageable representedDamageable = null;
	[SerializeField] private Slider healthGauge = null;
    private void Start()
    {
		representedDamageable = GameObject.FindGameObjectWithTag("Patient").GetComponent<Damageable>();
		RefreshValues();
	}

	public void RefreshValues()
	{
		healthGauge.maxValue = representedDamageable.maxHealth;
		healthGauge.value = representedDamageable.currentHealth;
	}
}
