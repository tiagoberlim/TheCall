using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that represents the MediaPlayer.
/// </summary>
public class MediaPlayer : MonoBehaviour
{
    /// <summary>
    /// Reference to the reversed audio file.
    /// </summary>
    [SerializeField] private AudioFile  _reversedAudio;

    /// <summary>
    /// Reference to the normal audio file.
    /// </summary>
    [SerializeField] private AudioFile  _normalAudio;

    /// <summary>
    /// Reference to the GameObject that contains the resources to play the
    /// audio.
    /// </summary>
    [SerializeField] private GameObject _audioContainer;

    /// <summary>
    /// Reference to the current audio file being played.
    /// </summary>
    [SerializeField] private AudioFile  _currentAudio;

    /// <summary>
    /// Reference to the slider that represents the timestamps.
    /// </summary>
    [SerializeField] private Slider     _timeStampSlider;

    /// <summary>
    /// Change the audio file that is currently being played.
    /// </summary>
    public void ChangeAudioFile()
    {
        if (!_audioContainer.activeSelf) return;

        _currentAudio.StopAudio();
        _currentAudio.gameObject.SetActive(false);

        _currentAudio = _currentAudio == _reversedAudio ? _normalAudio : _reversedAudio;
        _currentAudio.gameObject.SetActive(true);
    }

    /// <summary>
    /// Changes the current frame of the audio file being played.
    /// </summary>
    /// <param name="frame">The frame to change into.</param>
    public void ChangeAudioPlayStamp(float frame)
    {
        if (_currentAudio.gameObject.activeSelf) _currentAudio.ChangeAudioPlayStamp((int)frame);
    }

    /// <summary>
    /// Plays the current audio file.
    /// </summary>
    public void PlayAudio()
    {
        if (_currentAudio.gameObject.activeSelf) _currentAudio.PlayAudio();
    }

    /// <summary>
    /// Pauses the current audio file.
    /// </summary>
    public void PausePlaying()
    {
        if (_currentAudio.gameObject.activeSelf) _currentAudio.PauseAudio();
    }

    /// <summary>
    /// Stops playing the current audio file.
    /// </summary>
    public void StopPlaying()
    {
        if (_currentAudio.gameObject.activeSelf) _currentAudio.StopAudio();
    }

    /// <summary>
    /// Closes the media player.
    /// </summary>
    public void CloseMediaPlayer()
    {
        if (_currentAudio.gameObject.activeSelf)
        {
            _currentAudio.StopAudio();
            _currentAudio = _reversedAudio;
        }
    }
}
