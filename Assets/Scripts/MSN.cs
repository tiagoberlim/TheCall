using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Class that represents the MSN application.
/// </summary>
public class MSN : MonoBehaviour
{
    /// <summary>
    /// Reference to the GameObject containing the MSN's page where we can
    /// search for contacts.
    /// </summary>
    [SerializeField] private GameObject     _msnSearchScreen;

    /// <summary>
    /// Reference to the GameObject containing the chat with the "charon"
    /// contact.
    /// </summary>
    [SerializeField] private GameObject     _charonScreen;

    /// <summary>
    /// Reference to the GameObject to be activated when given the corresponding
    /// key. This GameObject contains the image file.
    /// </summary>
    [SerializeField] private GameObject     _charonImageFile;

    /// <summary>
    /// Reference to the GameObject to be activated when given the corresponding
    /// key. This GameObject contains the audio file.
    /// </summary>
    [SerializeField] private GameObject     _charonAudioFile;

    /// <summary>
    /// Reference to the GameObject containing the chat box where messages are
    /// displayed.
    /// </summary>
    [SerializeField] private GameObject     _chatBox;

    /// <summary>
    /// Reference to the prefab that represents a message.
    /// </summary>
    [SerializeField] private GameObject     _messagePrefab;

    /// <summary>
    /// Reference to the input field where the user writes a new message.
    /// </summary>
    [SerializeField] private TMP_InputField _messageBox;

    /// <summary>
    /// Delay between giving a valid key and getting a response.
    /// </summary>
    private YieldInstruction wfs;

    /// <summary>
    /// Initializes the instance variables.
    /// </summary>
    private void Awake()
    {
        wfs = new WaitForSeconds(1);
    }

    /// <summary>
    /// Activates the chat with the "charon" contact.
    /// </summary>
    /// <param name="name">Contact's name.</param>
    public void ShowCharon(string name)
    {
        if (name == "charon")
        {
            _msnSearchScreen.SetActive(false);
            _charonScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Adds a message to the chat box.
    /// </summary>
    /// <param name="msgContent">The message's text.</param>
    /// <param name="user">The name of who sent the message.</param>
    private void AddMessageToChatBox(string msgContent, string user = "Player")
    {
        GameObject newTextMessage = Instantiate(_messagePrefab, _chatBox.transform);

        TextMeshProUGUI message = newTextMessage.GetComponent<TextMeshProUGUI>();

        message.text = $"{user}: {msgContent}";
    }

    /// <summary>
    /// Adds a message to the chat box.
    /// </summary>
    /// <param name="msgContent">The message's text.</param>
    public void AddMessageToChatBox(string msgContent)
    {
        GameObject newTextMessage = Instantiate(_messagePrefab, _chatBox.transform);

        TextMeshProUGUI message = newTextMessage.GetComponent<TextMeshProUGUI>();

        message.text = $"Player: {msgContent}";

        _messageBox.text = "";

        if (msgContent.ToLower() == "73627cp9/key") StartCoroutine(ShowResponse(_charonImageFile));
        else if (msgContent.ToLower() == "48179cp0/audio") StartCoroutine(ShowResponse(_charonAudioFile));
    }

    /// <summary>
    /// Gives a response to the Player when a valid key was given to the contact.
    /// </summary>
    /// <param name="objToActivate"></param>
    /// <returns></returns>
    private IEnumerator ShowResponse(GameObject objToActivate = null)
    {
        yield return wfs;
        AddMessageToChatBox("File sent.", "charon");

        if (objToActivate) objToActivate.SetActive(true);
    }
}
