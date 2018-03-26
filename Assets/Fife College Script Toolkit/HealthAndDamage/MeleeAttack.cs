// *********************************************************************************************************************
// File: MeleeAttack.cs
// Purpose: Turns on an attacking collider when a button is pressed
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
public class MeleeAttack : MonoBehaviour {
// *********************************************************************************************************************
    
    // *****************************************************************************************************************
    #region Variables
    // *****************************************************************************************************************
    // Exposed Variables
    [Header("Primary")]
    [Tooltip("Collider for melee attack that should be turned on and off")]
    public Collider2D m_meleeCollider = null;
    [Tooltip("How long does the attack last (how long should the collider be active?")]
    public float m_attackDuration = 0.5f;
    [Tooltip("How much cooldown between attacking?")]
    public float m_cooldownDuration = 1f;
    [Tooltip("Button used to trigger attack")]
    public string m_attackButton;

    [Header("Effects")]
    [Tooltip("The sound that should be played on attack")]
    public AudioClip m_meleeSound = null;
    [Tooltip("The animator that should be used for attacking animations")]
    public Animator m_animator;
    [Tooltip("The name of the trigger parameter that should be used for attacking effects")]
    public string m_meleeTrigger;

    // Private variables
    private float m_attackEnd = 0;
    private float m_cooldownEnd = 0;
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region Unity Functions
    // *****************************************************************************************************************
    // Update is called once per frame
    void Update()
    {
        // If we have clicked the mouse button and our attack is not on cooldown...
        if (Input.GetButtonDown(m_attackButton) && !IsOnCooldown())
        {
            // Turn on the attack collider
            m_meleeCollider.enabled = true;

            // Record when our attack should end.
            // At that time, we'll turn our collider back off
            m_attackEnd = Time.time + m_attackDuration;

            // Record when our cooldown should end.
            // This makes us unable to attack for m_cooldownDuration seconds
            m_cooldownEnd = Time.time + m_cooldownDuration;

            // If we have specified a sound to play...
            if (m_meleeSound != null)
            {
                // Play the sound for dying at this locaiton
                AudioSource.PlayClipAtPoint(m_meleeSound, transform.position);
            }

            // If we have supplied an animator and a trigger to use...
            if (m_animator != null && m_meleeTrigger != "")
            {
                // Play death animation
                m_animator.SetTrigger(m_meleeTrigger);
            }
        }

        // Check if it is now past the attack end time
        if (Time.time >= m_attackEnd)
        {
            // If it is, we should turn off our melee collider
            m_meleeCollider.enabled = false;
        }
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************


    // *****************************************************************************************************************
    #region Public Functions
    // *****************************************************************************************************************
    // Is our attack on cooldown?
    public bool IsOnCooldown()
    {
        // true if the current time is less than the end time for the cooldown
        return Time.time < m_cooldownEnd;
    }
    // *****************************************************************************************************************
    #endregion
    // *****************************************************************************************************************


}
// *********************************************************************************************************************
