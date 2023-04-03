using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    //cowritten with ChatGPT

    public bool initialized = false;

    public float time;

    public float timeScale;
    
    public bool ticking;

    public enum currentDay
    {
        Day1,
        Day2,
        Day3,
        Night1,
        Night2,
        Night3
    }
    public currentDay currentDayTracker;

    public enum TimeOfDay
    {
        Morning,
        Midday,
        Evening,
        Night
    }
    public TimeOfDay currentTimeOfDay;

    private const float MinutesPerHour = 60;
    private const float HoursPerDay = 24;
    private const float MinutesPerDay = MinutesPerHour * HoursPerDay;

    public void StartGame()
    {
        time = 0;
        currentTimeOfDay = TimeOfDay.Morning;
        ticking = true;
        initialized = true;
        StartCoroutine(MarchOfTime());
    }

    IEnumerator MarchOfTime()
    {
        while (ticking)
        {
            time++;
            if (time >= MinutesPerDay)
            {
                time = 0;
            }

            if (time >= 10 * MinutesPerHour && time < 14 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Midday;
            }
            else if (time >= 14 * MinutesPerHour && time < 20 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Evening;
            }
            else if (time >= 20 * MinutesPerHour || time < 4 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Night;
            }
            else
            {
                currentTimeOfDay = TimeOfDay.Morning;
            }

            if (time % 1440 == 0)
            {
                currentDayTracker++;
            }

            yield return new WaitForSeconds(1*timeScale);
        }
    }

    public string GetFormattedTime()
    {
        float realTime = time;
        int hours = Mathf.FloorToInt(time / MinutesPerHour);
        int minutes = Mathf.FloorToInt(time % MinutesPerHour);
        
        //cool
        return string.Format("{0:00}:{1:00}", hours, minutes+" ("+realTime+")");
    }
}
