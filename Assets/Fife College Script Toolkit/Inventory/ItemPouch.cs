// *********************************************************************************************************************
// File: Inventory.cs
// Purpose: Hold any number of items
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
public class ItemPouch : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Private Variables
	private Dictionary<string, int> m_itemValues = new Dictionary<string, int>();
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Public Functions
	// *****************************************************************************************************************
	// Adds the specified item to the 
	public void AddItem(string _itemName, int _amount = 1) {
		// Check if we already have a slot for it...
		if (m_itemValues.ContainsKey (_itemName)) {
			// If we have a slot already, just add to it
			m_itemValues [_itemName] += _amount;
		} else {
			// If not, create a slot and set it to this amount
			m_itemValues [_itemName] = _amount;
		}
	}
	// *****************************************************************************************************************
	// Returns the amount of the specified item we have
	public int GetNumItems(string _itemName) {
		// Check if we have a slot for it...
		if (m_itemValues.ContainsKey (_itemName)) {
			// If we have a slot, return the amount we have
			return m_itemValues[_itemName];
		} else {
			// If not, that means we have 0 so return 0
			return 0;
		}
	}
	// *****************************************************************************************************************
	// Removes the specified items if we have enough - returns true if we had enough, otherwise false
	public bool RemoveItems(string _type, int _amount) {
		// Check if we have a slot for it...
		if (m_itemValues.ContainsKey (_type)) {
			// If we have a slot, see if we have enough to spend
			if (m_itemValues [_type] >= _amount) {
				// If we have enough, remove it
				m_itemValues [_type] -= _amount;
				// we successfully spent it, so return true
				return true;
			} else {
				// We don't have enough, so don't remove any - return false
				return false;
			}
		} else {
			// If not, that means we have none so we cannot remove it
			return false;
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
