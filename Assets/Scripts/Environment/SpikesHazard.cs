using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesHazard : Hazard
{
    [SerializeField] private float _knockback = 2;
    [SerializeField] private int _damage = 1;
    protected override void HazardTouched(PlayerHealthController playerHealth)
    {
        base.HazardTouched(playerHealth);
        var direction = playerHealth.transform.position - transform.position;
        var force = direction * _knockback;
        playerHealth.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        playerHealth.TakeDamage(_damage);
    }
}
