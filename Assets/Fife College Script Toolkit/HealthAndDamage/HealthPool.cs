// *********************************************************************************************************************
// File: HealthPool.cs
// Purpose: A health pool that can be damaged by atacks
// Project: Fife College Unity Toolkit
// Copyright Fife College 2018
// *********************************************************************************************************************


// *********************************************************************************************************************
#region Imports
// *********************************************************************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
// *********************************************************************************************************************


// *********************************************************************************************************************
public class HealthPool : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Header("Primary")]
	[Tooltip("How much health this object has to start with")]
	public float m_maxHealth = 100;
	[Tooltip("How long this object should be invulnerable after taking damage, in seconds")]
	public float m_invulnDuration = 0;
	[Tooltip("If true, this object will be destroyed when health reaches 0")]
	public bool m_destroyOnDeath = true;
	[Tooltip("The target type of the attack must match this type in order to deal damage")]
	public string m_targetType = "enemy";

	[Header("Effects")]
	[Tooltip("The sound that should be played when this object takes damage")]
	public AudioClip m_damageSound = null;
	[Tooltip("The sound that should be played when this object dies")]
	public AudioClip m_deathSound = null;
	[Tooltip("The animator that should be used for damage animations")]
	public Animator m_animator;
	[Tooltip("The name of the trigger parameter that should be used for damage effects")]
	public string m_damageTrigger;
	[Tooltip("The name of the trigger parameter that should be used for death effects")]
	public string m_deathTrigger;
	[Tooltip("Should this object blink while invincible?")]
	public bool m_blinkWhenInvincible;
	[Tooltip("How long should we wait between blinks?")]
	public float m_blinkDuration = 0.2f;

	// Private Variables
	private float m_invulnEnd = 0;
	private float m_blinkEnd = 0;
	private float m_health = 100;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called once when the object is created
	void Start() {
		// Set our health to match our max health
		m_health = m_maxHealth;
	}
	// *****************************************************************************************************************
	// Called every frame
	void Update() {
		HandleBlink();
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Public Functions
	// *****************************************************************************************************************
	// Damage the player - may cause death!
	public bool Damage(float _damage, string _targetType)
	{
		// Don't deal damage if we are invulnerable
		// (Check current time against what time it should be to
		//    stop being invulnerable)
		if (!IsInvulnerable() && _targetType == m_targetType)
		{
			// Reduce our health by the damage taken
			m_health = m_health - _damage;

			// If the health would become negative, just make it 0
			if (m_health < 0)
			{
				m_health = 0;
			}

			// Record when our invulnerability should end.
			// This makes us become invulnerable for m_invulnDuration seconds
			m_invulnEnd = Time.time + m_invulnDuration;

			// If we have specified a sound to play on damage...
			if (m_damageSound != null) {
				// Play the sound for damage at this locaiton
				AudioSource.PlayClipAtPoint(m_damageSound,transform.position);
			}

			// If we have supplied an animator and a trigger to use when damaged...
			if (m_animator != null && m_damageTrigger != "") {
				// Play death animation
				m_animator.SetTrigger(m_damageTrigger);
			}

			// If our health has dropped to 0 or lower, we are dead!
			if (m_health <= 0)
			{
				// If we have marked that we should destroy this object when it dies...
				if (m_destroyOnDeath)
				{
					// Destroy the object
					Destroy(gameObject);
				}

				// If we have specified a sound to play on death...
				if (m_deathSound != null) {
					// Play the sound for dying at this locaiton
					AudioSource.PlayClipAtPoint(m_deathSound,transform.position);
				}

				// If we have supplied an animator and a trigger to use on death...
				if (m_animator != null && m_deathTrigger != "") {
					// Play death animation
					m_animator.SetTrigger(m_deathTrigger);
				}
			}

			// We dealt damage, so return true
			return true;
		}

		// We did not deal damage, so return false
		return false;
	}
	// *****************************************************************************************************************
	public bool Heal(float _healing, string _targetType)
	{
		// the type matches...
		if (_targetType == m_targetType) {
			// add the health
			m_health = m_health + _healing;

			// we healed, so return true
			return true;
		}

		// we did not heal, return false
		return false;
	}
	// *****************************************************************************************************************
	public bool IsInvulnerable()
	{
		return Time.time < m_invulnEnd;
	}
	// *****************************************************************************************************************
	public float GetFractionHealth()
	{
		return m_health / m_maxHealth;
	}
	// *****************************************************************************************************************
	public bool IsAlive()
	{
		return m_health > 0;
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Private Functions
	// *****************************************************************************************************************
	private void HandleBlink()
	{
		// Get the sprite renderer component from the Player's game object
		// This is what we will disable and enable to make the blinking effect
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// If we should be invulnerable and this object has a sprite renderer on it ...
		if (spriteRenderer != null && IsInvulnerable())
		{
			// Check if it is time to turn off or on our sprite.
			if (Time.time >= m_blinkEnd)
			{
				// If it is...
				// Set our sprite renderers state to the opposite of
				//     what it currently is
				// In other words - toggle it.
				spriteRenderer.enabled = !spriteRenderer.enabled;

				// Record when we should next toggle our sprite renderer.
				m_blinkEnd = Time.time + m_blinkDuration;
			}
		}
		// If we should NOT be invulnerable...
		else
		{
			// Make sure our sprite is showing!
			spriteRenderer.enabled = true;
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************
}
// *********************************************************************************************************************
