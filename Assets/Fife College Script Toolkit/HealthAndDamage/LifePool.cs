// *********************************************************************************************************************
// File: LifePool.cs
// Purpose: Keeps track of the lives the player has left
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
public class LifePool : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("How many lives the player should start with")]
	public int m_startingLives = 3;
	[Tooltip("Save key for lives in the player preferences")]
	public string m_saveKey = "lives";
	[Tooltip("Visual display for lives")]
	public TextMesh m_livesDisplay;
	[Tooltip("Screen to use for game over")]
	public string m_gameOverScreen;

	// Private variables
	private int m_currentLives = 3;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called when the object is created
	void Start()
	{
		// If the player preferences have save data for the lives...
		if (PlayerPrefs.HasKey(m_saveKey))
		{
			// load current lives from save data
			m_currentLives = PlayerPrefs.GetInt(m_saveKey);
		}
		else
		{
			// no save data, so use starting lives
			m_currentLives = m_startingLives;
		}

		// If we should game over, game over!
		if (!HasLives() && m_gameOverScreen != "")
		{
			// Load the game over screen
			SceneManager.LoadScene(m_gameOverScreen);
		}

		// Update our visual display
		UpdateVisualDisplay();
	}
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Public Functions
	// *****************************************************************************************************************
	public void AddLife(int _numToAdd = 1) { 
		// The "=1" means that 1 is the default value
		// This means calling functions don't HAVE to pass a value in -
		// if they don't, it is as if they passed 1.

		// Add to current lives
		m_currentLives += _numToAdd;

		// Save our new total
		UpdateSaveData();

		// Update our visual display
		UpdateVisualDisplay();
	}
	// *****************************************************************************************************************
	public int GetCurrentLives() {
		return m_currentLives;
	}
	// *****************************************************************************************************************
	public bool LoseLife(int _amount = 1) {
		// Reduce our lives by the amount passed in
		m_currentLives -= _amount;

		// If we have less than 0, set to 0
		// (prevents negative lives)
		if (m_currentLives < 0)
		{
			m_currentLives = 0;
		}

		// Save our new total
		UpdateSaveData();

		// Update our visual display
		UpdateVisualDisplay();

		// If we should game over, game over!
		if (!HasLives() && m_gameOverScreen != "")
		{
			// Load the game over screen
			SceneManager.LoadScene(m_gameOverScreen);
		}

		// return wether we are still have lives or not
		return HasLives();
	}
	// *****************************************************************************************************************
	public bool HasLives()
	{
		return m_currentLives > 0;
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Private Functions
	// *****************************************************************************************************************
	private void UpdateVisualDisplay() {
		// If we have a visual display for lives...
		if (m_livesDisplay != null)
		{
			// Update the display text to our current life count
			m_livesDisplay.text = m_currentLives.ToString();
		}
	}
	// *****************************************************************************************************************
	private void UpdateSaveData()
	{
		// Set the value in the player preferences to match current lives
		PlayerPrefs.SetInt(m_saveKey, m_currentLives);
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************

}
// *********************************************************************************************************************
