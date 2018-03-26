// *********************************************************************************************************************
// File: InputTopDown.cs
// Purpose: A controller for top down style input and movement
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
public class InputTopDown : MonoBehaviour
{
// *********************************************************************************************************************


    // *****************************************************************************************************************
    #region Variables
    // *****************************************************************************************************************
    // Exposed Variables
    [Header("Primary")]
    [Tooltip("How fast does this character move?")]
    public float m_speed = 10;
    [Tooltip("Name of the input axis for Horizontal movement")]
    public string m_inputAxisHorizontal = "Horizontal";
    [Tooltip("Name of the input axis for Vertical movement")]
    public string m_inputAxisVertical = "Vertical";
    [Tooltip("Flip x based on velocity?")]
    public bool m_flipX = true;
    [Tooltip("Flip y based on velocity?")]
    public bool m_flipY = true;

    [Header("Effects")]
    [Tooltip("The animator that should be used for movement")]
    public Animator m_animator;
    [Tooltip("The name of the float animation parameter that should be used for speed.")]
    public string m_speedParameter = "";
    [Tooltip("The name of the float animation parameter that should be used for x velocity")]
    public string m_xVelParameter = "";
    [Tooltip("The name of the float animation parameter that should be used for y velocity")]
    public string m_yVelParamter = "";

    // Private Variables
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

        // Do the same for vertical movement
        float vertical = Input.GetAxis(m_inputAxisVertical);

        // Get the rigidbody component attached to the Player
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        // From the rigidbody, get the current velocity.
        // This is so we can preserve any existing y velocity.
        Vector2 velocity = rigidBody.velocity;

        // Set only the x component of the velocity.
        // Set it to the value of the horizontal axis (-1 to 1)
        //    multiplied by the designer variable m_speed.
        velocity.x = horizontal * m_speed;

        // do the same for y and vertical
        velocity.y = vertical * m_speed;

        // Now just update the rigidbody with this new velocty
        // This will update the rigidbody for both up and down movement
        rigidBody.velocity = velocity;

        // Set our animator speed value to control running/walking/standing animation
        // We use the magnitude of the velocity vector to get the overall speed
        if (m_animator != null)
        {
            if (m_speedParameter != "")
                m_animator.SetFloat(m_speedParameter, velocity.magnitude);

            // These can be used in the animtor controller to control what direction the object 
            // is facing, if there are custom sprites for direction.
            if (m_xVelParameter != "")
                m_animator.SetFloat(m_xVelParameter, velocity.x);
            if (m_yVelParamter != "")
                m_animator.SetFloat(m_yVelParamter, velocity.y);
        }

        // Turn left or right based on the velocity
        if (m_flipX)
        {
            // Only do this if we actually have a velocity - otherwise we would always return to facing one direction when standing.
            bool shouldFlipX = velocity.x < 0;
            float flipMultX = 1;
            if (shouldFlipX)
            {
                flipMultX = -1;
            }

            if (Mathf.Abs(velocity.x) > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flipMultX,
                    transform.localScale.y,
                    transform.localScale.z);
            }
        }

        // Turn up or down based on the velocity
        if (m_flipY)
        {
            // Only do this if we actually have a velocity - otherwise we would always return to facing one direction when standing.
            bool shouldFlipY = velocity.y < 0;
            float flipMult = 1;
            if (shouldFlipY)
            {
                flipMult = -1;
            }

            if (Mathf.Abs(velocity.y) > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x,
                    Mathf.Abs(transform.localScale.y) * flipMult,
                    transform.localScale.z);
            }
        }

    }
    #endregion
}
