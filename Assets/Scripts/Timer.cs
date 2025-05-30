﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public static Action OnGameEnded;
	public static bool GameEnded { get; private set; }

	[SerializeField] TMP_Text timerText;

	float endTime;

	const float gameTime = 20f;

	void Start()
{
    TargetShooter.ResetStats(); 
    GameEnded = false;
    endTime = Time.time + gameTime;

	 Debug.Log("Stats reset - " +
             $"Shots: {TargetShooter.TotalShotsFired}, " +
             $"Hits: {TargetShooter.SuccessfulHits}, " +
             $"Misses: {MissCounter.Misses}");
}

	void Update()
	{
		if(GameEnded)
			return;

		float timeLeft = endTime - Time.time;

		if(timeLeft <= 0)
		{
			GameEnded = true;
			 Debug.Log("Game Ended Invoked");
			OnGameEnded?.Invoke();

			timeLeft = 0;
		}

		timerText.text = $"Time Left: {timeLeft.ToString("0.0")}";
	}

	public static void ResetTimer()
    {
        GameEnded = false;
        OnGameEnded = null; // Clear all subscribers
    }
}
