using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for controlling the current scene.
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
   public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
