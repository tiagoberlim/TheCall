using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Class that contains the Telephone's functionality.
/// </summary>
public class Telephone : MonoBehaviour
{
    /// <summary>
    /// Key codes of numeric keys.
    /// </summary>
    private KeyCode[] keyCodes = new KeyCode []{ KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };


    [Header("Arduino Settings")]

    /// <summary>
    /// Reference to the <c>SerialController</c> that connects to the Arduino.
    /// </summary>
    [SerializeField] private SerialController   _serialController;


    [Header("Audio Sources")]

    /// <summary>
    /// Reference to the <c>AudioSource</c> responsible for playing computer
    /// related sounds.
    /// </summary>
    [SerializeField] private AudioSource        _computerAudioSource;


    [Header("Audio Clips")]

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the dial up sound that is 
    /// played when a valid contact was dialed.
    /// </summary>
    [SerializeField] private AudioClip          _correctNumberDialUp;

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the dial up sound that is
    /// played when a invalid contact was dialed.
    /// </summary>
    [SerializeField] private AudioClip          _wrongNumberDialUp;

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the notification sound that
    /// is played when a invalid contact was dialed.
    /// </summary>
    [SerializeField] private AudioClip          _wrongNumberAudioNotification;

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the sound that is played
    /// when a call ends.
    /// </summary>
    [SerializeField] private AudioClip          _endCallLoopSound;

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the morse code sound that
    /// is played in the first received call.
    /// </summary>
    [SerializeField] private AudioClip          _morseCodeAudio;

    /// <summary>
    /// Reference to the <c>AudioClip</c> containing the error sound that is
    /// played when an error pops up.
    /// </summary>
    [SerializeField] private AudioClip          _errorSound;


    [Header("Contacts")]

    /// <summary>
    /// The number called for the demo to end.
    /// </summary>
    [SerializeField] private string             _lastNumber;

    /// <summary>
    /// List of valid contacts.
    /// </summary>
    [SerializeField] private List<PhoneNumber>  _validContacts;


    [Header("Extras")]

    /// <summary>
    /// Reference to the GameObject that represents an error popup.
    /// </summary>
    [SerializeField] private GameObject _errorPopUp;


    /// <summary>
    /// Reference to the <c>AudioSource</c> responsible for playing telephone
    /// related sounds.
    /// </summary>
    private AudioSource     _telephoneAudioSource;

    /// <summary>
    /// <c>StringBuilder</c> responsible for maintaining the Player's input.
    /// </summary>
    private StringBuilder   _sb;

    /// <summary>
    /// Audio recording played during the last call.
    /// </summary>
    private AudioClip       _lastRecordingPlayed;

    /// <summary>
    /// Last dial up sound played.
    /// </summary>
    private AudioClip       _lastDialUpRecordingPlayed;

    /// <summary>
    /// Wether or not the telephone is ringing.
    /// </summary>
    private bool            _isRinging;

    /// <summary>
    /// Wether or not there is a on going call.
    /// </summary>
    private bool            _onGoingCall;

    /// <summary>
    /// Wether or not the first call has been received.
    /// </summary>
    private bool            _firstCallReceived;

    /// <summary>
    /// Wether or not the telephone's handset is up.
    /// </summary>
    private bool            _isHandSetUp;

    /// <summary>
    /// Wether or not the last call has been received.
    /// </summary>
    private bool            _lastCallReceived;

    /// <summary>
    /// Wether or not the game has ended.
    /// </summary>
    private bool            _gameEnded;
    

    /// <summary>
    /// Initializes all the instance variables and clears the Player's input.
    /// </summary>
    private void Awake()
    {
        _telephoneAudioSource       = GetComponent<AudioSource>();
        _sb                         = new StringBuilder(10);
        _lastRecordingPlayed        = null;
        _lastDialUpRecordingPlayed  = null;
        _isRinging                  = false;
        _onGoingCall                = false;
        _firstCallReceived          = false;
        _isHandSetUp                = false;
        _lastCallReceived           = false;
        _gameEnded                  = false;

        ClearCurrentInput();
    }

    // ---------------------------------------------------------------------------------------------
    // Uncomment the Update method to call a number using the numeric keys of 
    // the keyboard. 

    // private void Update()
    // {
    //     for( int i = 0 ; i < keyCodes.Length ; ++i )
    //     {
    //         if(Input.GetKeyDown(keyCodes[i]))
    //         {
    //             AddInputToCurrentTry(i.ToString());
    //         }
    //     }

    //     //if (Input.GetKeyDown(KeyCode.Return)) _serialController.SendSerialMessage("ring");
    // }
    // ---------------------------------------------------------------------------------------------

    /// <summary>
    /// Process the Player's input.
    /// </summary>
    /// <param name="input"></param>
    private void ProcessPhoneInput(string input)
    {
        Debug.Log(input.Length);
        if (input == "down") // If handset was put back in place
        {
            if (_onGoingCall) // If there was an on going call
            {
                EndCall();
                return;
            }
            else
            {
                _isHandSetUp = false;
                ClearCurrentInput();
            }
        }
        else if (_onGoingCall && !_isRinging) return; // If there is an on going call ignore input
        
        Debug.Log("Made it past first process input checks | " + Time.time);
        if (input.Length > 1)
        {
            if (_isRinging && input == "up") // Picked up a call
            {
                _isRinging = false;
                _isHandSetUp = true;
                Debug.Log("Picked up when phone was ringing");
            }
            else if (input == "up") // Ready to receive inputs
            {
                Debug.Log("Picked up when phone was not ringing");
                _isHandSetUp = true;
            }
        }
        else
        {
            if (int.TryParse(input, out _) || input == "*")
                AddInputToCurrentTry(input);
            else
                ReplayLastRecording();
        }
    }

