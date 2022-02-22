// Written by Kevin Chao

using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countDownText; // Holds current time remaining for display
    [SerializeField] private Color textColor;
    [SerializeField] private Image countDownTextBackground;
    [SerializeField] private bool transparentBackground;
    [SerializeField] private EventTypes.Events eventToNotify; // What event to notify when countdown ends

    [SerializeField] private int timerLength; // Value that timer counts down from
    private int timeRemaining;                // Time remaining in seconds

    private bool timerStarted;
    private CancellationTokenSource cts;      // Used for cancelling async Tasks



    private void Awake()
    {
        timerStarted = false;
        cts = new CancellationTokenSource();

        if (transparentBackground)
        {
            Color bgColor = countDownTextBackground.color;
            bgColor.a = 0.0f;
            countDownTextBackground.color = bgColor;
        }

        countDownText.color = textColor;

        HideCountDown();
    }


    private void OnDisable()
    {
        cts.Cancel();
    }


    public bool TimerStarted()
    {
        return timerStarted;
    }


    // Async entry point
    public async void BeginCountDown()
    {
        Debug.Log("Countdown Started");

        // Reset CountDownText to the length of the timer
        countDownText.text = $"{timerLength}";

        timerStarted = true;
        ShowCountDown();

        // Get cancellation token
        CancellationToken ctkn = cts.Token;

        // Wait until task timerTick finishes before running timerTick again
        try
        {
            // Reset value of timeRemaining to length of timer
            for (timeRemaining = timerLength; timeRemaining > 0;)
                await TimerTick(ctkn);
        }

        catch
        {
            Debug.Log("Countdown Cancelled");
        }

        if (timeRemaining == 0)
        {
            Debug.Log("Countdown Finished");

            EventManager.Instance.Notify(eventToNotify);
        }
    }


    // "async Task" represents a single operation that can run asynchronously
    private async Task TimerTick(CancellationToken ctkn)
    {
        await Task.Delay(1000, ctkn);

        // Update timeRemaining and timerText
        timeRemaining -= 1;
        countDownText.text = $"{timeRemaining}";
    }


    // Stops current countdown
    public void StopCountDown()
    {
        timerStarted = false;
        HideCountDown();

        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
    }


    // Displays countdown UI elements
    private void ShowCountDown()
    {
        countDownText.gameObject.SetActive(true);
        countDownTextBackground.gameObject.SetActive(true);
    }


    // Hides countdown UI elements
    private void HideCountDown()
    {
        countDownText.gameObject.SetActive(false);
        countDownTextBackground.gameObject.SetActive(false);
    }
}