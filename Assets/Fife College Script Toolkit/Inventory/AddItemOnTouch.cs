// *********************************************************************************************************************
// File: AddItemOnTouch.cs
// Purpose: Adds item to an ItemPouch that touches this trigger
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
public class AddItemOnTouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What type of item should be added?")]
	public string m_itemName = "";
	[Tooltip("How many items will be added?")]
	public int m_amount = 1;
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
	private void HandleInteraction(Collider2D _other)
	{
		// Check if the other collider that we hit has a ItemPouch on it
		ItemPouch itemPouch = _other.GetComponent<ItemPouch>();
		if (itemPouch != null) {
			// Add to the ItemPouch
			itemPouch.AddItem(m_itemName, m_amount);

			// If we should destroy this object, do so
			if (m_destroyOnCollect) {
				Destroy (gameObject);
			}

			// If we have a sound to play, play it
			if (m_collectSound) {
				// Play the sound for collecting at this locaiton
				AudioSource.PlayClipAtPoint(m_collectSound,transform.position);
			}
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
