using System;
using UnityEngine;

public class MinimapTrigger : RoomTrigger
{
    public static Action<Room> OnRoomEntered;

    protected override void RoomEntered()
    {
        base.RoomEntered();
        OnRoomEntered?.Invoke(_room);
    }
}