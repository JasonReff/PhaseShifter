using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupSound;

    protected virtual void OnPickup()
    {
        if (_pickupSound != null)
            AudioManager.PlaySoundEffect(_pickupSound);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPickup();
            Destroy(gameObject);
        }
    }
}
