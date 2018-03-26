// *********************************************************************************************************************
// File: MovementChase.cs
// Purpose: Moves an object toward a target object
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
public class MovementChase : MonoBehaviour {
// *********************************************************************************************************************


    // *****************************************************************************************************************
    #region Variables
    // *****************************************************************************************************************
    // Exposed Variables
    [Tooltip("Speed this object should move at")]
    public float m_speed;
    [Tooltip("Object we should chase")]
    public GameObject m_target;
    [Tooltip("Should we apply movement in the x direction?")]
    public bool m_moveX = true;
    [Tooltip("Should we apply movement in the y direction?")]
    public bool m_moveY = true;
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region Unity Functions
    // *****************************************************************************************************************
    // Update is called once per frame
    void Update()
    {
        // Get our current target point
        Vector3 targetPoint = m_target.transform.position;

        // Determine direction & normalize to length of 1
        Vector2 direction = targetPoint - transform.position;

        // If we shouldn't be moving in the x direction, clear x direction
        if (!m_moveX)
        {
            direction.x = 0;
        }

        // If we shouldn't be moving in the y direction, clear y direction
        if (!m_moveY)
        {
            direction.y = 0;
        }

        // Normalize the direction to length of 1
        direction.Normalize();

        // Get the rigidbody attached to this object
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        // Get the velocity from our rigidbody
        Vector2 velocity = rigidbody.velocity;

        // Our velocity will be equal to our direction times our speed
        // Only set x velocity if we have said we should move in x
        velocity.x = direction.x * m_speed;
        // Only set y velocity if we have said we should move in y
        velocity.y = direction.y * m_speed;

        // Assign our modified velocity back to the rigidbody
        rigidbody.velocity = velocity;
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************

}
