// *********************************************************************************************************************
// File: DamageOnTouch.cs
// Purpose: Deals damage to a health pool when this collider intersects with health pool
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
[RequireComponent(typeof(Collider2D))]
public class DamageOnTouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("How much damage this object should do")]
	public float m_damage = 10;
	[Tooltip("The target type of the health pool must match this type in order to deal damage")]
	public string m_targetType = "player";
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// When a trigger interaction starts involving this game object...
	void OnTriggerEnter2D(Collider2D _other)
	{
		// Check if the other collider that we hit has a HealthPool on it
		HealthPool healthPool = _other.GetComponent<HealthPool>();
		if (healthPool != null) {
			// Apply damage to the health pool
			healthPool.Damage(m_damage, m_targetType);
		}
	}
	// *****************************************************************************************************************
    // When a collision interaction starts involving this game object...
    void OnCollisionEnter2D(Collision2D _collision)
    {
        // Check if the other collider that we hit has a HealthPool on it
        HealthPool healthPool = _collision.collider.GetComponent<HealthPool>();
        if (healthPool != null)
        {
            // Apply damage to the health pool
            healthPool.Damage(m_damage, m_targetType);
        }
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************


}
// *********************************************************************************************************************
