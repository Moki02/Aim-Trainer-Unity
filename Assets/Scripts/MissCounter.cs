using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissCounter : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	public static int Misses { get; private set; }

	void OnEnable()
	{
		TargetShooter.OnTargetMissed += OnTargetMissed;
	}

	void OnDisable()
	{
		TargetShooter.OnTargetMissed -= OnTargetMissed;
	}

	void OnTargetMissed()
	{
		Misses++;
		text.text = $"Misses: {Misses}";
	}
}
