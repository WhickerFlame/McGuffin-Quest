// *********************************************************************************************************************
// File: ChangeSceneOnTouch.cs
// Purpose: Changes to a different scene when touched, optionally requiring a button press
// Project: Fife College Unity Toolkit
// Copyright Fife College 2018
// *********************************************************************************************************************


// *********************************************************************************************************************
#region Imports
// *********************************************************************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// The scene management library has to be included
using UnityEngine.SceneManagement;
#endregion
// *********************************************************************************************************************


// *********************************************************************************************************************
public class ChangeSceneOnTouch : MonoBehaviour {
	// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What scene should we go to?")]
	public string m_sceneName;
	[Tooltip("Optional - what button must be pressed?")]
	public string m_requiredButton;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called every frame trigger interaction happens involving this game object...
	void OnTriggerStay2D(Collider2D _other)
	{
		HandleCollision(_other);
	}
	// Called every frame a collision interaction happens involving this game object...
	void OnCollisionStay2D(Collision2D _collision)
	{
		HandleCollision(_collision.collider);
	}
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region private Functions
	// *****************************************************************************************************************
	private void HandleCollision(Collider2D _other)
	{
		// if we don't require a button, OR, the required button is pressed...
		if (m_requiredButton == "" || Input.GetButton(m_requiredButton))
		{
			// Load the next scene
			SceneManager.LoadScene(m_sceneName);
		}
	}
	#endregion
	// *****************************************************************************************************************

}
// *********************************************************************************************************************
