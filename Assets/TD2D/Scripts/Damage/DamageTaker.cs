﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This target can receive damage.
/// </summary>
public class DamageTaker : MonoBehaviour
{
    // Start hitpoints
    public int hitpoints = 1;
    // Remaining hitpoints
    public int currentHitpoints;
    // Hit visual effect duration
    public float hitDisplayTime = 0.2f;

    // Image of this object
    private SpriteRenderer sprite;
    // Visualisation of hit
    private bool hitCoroutine;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        currentHitpoints = hitpoints;
        sprite = GetComponentInChildren<SpriteRenderer>();
        Debug.Assert(sprite, "Wrong initial parameters");
    }

    /// <summary>
    /// Take damage.
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void TakeDamage(int damage)
    {
        if (currentHitpoints > damage)
        {
            // Still alive
            currentHitpoints -= damage;
            // If no coroutine now
            if (hitCoroutine == false)
            {
                // Damage visualisation
                StartCoroutine(DisplayDamage());
            }
        }
        else
        {
            // Die
            currentHitpoints = 0;
            Die();
        }
    }

    /// <summary>
    /// Die this instance.
    /// </summary>
    public void Die()
    {
        EventManager.TriggerEvent("UnitDie", gameObject, null);
        Destroy(gameObject);
    }

    /// <summary>
    /// Damage visualisation.
    /// </summary>
    /// <returns>The damage.</returns>
    IEnumerator DisplayDamage()
    {
        hitCoroutine = true;
        Color originColor = sprite.color;
        float counter;
        // Set color to black and return to origin color over time
        for (counter = 0f; counter < hitDisplayTime; counter += Time.deltaTime)
        {
            sprite.color = Color.Lerp(originColor, Color.black, Mathf.PingPong(counter, hitDisplayTime));
            yield return new WaitForEndOfFrame();
        }
        sprite.color = originColor;
        hitCoroutine = false;
    }
}
