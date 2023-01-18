using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for updating the current displayed date and time.
/// All info will be according to the system's current date and time except for
/// the year, which will always be 2004.
/// </summary>
public class Date : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI element where date and time will be displayed.
    /// </summary>
    public TextMeshProUGUI largeText;

    /// <summary>
    /// Variable that contains the current delay between checks for updating
    /// the date and time.
    /// </summary>
    private float seconds;

    /// <summary>
    /// Initializes the date and time.
    /// </summary>
    void Start()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString($"dd/MM/2004  HH:mm");
        largeText.text = time;
        seconds = 1.0f;
    }

    /// <summary>
    /// Updates the date and time every second.
    /// </summary>
    void Update()
    {
        // If delay has reached 0, update the date and time and reset the delay
        if (seconds <= 0.0f)
        {
            string time = System.DateTime.UtcNow.ToLocalTime().ToString($"dd/MM/2004  HH:mm");
            largeText.text = time;
            seconds = 1.0f;
        }
        else seconds -= Time.deltaTime; // Update the delay
    }
}