    /// <summary>
    /// Adds input to Player's current attempt of contacting someone.
    /// </summary>
    /// <param name="number">The number to be added to the current attempt.</param>
    private void AddInputToCurrentTry(string number)
    {
        _sb.Append(number);
        Debug.Log($"SB current length is: {_sb.Length} | Current string is: {_sb.ToString()}");

        if (_sb.Length == 10) // An entire number has been received
        {
            Debug.Log("SB's length reached 10!");
            CheckForValidContact();
        }
    }

    /// <summary>
    /// Checks if the given numbers corresponds to a valid contact.
    /// </summary>
    private void CheckForValidContact()
    {
        bool foundValidContact = false;

        foreach(PhoneNumber contact in _validContacts)
        {
            if (contact.Number == _sb.ToString()) // Found a valid contact
            {
                if (contact.Number == _lastNumber)
                {
                    _lastCallReceived = true;
                }
                else if (contact.Number == "3059275410") // Called the Hotline easter egg
                {
                    foundValidContact = true;
                    StartCoroutine(PlayAudioRecording(contact.AudioRecording, _wrongNumberDialUp));
                    break;
                }

                foundValidContact = true;
                StartCoroutine(PlayAudioRecording(contact.AudioRecording, _correctNumberDialUp));
                break;
            }
        }

        if (!foundValidContact) // Couldn't find a valid contact
        {
            Debug.Log("Couldn't find a valid contact");
            StartCoroutine(PlayAudioRecording(_wrongNumberAudioNotification, _wrongNumberDialUp));
        }

        ClearCurrentInput();
    }

    /// <summary>
    /// Clears the Player's current attempt.
    /// </summary>
    private void ClearCurrentInput()
    {
        Debug.Log($"Clearing input... [{Time.time}]");
        _sb.Clear();
        Debug.Log($"SB new length is: {_sb.Length} | Current string is: {_sb.ToString()} | [{Time.time}]");
    }

    /// <summary>
    /// Ends the current call.
    /// </summary>
    private void EndCall()
    {
        StopAllCoroutines();
        ResetAudioSource();
        ClearCurrentInput();
        _onGoingCall = false;
        _isHandSetUp = false;

        if (_lastCallReceived) // If the call was the last call => END THE GAME
        {
            _gameEnded = true;
            _errorPopUp.SetActive(true);
            _computerAudioSource.PlayOneShot(_errorSound);
        }
    }

    /// <summary>
    /// Resets the telephone's <c>AudioSource</c>.
    /// </summary>
    private void ResetAudioSource()
    {
        _telephoneAudioSource.Stop();
        _telephoneAudioSource.clip = null;
        _telephoneAudioSource.loop = false;
    }

    /// <summary>
    /// Replay last call's audio recording.
    /// </summary>
    private void ReplayLastRecording()
    {
        if (_lastRecordingPlayed != null && _lastDialUpRecordingPlayed != null)
            StartCoroutine(PlayAudioRecording(_lastRecordingPlayed, _lastDialUpRecordingPlayed));
    }

    /// <summary>
    /// Plays the audio recording correspondent to the dialed up number.
    /// </summary>
    /// <param name="audioRecording">The audio recording to be played.</param>
    /// <param name="dialUpRecording">The dial up sound to be played.</param>
    /// <returns>A yield instruction.</returns>
    private IEnumerator PlayAudioRecording(AudioClip audioRecording, AudioClip dialUpRecording)
    {
        _onGoingCall = true;
        _lastRecordingPlayed = audioRecording;
        _lastDialUpRecordingPlayed = dialUpRecording;

        _telephoneAudioSource.PlayOneShot(dialUpRecording);
        yield return new WaitForSeconds(dialUpRecording.length + 0.5f);

        _telephoneAudioSource.PlayOneShot(audioRecording);
        yield return new WaitForSeconds(audioRecording.length + 1.0f);

        _telephoneAudioSource.clip = _endCallLoopSound;
        _telephoneAudioSource.loop = true;
        _telephoneAudioSource.Play();
    }

    /// <summary>
    /// The first call to be received by the Player.
    /// </summary>
    /// <returns>A yield instruction.</returns>
    private IEnumerator FirstCall()
    {
        _firstCallReceived = true;

        yield return (new WaitForSeconds(10.0f));

        // We need to check if the handset was up to prevent the first call to
        // never be played.
        if (_isHandSetUp)
        {
            while (_isHandSetUp)
            {
                Debug.Log("Trying to make first call but telephone is up");
                yield return null;
            }
            Debug.Log("Handset down, going to make first call");
        }
        
        // Tell Arduino to start ringing
        _serialController.SendSerialMessage("ring");

        _onGoingCall = true;
        _isRinging = true;

        // Wait for the player to pick up the call
        while (_isRinging)
        {
            //Debug.Log("Waiting for call to be picked up");
            yield return null;
        }

        //Debug.Log("Call has been accepted");
        yield return (new WaitForSeconds(2.5f)); // Delay to give player time to get handset close

        _telephoneAudioSource.PlayOneShot(_morseCodeAudio);
        _lastRecordingPlayed = _morseCodeAudio;
        _lastDialUpRecordingPlayed = _correctNumberDialUp;

        yield return new WaitForSeconds(_morseCodeAudio.length + 2.0f);

        _telephoneAudioSource.clip = _endCallLoopSound;
        _telephoneAudioSource.loop = true;
        _telephoneAudioSource.Play();
    }

    /// <summary>
    /// Triggers the first call to be received by the player.
    /// </summary>
    public void ReceiveFirstCall()
    {
        if (!_firstCallReceived) StartCoroutine(FirstCall());
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        if (_gameEnded) return;
        //Debug.Log("Message arrived: " + msg);
        ProcessPhoneInput(msg);
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
