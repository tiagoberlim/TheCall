using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

/// <summary>
/// Class that represents the cutscene played at the end of the game. 
/// </summary>
public class FinalCutscene : MonoBehaviour
{
    [Header("Audio Parameters")]
    [SerializeField] private AudioSource _computerAudioSource;
    [SerializeField] private AudioClip _errorSound;
    [SerializeField] private AudioClip _shutdownSound;

    [Header("Glitch Effects")]
    [SerializeField] private AnalogGlitch _analogGlitch;
    [SerializeField] private DigitalGlitch _digitalGlitch;

    [Header("Errors")]
    [SerializeField] private GameObject _error1;
    [SerializeField] private GameObject _error2;
    [SerializeField] private GameObject _error3;
    [SerializeField] private GameObject _error4;
    [SerializeField] private GameObject _error5;
    [SerializeField] private GameObject _error6;
    [SerializeField] private GameObject _error7;
    [SerializeField] private List<GameObject> _errors;

    [Header("End Screen Parameters")]
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private CanvasGroup _endDemo;

    /// <summary>
    /// Contains info on wether the final cutscene has started.
    /// </summary>
    private bool _cutsceneStarted;

    /// <summary>
    /// Initializes the instance variables.
    /// </summary>
    private void Awake()
    {
        _cutsceneStarted = false;
    }

    /// <summary>
    /// Triggers the final custcene.
    /// </summary>
    public void PlayCutscene()
    {
        if (!_cutsceneStarted) StartCoroutine(FinalCutScene());
    }

    /// <summary>
    /// Activates an error's visual effects and plays an error sound.
    /// </summary>
    /// <param name="error">The error to be activated.</param>
    private void ActivateError(GameObject error)
    {
        error.SetActive(true);
        _computerAudioSource.PlayOneShot(_errorSound);
    }

    /// <summary>
    /// The final cutscene. Activates all errors, with a delay between each
    /// activation. At the end triggers the end demo screen.
    /// </summary>
    /// <returns>A yield instruction.</returns>
    private IEnumerator FinalCutScene()
    {
        _cutsceneStarted = true;

        yield return new WaitForSeconds(2.5f);
        ActivateError(_error1);

        yield return new WaitForSeconds(2.0f);
        ActivateError(_error2);

        yield return new WaitForSeconds(1.0f);
        ActivateError(_error3);

        yield return new WaitForSeconds(0.75f);
        ActivateError(_error4);

        yield return new WaitForSeconds(0.3f);
        ActivateError(_error5);

        yield return new WaitForSeconds(0.15f);
        ActivateError(_error6);

        yield return new WaitForSeconds(0.10f);
        ActivateError(_error7);

        foreach (GameObject error in _errors)
        {
            ActivateError(error);
            yield return new WaitForSeconds(0.1f);
        }

        _analogGlitch.scanLineJitter = 0.364f;
        _analogGlitch.verticalJump = 0.048f;
        _analogGlitch.horizontalShake = 0.024f;

        _digitalGlitch.intensity = 0.025f;

        yield return new WaitForSeconds(3.0f);

        _endScreen.SetActive(true);
        _computerAudioSource.PlayOneShot(_shutdownSound);
        _analogGlitch.scanLineJitter = 0.0f;
        _analogGlitch.verticalJump = 0.0f;;
        _analogGlitch.horizontalShake = 0.0f;;

        _digitalGlitch.intensity = 0.0f;;

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(FadeInEndDemoScreen());
    }

    /// <summary>
    /// Fades in the end demo screen.
    /// </summary>
    /// <returns>A yield instruction.</returns>
    private IEnumerator FadeInEndDemoScreen()
    {
        float duration = 2.0f;
        float elapsedTime = 0.0f;

        _endDemo.gameObject.SetActive(true);
        while (elapsedTime < duration)
        {
            _endDemo.alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            Debug.Log($"Alpha = {_endDemo.alpha} | Elapsed time = {elapsedTime.ToString()}");
            yield return null;
        }
        _endDemo.alpha = 1.0f;
    }
}
