// *********************************************************************************************************************
// File: CurrencyPouch.cs
// Purpose: Hold any number of currencies
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
public class CurrencyPouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("Names of the currency held by this object")]
	public List<string> m_currencyNames = new List<string>();
	[Tooltip("Optional: Text mesh to display this currency value. Must match order of names.")]
	public List<TextMesh> m_currencyDisplay = new List<TextMesh>();
	[Tooltip("Should we save these currencies to the player preferences?")]
	public bool m_save = false;

	// Private Variables
	private Dictionary<string, int> m_currencyValues = new Dictionary<string, int>();
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called when the object is created
	void Start()
	{
		// Load data if save is true
		if (m_save == true)
		{
			LoadSaveData();
		}
	}
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Public Functions
	// *****************************************************************************************************************
	// Adds the specified amount to our pouch, if we can hold that type
	public bool AddCurrency(string _type, int _amount) {
		// If this type of currency is one that we can carry...
		if (m_currencyNames.Contains(_type)) {
			// Check if we already have a slot for it...
			if (m_currencyValues.ContainsKey (_type)) {
				// If we have a slot already, just add to it
				m_currencyValues [_type] += _amount;
			} else {
				// If not, create a slot and set it to this amount
				m_currencyValues [_type] = _amount;
			}

			// Update our visuals
			UpdateVisualDisplay();

			// Return true because we successfully added the currency
			return true;
		}

		// Return false because we cannot add this currency
		return false;
	}
	// *****************************************************************************************************************
	// Returns the amount of the specified currency we have
	public int GetCurrencyValue(string _type) {
		// If this type of currency is one that we can carry...
		if (m_currencyNames.Contains(_type)) {
			// Check if we have a slot for it...
			if (m_currencyValues.ContainsKey (_type)) {
				// If we have a slot, return the amount we have
				return m_currencyValues[_type];
			} else {
				// If not, that means we have 0 so return 0
				return 0;
			}
		}

		// We can't hold this currency, so we have 0
		return 0;
	}
	// *****************************************************************************************************************
	// Spends the specified currency if we have enough - returns true if we had enough, otherwise false
	public bool SpendCurrency(string _type, int _amount) {
		// If this type of currency is one that we can carry...
		if (m_currencyNames.Contains(_type)) {
			// Check if we have a slot for it...
			if (m_currencyValues.ContainsKey (_type)) {
				// If we have a slot, see if we have enough to spend
				if (m_currencyValues [_type] >= _amount) {
					// If we have enough, spend it
					m_currencyValues [_type] -= _amount;
					// Update our visuals
					UpdateVisualDisplay();
					// we successfully spent it, so return true
					return true;
				} else {
					// We don't have enough, so don't spend any - return false
					return false;
				}
			} else {
				// If not, that means we have none so we cannot spend it
				return false;
			}
		}

		// We can't hold this currency, so cannot spend it
		return false;
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Private Functions
	// *****************************************************************************************************************
	// Update te visual display for the currency
	private void UpdateVisualDisplay() {
		// Loop through each of our currency currencies
		for (int i = 0; i < m_currencyNames.Count; ++i) {
			// get the name for this currency
			string currencyName = m_currencyNames [i];
			// If we have a value for this currency...
			if (m_currencyValues.ContainsKey (currencyName)) {
				// if we have a display for this currency...
				if (m_currencyDisplay.Count > i && m_currencyDisplay[i] != null) {
					// update the text display with the value
					m_currencyDisplay[i].text = m_currencyValues[currencyName].ToString();
				}
			}
		}
	}
	// *****************************************************************************************************************
	private void UpdateSaveData()
	{
		// Loop through each of our currency currencies
		for (int i = 0; i < m_currencyNames.Count; ++i) {
			// get the name for this currency
			string currencyName = m_currencyNames [i];
			// If we have a value for this currency...
			if (m_currencyValues.ContainsKey (currencyName)) {
				// Save our value in the player preferences
				PlayerPrefs.SetInt(currencyName, m_currencyValues[currencyName]);
			}
		}
	}
	// *****************************************************************************************************************
	private void LoadSaveData()
	{
		// Loop through each of our currency currencies
		for (int i = 0; i < m_currencyNames.Count; ++i) {
			// get the name for this currency
			string currencyName = m_currencyNames [i];
			// If this currency is stored in preferences...
			if (PlayerPrefs.HasKey(currencyName))
			{
				// load our value from the player preferences
				m_currencyValues[currencyName] = PlayerPrefs.GetInt(currencyName);
			}
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************