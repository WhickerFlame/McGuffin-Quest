// *********************************************************************************************************************
// File: CameraFollow.cs
// Purpose: Allows the camera to follow an object
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
public class CameraFollow : MonoBehaviour {
	// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("Which object should we follow?")]
	public Transform m_target;
	[Tooltip("Should we follow on the X axis?")]
	public bool m_followX = true;
	[Tooltip("Should we follow on the Y axis?")]
	public bool m_followY = true;
	[Tooltip("Rectangle defining dead zone - if character is within this range, will not follow")]
	public Vector2 m_deadZoneMin = Vector2.zero;
	[Tooltip("Rectangle defining dead zone - if character is within this range, will not follow")]
	public Vector2 m_deadZoneMax = Vector2.zero;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called every frame, after all other functions
	void LateUpdate()
	{
		// Get our current position from our transform
		Vector3 cameraPosition = transform.position;

		// Get the position of our target relative to us, for dead zone calcs
		Vector3 relativePosition = m_target.position - cameraPosition;

		// Move in X direction
		if (m_followX == true)
		{
			// Check if target is outside of deadzone to the left
			if (relativePosition.x < m_deadZoneMin.x)
			{
				// Target is outside the deadzone, so we need to adjust
				float difference = relativePosition.x - m_deadZoneMin.x;
				cameraPosition.x += difference;
			}
			// Check if target is outside of deadzone to the right
			else if (relativePosition.x > m_deadZoneMax.x)
			{
				// Target is outside the deadzone, so we need to adjust
				float difference = relativePosition.x - m_deadZoneMax.x;
				cameraPosition.x += difference;
			}
		}
		// Move in Y direction
		if (m_followY == true)
		{
			// Check if target is outside of deadzone to the bottom
			if (relativePosition.y < m_deadZoneMin.y)
			{
				// Target is outside the deadzone, so we need to adjust
				float difference = relativePosition.y - m_deadZoneMin.y;
				cameraPosition.y += difference;
			}
			// Check if target is outside of deadzone to the top
			else if (relativePosition.y > m_deadZoneMax.y)
			{
				// Target is outside the deadzone, so we need to adjust
				float difference = relativePosition.y - m_deadZoneMax.y;
				cameraPosition.y += difference;
			}
		}

		// Assign our adjusted position back to the camera
		transform.position = cameraPosition;
	}
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
