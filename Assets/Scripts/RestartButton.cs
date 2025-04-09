using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
	public void Restart()
	{
		Timer.ResetTimer();
        TargetShooter.ResetStats();
        
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
