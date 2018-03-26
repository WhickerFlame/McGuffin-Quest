// *********************************************************************************************************************
// File: AddHealthOnTouch.cs
// Purpose: Adds health to a health pool on touch
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
public class AddHealthOnTouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("How much should this heal?")]
	public float m_healing = 10;
	[Tooltip("The target type of the health pool must match this type in order to heal")]
	public string m_targetType = "player";
	[Tooltip("Should we destroy this object after collecting it?")]
	public bool m_destroyOnCollect = true;
	[Tooltip("The sound that should be played when this item is collected")]
	public AudioClip m_collectSound = null;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// When a trigger interaction starts involving this game object...
	void OnTriggerEnter2D(Collider2D _other)
	{
        AttemptHeal(_other);
    }
    // When a collision interaction starts involving this game object...
    void OnCollisionEnter2D(Collision2D _collision)
    {
        AttemptHeal(_collision.collider);
    }
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region private Functions
    // *****************************************************************************************************************
    private void AttemptHeal(Collider2D _other)
    {
        // Get the health pool from our collided object
        HealthPool healthPool = _other.GetComponent<HealthPool>();

        // If it had a health pool...
        if (healthPool != null)
        {
            // Heal it by the amount set
            bool healed = healthPool.Heal(m_healing, m_targetType);

            // if we did in fact heal....
            if (healed)
            {

                // If we should destroy this object, do so
                if (m_destroyOnCollect)
                {
                    Destroy(gameObject);
                }

                // If we have a sound to play, play it
                if (m_collectSound)
                {
                    // Play the sound for collecting at this locaiton
                    AudioSource.PlayClipAtPoint(m_collectSound, transform.position);
                }
            }
        }
    }
    #endregion
    // *****************************************************************************************************************

}
// *********************************************************************************************************************
