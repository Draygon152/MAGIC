using UnityEngine;
using UnityEngine.UI;
using System;
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
    private CancellationTokenSource canceller;


    private void Awake()
    {
        timerStarted = false;

        HideCountDown();
    }


    public bool TimerStarted()
    {
        return timerStarted;
    }


    // Entry point for asynchronous code. Return type of void allows for
    // method to wait for asynchronous methods to complete, but cannot
    // catch exceptions in the same way as "async Task" declared methods
    public async void BeginCountDown()
    {
        // Reset CountDownText to the length of the timer
        CountDownText.text = $"{timerLength}";

        // Reset value of timeRemaining to length of timer
        timeRemaining = timerLength;
        
        timerStarted = true;
        ShowCountDown();

        // Wait until task timerTick finishes before running timerTick again
        while (timeRemaining != 0)
            await TimerTick();

        Debug.Log("Countdown Ended");
    }


    // "async Task" represents a single operation that can run asynchronously
    private async Task TimerTick()
    {
        // Task not complete until 1 second has passed
        float nextSecondEndTime = Time.time + 1;

        while (Time.time < nextSecondEndTime)
            await Task.Yield();

        // Update timeRemaining and timerText
        timeRemaining -= 1;
        CountDownText.text = $"{timeRemaining}";
    }


    public void StopCountDown()
    {
        timerStarted = false;
        HideCountDown();
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