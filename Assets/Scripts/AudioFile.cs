
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Class that represents an audio file to be played by the <c>MediaPlayer</c>
/// </summary>
public class AudioFile : MonoBehaviour
{
    /// <summary>
    /// Contains information about wether this <c>AudioFile</c> is being played
    /// or not.
    /// </summary>
    public bool IsPlaying => _videoPlayer.isPlaying;

    /// <summary>
    /// Reference to the <c>MediaPlayer</c> timestamp slider.
    /// </summary>
    [SerializeField] private Slider _slider;

    /// <summary>
    /// Reference to the <c>VideoPlayer</c> where the audio is being played.
    /// </summary>
    private VideoPlayer _videoPlayer;

    /// <summary>
    /// Initializes all the instance variables.
    /// </summary>
    private void Awake()
    {
        _videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        if (_videoPlayer.isPlaying) _slider.value = _videoPlayer.frame;
    }

    /// <summary>
    /// Called when the GameObject this script is attached to is enabled.
    /// </summary>
    private void OnEnable()
    {
        _slider.maxValue = _videoPlayer.frameCount;
        Debug.Log(_slider.maxValue.ToString());
    }

    /// <summary>
    /// Changes the current frame of the <c>VideoPlayer</c> where the audio is
    /// being played.
    /// </summary>
    /// <param name="frame"></param>
    public void ChangeAudioPlayStamp(int frame)
    {
        _videoPlayer.frame = frame;
    }

    /// <summary>
    /// Starts playing the audio.
    /// </summary>
    public void PlayAudio()
    {
        _videoPlayer.Play();
    }

    /// <summary>
    /// Pauses the audio.
    /// </summary>
    public void PauseAudio()
    {
        _videoPlayer.Pause();
    }

    /// <summary>
    /// Stops playing the audio.
    /// </summary>
    public void StopAudio()
    {
        _videoPlayer.Stop();
    }
}
