using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text CountDownText;
    [SerializeField] private Image CountDownTextBackground;

    // Time remaining in seconds
    [SerializeField] private int timerLength;
    int timeRemaining;

    private bool timerStarted;
    private CancellationTokenSource cts;


    private void Start()
    {
        timerStarted = false;
        cts = new CancellationTokenSource();

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
        CountDownText.text = $"{timerLength}";

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

            EventManager.Instance.Notify(Event.EventTypes.GameStart);
        }
    }


    // "async Task" represents a single operation that can run asynchronously
    private async Task TimerTick(CancellationToken ctkn)
    {
        await Task.Delay(1000, ctkn);

        // Update timeRemaining and timerText
        timeRemaining -= 1;
        CountDownText.text = $"{timeRemaining}";
    }


    public void StopCountDown()
    {
        timerStarted = false;
        HideCountDown();

        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
    }


    private void ShowCountDown()
    {
        CountDownText.gameObject.SetActive(true);
        CountDownTextBackground.gameObject.SetActive(true);
    }


    private void HideCountDown()
    {
        CountDownText.gameObject.SetActive(false);
        CountDownTextBackground.gameObject.SetActive(false);
    }
}