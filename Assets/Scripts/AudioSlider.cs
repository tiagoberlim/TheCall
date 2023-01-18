using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class that represents an audio slider to change the volume of a certain
/// <c>AudioMixer</c>.
/// </summary>
public class AudioSlider : MonoBehaviour
{
    /// <summary>
    /// Reference to the <c>AudioMixer</c> this slider controls.
    /// </summary>
    [SerializeField] private AudioMixer _audioMixer;

    /// <summary>
    /// Sets the new volume of the corresponding <c>AudioMixer</c>.
    /// </summary>
    /// <param name="newVolume">The new volume to be set.</param>
    public void SetVolume(float newVolume)
    {
        _audioMixer.SetFloat("Volume", Mathf.Log10(newVolume) * 20);
    }
}
