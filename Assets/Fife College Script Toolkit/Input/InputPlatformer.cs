// *********************************************************************************************************************
// File: InputPlatformer.cs
// Purpose: A controller for platformer style input and movement
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
public class InputPlatformer : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Header("Primary")]
	[Tooltip("How fast does this character move?")]
	public float m_speed = 10;
	[Tooltip("Initial jump speed - higher means higher jumps")]
	public float m_jumpSpeed = 50;
	[Tooltip("How many air jumps are allowed?")]
	public int m_allowedAirJumps = 0;
	[Tooltip("Name of the input axis for Horizontal movement")]
	public string m_inputAxisHorizontal = "Horizontal";
	[Tooltip("Name of the input button for jumping")]
	public string m_inputButtonJump = "Jump";
	[Tooltip("Name of the layer for touching the ground")]
	public string m_layerGround = "Ground";
	[Tooltip("The collider to use for checking ground touch on mario")]
	public Collider2D m_footCollider;

	[Header("Effects")]
	[Tooltip("The animator that should be used for movement")]
	public Animator m_animator;
	[Tooltip("The name of the float animation parameter that should be used for speed")]
	public string m_speedParameter = "Speed";
	[Tooltip("The name of the bool animation parameter that should be used for whether the character is touching the ground")]
	public string m_touchingGroundParameter = "TouchingGround";

	// Private Variables
	private int m_numAirJumps = 0;
	#endregion
	// *****************************************************************************************************************

	#region Unity Functions
	void Update()
	{

		// Check if we are pressing a button to more horizontally
		// This will be a number between -1 and 1 based on whether the
		//    player has been pressing the left or right buttons (or a/d)
		//    It should also work with a controller!
		float horizontal = Input.GetAxis(m_inputAxisHorizontal);

		// Get the rigidbody component attached to the Player
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		// From the rigidbody, get the current velocity.
		// This is so we can preserve any existing y velocity.
		Vector2 velocity = rigidBody.velocity;

		// Set only the x component of the velocity.
		// Set it to the value of the horizontal axis (-1 to 1)
		//    multiplied by the designer variable m_speed.
		velocity.x = horizontal * m_speed;

		// Set our animator speed value to control running/walking/standing animation
		if (m_animator != null && m_speedParameter != "" ) {
			m_animator.SetFloat(m_speedParameter, Mathf.Abs(velocity.x));
		}

		// Turn left or right based on the velocity
		// Only do this if we actually have a velocity - otherwise we would always return to facing one direction when standing.
		bool flip = velocity.x < 0;
		float flipMult = 1;
		if (flip)
		{
			flipMult = -1;
		}

		if (Mathf.Abs(velocity.x) > 0)
		{
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flipMult, 
				transform.localScale.y, 
				transform.localScale.z);
		}

		// Handle jumping
		// First we need to check if we are touching the ground!

		// Get the LayerMask for the ground layer - we need this for our next
		// function call.
		LayerMask groundLayer = LayerMask.GetMask(m_layerGround);

		// Ask the collider if we are touching the ground layer.
		bool touchingGround = m_footCollider.IsTouchingLayers(groundLayer);

		// Set our animator TouchingGround value to control our jumping/standing animation
		if (m_animator != null && m_touchingGroundParameter != "") {
			m_animator.SetBool(m_touchingGroundParameter, touchingGround);
		}

		// Check if the "Jump" button (space) has been pressed down.
		if(Input.GetButtonDown(m_inputButtonJump))
		{
			// If we are touching the ground,
			//    we can reset our air jump count to 0
			if (touchingGround)
				m_numAirJumps = 0;

			// Normally we are only allowed to jump if we are touching the ground.
			bool allowedToJump = touchingGround;

			// However, if our allowed air jumps are 
			//    higher than our current air jump count
			//    (meaning we have at least 1 jump left)
			//    we are also allowed to jump,
			//    even if we aren't touching the ground!
			if (m_allowedAirJumps > m_numAirJumps)
				allowedToJump = true;

			// If we are in fact touching the ground,
			//    only then do we apply the jump!
			//    Otherwise we ignore it.
			if (allowedToJump)
			{
				// Set our upward velocty
				velocity.y = m_jumpSpeed;

				// If we aren't currently touching the ground, 
				//    we need to add to our air jump count.
				if (!touchingGround)
					++m_numAirJumps;
			}
		}

		// Now just update the rigidbody with this new velocty
		// This will update the rigidbody for both running and jumping!
		rigidBody.velocity = velocity;

	}
	#endregion
}
