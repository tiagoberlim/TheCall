using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that represents the Google search engine.
/// </summary>
public class GoogleBrowser : MonoBehaviour
{
    // All the references bellow correspond to a valid google search result's
    // corresponding window.
    [SerializeField] private GameObject morseCodeScreen;
    [SerializeField] private GameObject binaryCodeScreen;
    [SerializeField] private GameObject hubScreen;
    [SerializeField] private GameObject youtubeScreen;
    [SerializeField] private GameObject noResultScreen;
    [SerializeField] private GameObject miniClipScreen;
    [SerializeField] private GameObject facebookScreen;
    [SerializeField] private GameObject googleSearchScreen;

    /// <summary>
    /// Reference to the input field where the user searches for things.
    /// </summary>
    [SerializeField] private TMPro.TMP_InputField inputField;
    
    /// <summary>
    /// Dictionary containing valid search terms as the keys and the corresponding
    /// GameObject to be activated as the values.
    /// </summary>
    private IDictionary<string, GameObject> dict;

    /// <summary>
    /// Initializes the instance variables.
    /// </summary>
    private void Awake()
    {
        dict = new Dictionary<string, GameObject>
        {
            {"morse code", morseCodeScreen},
            {"binary code", binaryCodeScreen},
            {"pornhub", hubScreen},
            {"youtube", youtubeScreen},
            {"miniclip", miniClipScreen},
            {"facebook", facebookScreen}
        };        
    }

    /// <summary>
    /// Searches the given input. If the input is valid activates the corresponding
    /// result. Otherwise activates the error page.
    /// </summary>
    /// <param name="txt"></param>
    public void Search(string txt)
    {
        if (dict.TryGetValue(txt.ToLower(), out GameObject result))
        {
            result.SetActive(true);
            googleSearchScreen.SetActive(false);
            Debug.Log(txt.ToLower());
            inputField.text = "";
        }
        else
        {
            noResultScreen.SetActive(true);
            googleSearchScreen.SetActive(false);
            inputField.text = "";
        }
    }

    /// <summary>
    /// Searches for input when the "Search Button" is clicked. (NOT WORKING PROPERLY)
    /// </summary>
    public void SearchButton()
    {
        if (dict.TryGetValue(inputField.textComponent.text.ToLower(), out GameObject result))
        {
            result.SetActive(true);
            googleSearchScreen.SetActive(false);

        }
        else
        {
            // noResultScreen.SetActive(true);
            // googleSearchScreen.SetActive(false);
            Debug.Log($"Result not found | {inputField.textComponent.text.ToLower()}");
        }
        //Debug.Log(inputField.textComponent.text);
    }
}
