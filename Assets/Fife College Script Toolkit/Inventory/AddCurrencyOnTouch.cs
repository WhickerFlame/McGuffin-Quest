// *********************************************************************************************************************
// File: AddCurrencyOnTouch.cs
// Purpose: Adds currency to a CurrencyPouch that touches this trigger
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
public class AddCurrencyOnTouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What type of currency should be added?")]
	public string m_currencyType = "score";
	[Tooltip("How much currency will be added?")]
	public int m_amount = 1;
	[Tooltip("Should we destroy this object after collecting it?")]
	public bool m_destroyOnCollect = true;
	[Tooltip("The sound that should be played when this currency is added")]
	public AudioClip m_collectSound = null;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// When a trigger interaction starts involving this game object...
	void OnTriggerEnter2D(Collider2D _other)
	{
		HandleInteraction(_other);
	}
	// *****************************************************************************************************************
	// When a collision interaction starts involving this game object...
	void OnCollisionEnter2D(Collision2D _collision)
	{
		HandleInteraction(_collision.collider);
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Private Functions
	// *****************************************************************************************************************
	// This function actually adds the currency
	private void HandleInteraction(Collider2D _other)
	{
		// Check if the other collider that we hit has a CurrencyPouch on it
		CurrencyPouch currencyPouch = _other.GetComponent<CurrencyPouch>();
		if (currencyPouch != null) {
			// Attempt to add to the CurrencyPouch
			bool added = currencyPouch.AddCurrency(m_currencyType, m_amount);

			// if we successfully added it...
			if (added) {
				// If we should destroy this object, do so
				if (m_destroyOnCollect) {
					Destroy (gameObject);
				}

				// If we have a sound to play, play it
				if (m_collectSound) {
					// Play the sound for currency add at this locaiton
					AudioSource.PlayClipAtPoint(m_collectSound,transform.position);
				}
			}
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
