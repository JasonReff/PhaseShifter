using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomTrigger : MonoBehaviour
{
    protected Room _room;
    private bool _roomEntered;

    public Room Room { get => _room; set => _room = value; }
    protected virtual void RoomEntered()
    {
        _roomEntered = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _roomEntered == false)
        {
            RoomEntered();
        }
    }
}
