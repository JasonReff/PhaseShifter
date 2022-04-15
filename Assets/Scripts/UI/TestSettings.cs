using UnityEngine;

[CreateAssetMenu(menuName = "Settings/TestSettings")]
public class TestSettings : ScriptableObject
{
    public bool BoxCollidersVisible = false;
    public Sprite BoxColliderSprite;
}