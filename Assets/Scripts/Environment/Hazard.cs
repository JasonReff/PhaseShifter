using UnityEngine;

public class Hazard : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealthController playerHealth))
        {
            HazardTouched(playerHealth);
        }
    }

    protected virtual void HazardTouched(PlayerHealthController playerHealth)
    {

    }
}