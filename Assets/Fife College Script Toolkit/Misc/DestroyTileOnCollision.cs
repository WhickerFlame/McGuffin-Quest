// *********************************************************************************************************************
// File: DestroyTileOnCollision.cs
// Purpose: When the specified object collides with a tile in this map, 
// 			destroy the tile
// Project: Fife College Unity Toolkit
// Copyright Fife College 2018
// *********************************************************************************************************************


// *********************************************************************************************************************
#region Imports
// *********************************************************************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// We need to include the tilemap library
using UnityEngine.Tilemaps;
#endregion
// *********************************************************************************************************************


// *********************************************************************************************************************
public class DestroyTileOnCollision : MonoBehaviour {
	// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("The Tilemap we will check tiles for")]
	public Tilemap m_tileMap;
	[Tooltip("The Object which we will check collisions with")]
	public Collider2D m_targetCollider;
	[Tooltip("Optional particle effect to play when the tile is broken")]
	public GameObject m_particlePrefab = null;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// When a collision interaction starts involving this game object...
	void OnCollisionEnter2D(Collision2D _collision)
	{
		Debug.Log("Collision!");
		// Only act if the object we collided with is our target
		if (_collision.collider == m_targetCollider)
		{
			Debug.Log("Collision with head!");
			// Create a vector variable to hold our hit position
			Vector3 hitPosition = Vector3.zero;

			// Loop through each collision hit position in the collision data
			foreach (ContactPoint2D hit in _collision.contacts)
			{
				// Record teh hit location
				hitPosition.x = hit.point.x + 0.1f * hit.normal.x;
				hitPosition.y = hit.point.y + 0.1f * hit.normal.y;

				// Get the tile's position in the grid
				Vector3Int tilePosition = m_tileMap.WorldToCell(hitPosition);

				// Set that tile to null (meaning delete it)
				m_tileMap.SetTile(tilePosition, null);

				Debug.Log("Deleting tile at "+tilePosition);
			}

			// If we have a particle effect
			if (m_particlePrefab != null)
			{
				// Use instantiate to create our particle prefab at our hit position
				GameObject particles = Instantiate(m_particlePrefab);

				// Move our particles to the correct location
				particles.transform.position = _collision.collider.transform.position;
			}
		}
	}
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
