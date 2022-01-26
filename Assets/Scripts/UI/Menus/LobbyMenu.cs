using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : Menu<LobbyMenu>
{
    [SerializeField] private Toggle Player1Ready;
    [SerializeField] private Toggle Player2Ready;
    [SerializeField] private CountdownTimer timer;

    private bool player1IsReady;
    private bool player2IsReady;


    protected override void Awake()
    {
        Debug.Log("LobbyMenu Awake");

        // Call the Awake() function of Menu
        base.Awake();

        player1IsReady = false;
        player2IsReady = false;
    }


    public void Player1TogglePressed()
    {
        // If the toggle has just been activated
        if (Player1Ready.isOn == true)
        {
            // Set Player1 readystate to true
            player1IsReady = true;

            // If Player2 is also ready, start countdown
            if (player2IsReady)
                timer.BeginCountDown();
        }

        // If the toggle has just been deactivated
        else if (Player1Ready.isOn == false)
        {
            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
                timer.StopCountDown();

            // Set Player1's readystate to false
            player1IsReady = false;
        }
    }


    public void Player2TogglePressed()
    {
        // If the toggle has just been activated
        if (Player2Ready.isOn == true)
        {
            // Set Player2 readystate to true
            player2IsReady = true;

            // If Player1 is also ready, start countdown
            if (player1IsReady)
                timer.BeginCountDown();
        }

        // If the toggle has just been deactivated
        else if (Player2Ready.isOn == false)
        {
            // If countdown is currently occurring, cancel countdown
            if (timer.TimerStarted())
                timer.StopCountDown();

            // Set Player2's readystate to false
            player2IsReady = false;
        }
    }
}