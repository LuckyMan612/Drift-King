using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI timerCounter;
    public TextMeshProUGUI endTime;

    public TimeSpan timePlaying;
    public bool timerGoing;
    public string timePlayingString;

    public float elapsedTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        timerCounter.text = "00:00.00";
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        endTime.text = timePlayingString;
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingString = timePlaying.ToString("mm':'ss'.'ff");
            timerCounter.text = timePlayingString;

            yield return null;
        }
    }
}
