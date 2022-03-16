using UnityEngine;

[CreateAssetMenu(menuName = "VolumeSettings")]
public class VolumeSettings : ScriptableObject
{
    public float MusicVolume, EffectsVolume;

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        AudioManager.Instance.Music.volume = value;
    }

    public void SetEffectsVolume(float value)
    {
        EffectsVolume = value;
        AudioManager.Instance.Sfx.volume = value;
    }
}