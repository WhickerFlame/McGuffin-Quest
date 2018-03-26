// *********************************************************************************************************************
// File: HealthBar.cs
// Purpose: Displays visually the health of a HealthPool
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
public class HealthBar : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What HealthPool should we display?")]
	public HealthPool m_healthPool;
	[Tooltip("What sprite renderer is showing the fill of the health bar?")]
	public SpriteRenderer m_healthBarFill;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called every frame
	void Update()
	{
		// Get the fraction/percentage of how much health the HealthPool currently has
		float healthPercentage = m_healthPool.GetFractionHealth();

		// Set the scale of our sprite renderer to match this
		Vector3 scale = m_healthBarFill.transform.localScale;
		scale.x = healthPercentage;
		m_healthBarFill.transform.localScale = scale;

		// NOTE: This is designed to work with fill sprites with a pivot point set to "Left". 
		// Set this in the import settings for the sprite itself.
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
