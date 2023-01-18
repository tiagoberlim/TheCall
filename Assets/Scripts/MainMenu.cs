using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Class that holds the initial menu's functionality.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Reference to the GameObject containing the player's name.
    /// </summary>
    [SerializeField] GameObject _username;

    /// <summary>
    /// Reference to the GameObject containing the input field for the password.
    /// </summary>
    [SerializeField] GameObject _passwordInputObject;

    /// <summary>
    /// Reference to the GameObject containing the wrong password popup.
    /// </summary>
    [SerializeField] GameObject _wrongPasswordObj;

    /// <summary>
    /// Reference to the input field.
    /// </summary>
    [SerializeField] TMP_InputField _passwordTxt;

    /// <summary>
    /// Reference to the <c>DesktopLoader</c> responsible for the transition
    /// between scenes.
    /// </summary>
    [SerializeField] DesktopLoader _desktopLoader;

    /// <summary>
    /// Loads next scene.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Loads the Menu.
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame ()
    {
        Application.Quit();
    }

    /// <summary>
    /// Activates the input field where the Player inserts the password.
    /// </summary>
    public void AskForPassword()
    {
        _username.SetActive(false);
        _passwordInputObject.SetActive(true);
    }

    /// <summary>
    /// Checks if the given password is correct. If it is, loads the next scene.
    /// If the password is incorrect activates the wrong password popup.
    /// </summary>
    public void CheckPassword()
    {
        if (_passwordTxt.text == "TheCall")
        {
            _passwordInputObject.SetActive(false);
            _desktopLoader.LoadDesktop(1);
        }
        else _wrongPasswordObj.SetActive(true);
    }
}
