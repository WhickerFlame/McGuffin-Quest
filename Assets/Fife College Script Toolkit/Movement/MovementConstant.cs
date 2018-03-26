// *********************************************************************************************************************
// File: MovementConstant.cs
// Purpose: Moves an object constantly in a velocity
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
[RequireComponent(typeof(Rigidbody2D))]
public class MovementConstant : MonoBehaviour {
// *********************************************************************************************************************


    // *****************************************************************************************************************
    #region Variables
    // *****************************************************************************************************************
    // Exposed Variables
    [Tooltip("Do we have a constant x velocity?")]
    public bool m_hasXVel;
    [Tooltip("What should our constant x velocity be?")]
    public float m_xVel;
    [Tooltip("Do we have a constant y velocity?")]
    public bool m_hasYVel;
    [Tooltip("What should our constant y velocity be?")]
    public float m_yVel;
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region Unity Functions
    // *****************************************************************************************************************
    // Update is called once per frame
    void Update()
    {
        // Get the rigidbody attached to this object
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        // Get our velocity so we can modify it
        Vector2 velocity = rigidbody.velocity;

        // If we should set our x velocity, do so
        if (m_hasXVel)
        {
            velocity.x = m_xVel;
        }

        // If we should set our y velocity, do so
        if (m_hasYVel)
        {
            velocity.x = m_yVel;
        }

        // Assign our modified velocity back to the rigidbody
        rigidbody.velocity = velocity;
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************

}
// *********************************************************************************************************************
