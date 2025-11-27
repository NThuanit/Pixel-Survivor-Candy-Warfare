using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable Objects/New Sound", order = 0)]
public class SoundSO : ScriptableObject
{
    public enum AuioTypes
    {
        SFX,
        Music
    }

    public AuioTypes AudioType;
    public AudioClip AudioClip;
    public bool Loop = false;
    public bool RandomizePitch = false;
    [RangeAttribute(0f, 1f)]
    public float RandomPitchRangeModifier = 0.1f;
    [RangeAttribute(0.1f, 2f)]
    public float Volume = 1f;
    [Range(0.1f, 3f)]
    public float Pitch = 1f;
}
