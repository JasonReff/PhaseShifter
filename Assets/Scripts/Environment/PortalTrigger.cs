using System;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _portalSound;
    public static event Action OnPortalEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPortalEntered?.Invoke();
            AudioManager.PlaySoundEffect(_portalSound);
        }
    }
}