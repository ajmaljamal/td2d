﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If enemy rise this point - player will be defeated.
/// </summary>
public class CapturePoint : MonoBehaviour
{
    // Enemy already reached capture point
    private bool alreadyCaptured;

    /// <summary>
    /// Raises the trigger enter2d event.
    /// </summary>
    /// <param name="other">Other.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // If collision allowed for this scene
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true)
        {
            if (alreadyCaptured == false)
            {
                alreadyCaptured = true;
                EventManager.TriggerEvent("Captured", other.gameObject, null);
            }
        }
    }
}
