using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is responsible to displaying the splash screen for the apporiate 
//amount of time
public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] private float splashScreenTime = 1.0f;
    private float timeSinceStart = 0; //time since the splash screen was displayed
    
    void Update()
    {
        //update the time
        timeSinceStart += Time.deltaTime;

        //if the apporiate time has passed, load the next scene
        if (timeSinceStart >= splashScreenTime)
        {
            //load the next scene
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
