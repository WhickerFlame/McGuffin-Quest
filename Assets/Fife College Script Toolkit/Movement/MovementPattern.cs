// *********************************************************************************************************************
// File: MovementPattern.cs
// Purpose: Move an object toward targets
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
public class MovementPattern : MonoBehaviour {
    // *********************************************************************************************************************

    // *****************************************************************************************************************
    #region Variables
    // *****************************************************************************************************************
    // Exposed Variables
    [Tooltip("Speed this object should move at")]
    public float m_speed;
    [Tooltip("Target points this object should move towards")]
    public GameObject[] m_targetPoints;
    [Tooltip("Should we loop back around to the beginning after reaching our last target?")]
    public bool m_shouldLoop = true;
    [Tooltip("Should we apply movement in the x direction?")]
    public bool m_moveX = true;
    [Tooltip("Should we apply movement in the y direction?")]
    public bool m_moveY = true;

    // Private Variables
    private int m_currentPoint = 0;
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region Unity Functions
    // *****************************************************************************************************************
    // Update is called once per frame
    void Update()
    {
        // Get our current target point
        Vector3 targetPoint = m_targetPoints[m_currentPoint].transform.position;

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

        // Will we reach our destination by the next frame?
        // Get the distance to our target
        float distanceToTarget = (targetPoint - transform.position).magnitude;
        // special case if we are ignoring x or y
        if (!m_moveX)
            distanceToTarget = Mathf.Abs(targetPoint.y - transform.position.y);
        if (!m_moveY)
            distanceToTarget = Mathf.Abs(targetPoint.x - transform.position.x);
        // Get the distance we will travel in a frame
        float distanceTraveled = m_speed * Time.deltaTime;
        // If the distance we travel is equal or greater than the distance to target,
        // we know we've reached our target
        if (distanceTraveled >= distanceToTarget || distanceToTarget == 0)
        {
            // Update our target to a new target
            m_currentPoint = m_currentPoint + 1;
            
            // If our new point is equal or higher than the length of our array, we've got to the end
            if (m_currentPoint >= m_targetPoints.Length)
            {
                // If we are supposed to loop back around...
                if (m_shouldLoop)
                {
                    // Set our current point back to 0 (beginning of list)
                    m_currentPoint = 0;
                }
                // If we are NOT supposed to loop...
                else
                {
                    // Set our velocity to 0 and
                    // disable this script to keep it from acting.
                    rigidbody.velocity = Vector2.zero;
                    enabled = false;
                }
            }
        }
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************

}
// *********************************************************************************************************************

