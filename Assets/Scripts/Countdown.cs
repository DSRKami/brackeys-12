using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    public float startTime = 500;
    [SerializeField] private float currentTime;
    private Boolean countingDown = false;

    public int levelToSwitch;

    public Color startColour = Color.white;
    public Color endColour = Color.red;

    public Boolean shouldBlink = true;
    [Range(0,10)] public float baseBlinkSpeed = 1;
    public float startBlinking = 10;

    public LevelLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        startCountdown();
        countdownText.color = startColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (countingDown)
        {
            currentTime -= Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = (int) ((currentTime - Mathf.Floor(currentTime)) * 100);

        if(shouldBlink && currentTime <= startBlinking)
        {
            float blinkSpeed = baseBlinkSpeed - currentTime;
            countdownText.color = Color.Lerp(startColour, endColour, Mathf.PingPong(Time.time * blinkSpeed, 1));
        }

        if(currentTime > 10)
        {
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            countdownText.text = string.Format("{0:00}.{1:00}", seconds, milliseconds);
        }

        if (currentTime <= 0)
        {
            countdownEndTrigger();
        }
        
    }

    public void countdownEndTrigger()
    {
        // Other stuff
        shouldBlink = false;
        countdownText.color = endColour;
        stopCountdown();

        goToPlanet();
    }

    public void goToPlanet()
    {
        if (loader != null)
        {
            loader.LoadLevel(levelToSwitch);
        }
    }
    public void startCountdown()
    {
        currentTime = startTime; 
        countingDown = true;
    }

    public void stopCountdown() {
        countingDown = false;
        currentTime = 0;
    }

    public void toggleCountdown()
    {
        countingDown = !countingDown;
    }
}
