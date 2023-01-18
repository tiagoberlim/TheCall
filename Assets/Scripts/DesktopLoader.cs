using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the transition between the user login and the desktop.
/// </summary>
public class DesktopLoader : MonoBehaviour
{
    /// <summary>
    /// Reference to the GameObject containing the loading screen.
    /// </summary>
    public GameObject loadingScreen;

    /// <summary>
    /// Reference to the slider that contains information on the load status.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Activates the transition between scenes.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to be loaded.</param>
    public void LoadDesktop (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    /// <summary>
    /// Makes the transition between scenes.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to be loaded.</param>
    /// <returns>A yield instruction.</returns>
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }
}
