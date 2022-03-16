using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPool")]
public class ItemPool : ScriptableObject
{
    public List<GameObject> Items = new List<GameObject>();
}