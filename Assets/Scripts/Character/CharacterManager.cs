using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public GameObject Player;


    private void Awake()
    {
        Instance = this;
    }
}
