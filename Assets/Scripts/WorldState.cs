using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
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

    public void Start()
    {
        time = 0;
        currentTimeOfDay = TimeOfDay.Morning;
        ticking = true;
        StartCoroutine(MarchOfTime());
    }

    private void Update()
    {
        Debug.Log(GetFormattedTime());
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

            if (time >= 6 * MinutesPerHour && time < 12 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Midday;
            }
            else if (time >= 12 * MinutesPerHour && time < 18 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Evening;
            }
            else if (time >= 18 * MinutesPerHour || time < 6 * MinutesPerHour)
            {
                currentTimeOfDay = TimeOfDay.Night;
            }
            else
            {
                currentTimeOfDay = TimeOfDay.Morning;
            }

            if (time % 720 == 0)
            {
                currentDayTracker++;
            }

            yield return new WaitForSeconds(1*timeScale);
        }
    }

    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(time / MinutesPerHour);
        int minutes = Mathf.FloorToInt(time % MinutesPerHour);
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
