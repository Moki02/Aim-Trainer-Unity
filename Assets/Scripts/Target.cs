using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using System;

public class Target : MonoBehaviour
{
	public static Action OnTargetHit; 
    [SerializeField] TMP_Text coordText;
    private Vector3 originalPosition;
    private float timeOffset;


    void Start()
    {
        originalPosition = GetRandomPosition(); // Changed from RandomizePosition
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        coordText.text = $"({transform.position.x:F1}, {transform.position.y:F1}, {transform.position.z:F1})";
        
        // Matrix-based movement
        float angle = (Time.time + timeOffset) * 30f;
        transform.position = originalPosition + new Vector3(
            Mathf.Sin(angle * Mathf.Deg2Rad) * 2f,
            0,
            Mathf.Cos(angle * Mathf.Deg2Rad) * 2f
        );
    }

    public void Hit()
    {
        originalPosition = GetRandomPosition(); 
        timeOffset = Random.Range(0f, 100f);
		OnTargetHit?.Invoke();
		
    }

    Vector3 GetRandomPosition() 
    {
        return new Vector3(
            Random.Range(-3f, 8f),
            Random.Range(-2f, 5f),
            Random.Range(-8f, 8f)
        );
    }
}