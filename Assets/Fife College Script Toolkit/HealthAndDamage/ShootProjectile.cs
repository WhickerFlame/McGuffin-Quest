// *********************************************************************************************************************
// File: ShootProjectile.cs
// Purpose: Fire a projectile based on button press
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
public class ShootProjectile : MonoBehaviour {
// *********************************************************************************************************************

	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Header("Primary")]
	[Tooltip("What projectile should be fired")]
	public GameObject m_projectilePrefab = null;
	[Tooltip("How much cooldown between firing?")]
	public float m_cooldownDuration = 1f;
	[Tooltip("Speed the projectile should be fired at")]
	public float m_projectileSpeed = 10;
	[Tooltip("Point from which the projectile should be fired")]
	public Transform m_firingPoint;
    [Tooltip("Button used to trigger attack")]
    public string m_attackButton;

    [Header("Effects")]
	[Tooltip("The sound that should be played on fire")]
	public AudioClip m_fireSound = null;
	[Tooltip("The animator that should be used for firing animations")]
	public Animator m_animator;
	[Tooltip("The name of the trigger parameter that should be used for firing effects")]
	public string m_fireTrigger;

	// Private variables
	private float m_cooldownEnd = 0;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Update is called once per frame
	void Update () 
	{
		// If we have clicked the mouse button and our attack is not on cooldown...
		if (Input.GetButtonDown(m_attackButton) && !IsOnCooldown())
		{
			// Determine where the mouse is in the game world
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Determine the direction of the mouse relative to our position
			Vector2 direction = (mouseWorldPosition - m_firingPoint.position);

            // "normalize" changes the vector to have a length of 1, while keeping the direction -
            //   meaning we can multiply it by a speed to get an initial velocity for our projectile.
            direction.Normalize();

            // Create a copy of our projectile
            GameObject newProjectile = Instantiate(m_projectilePrefab);

			// Set the projetile's starting position to our fire point
			newProjectile.transform.position = m_firingPoint.position;
            
            // Get the rigidbody attached to the projectile
            Rigidbody2D projectileBody = newProjectile.GetComponent<Rigidbody2D>();

            // Set our projectile's velocity based on the direction we want it to go
            // and our projectile speed variable
            projectileBody.velocity = direction * m_projectileSpeed;

            // Record when our cooldown should end.
            // This makes us unable to attack for m_cooldownDuration seconds
            m_cooldownEnd = Time.time + m_cooldownDuration;

			// If we have specified a sound to play...
			if (m_fireSound != null) {
				// Play the sound for dying at this locaiton
				AudioSource.PlayClipAtPoint(m_fireSound,transform.position);
			}

			// If we have supplied an animator and a trigger to use...
			if (m_animator != null && m_fireTrigger != "") {
				// Play death animation
				m_animator.SetTrigger(m_fireTrigger);
			}
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Public Functions
	// *****************************************************************************************************************
	// Is our attack on cooldown?
	public bool IsOnCooldown()
	{
		// true if the current time is less than the end time for the cooldown
		return Time.time < m_cooldownEnd;
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************