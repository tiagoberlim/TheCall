using UnityEngine;

/// <summary>
/// ScriptableObject that represents a valid phone number.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/PhoneNumber", fileName = "NewPhoneNumber")]
public class PhoneNumber : ScriptableObject
{
    /// <summary>
    /// The phone number.
    /// </summary>
    [SerializeField] private string     _number;

    /// <summary>
    /// Audio to be played when this number is contacted.
    /// </summary>
    [SerializeField] private AudioClip  _audioRecording;
    
    /// <summary>
    /// The phone number.
    /// </summary>
    public string Number            => _number;

    /// <summary>
    /// Audio to be played when this number is contacted.
    /// </summary>
    public AudioClip AudioRecording => _audioRecording;
}
