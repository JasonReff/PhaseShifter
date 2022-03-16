using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitHazard : Hazard
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterHealthController characterHealth))
        {
            StartCoroutine(characterHealth.FallingDeath());
        }
    }
}
