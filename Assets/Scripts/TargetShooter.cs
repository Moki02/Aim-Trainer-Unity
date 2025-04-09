using UnityEngine;
using TMPro;
using System;

public class TargetShooter : MonoBehaviour
{
    public static Action OnTargetMissed;
	public static int TotalShotsFired { get; private set; }
    public static int SuccessfulHits { get; private set; }
    public static float TotalAccuracyScore { get; private set; }
    
    [Header("References")]
    [SerializeField] Camera cam;
    [SerializeField] KillFeed killFeed;
    [SerializeField] float hitRadius = 0.5f;

    // Accuracy tracking variables
    private float totalDotProduct;
    private int successfulHits;

    void Update()
    {
        if (Timer.GameEnded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit; // Declare hit variable here
            
            if (Physics.SphereCast(ray, hitRadius, out hit, Mathf.Infinity))
            {
                Target target = hit.collider.GetComponent<Target>();
                
                if (target != null)
                {
                    // Successful hit
                    target.Hit();
                    LogSuccessfulShot(hit); // Pass the hit data
                }
                else
                {
                    OnTargetMissed?.Invoke();
                    LogMissedShot(ray.origin);
                }
            }
            else
            {
                OnTargetMissed?.Invoke();
                LogMissedShot(ray.origin);
            }
        }
    }

    void LogSuccessfulShot(RaycastHit hit) // Now receives hit as parameter
    {
        Vector3 playerPos = cam.transform.position;
        Vector3 hitPoint = hit.point;
        Vector3 direction = (hitPoint - playerPos).normalized;
        
        // Calculate accuracy using dot product
        float dotProduct = Vector3.Dot(direction, hit.normal);
        float accuracyPercent = (dotProduct + 1) / 2 * 100; // Convert to 0-100%
        
        // Track for averages
       SuccessfulHits++;
        TotalAccuracyScore += accuracyPercent;
        TotalShotsFired++;

        // Format vectors
        string playerPosStr = FormatVector(playerPos);
        string directionStr = FormatVector(direction);
        
        killFeed.Log($"HIT! Accuracy: {accuracyPercent:F0}%\nL(t) = {playerPosStr} + t{directionStr}");
    }

    void LogMissedShot(Vector3 origin)
    {
        Vector3 direction = cam.transform.forward;
        string originStr = FormatVector(origin);
        string directionStr = FormatVector(direction);
		TotalShotsFired++;
        
        killFeed.Log($"MISS: L(t) = {originStr} + t{directionStr}");
    }

    string FormatVector(Vector3 v)
    {
        return $"({v.x:F1}, {v.y:F1}, {v.z:F1})";
    }

    // Debug visualization
    void OnDrawGizmos()
    {
        if (cam != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 rayStart = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            Gizmos.DrawWireSphere(rayStart + cam.transform.forward * 2f, hitRadius);
        }
    }
	// Add this to TargetShooter.cs
public static void ResetStats()
{
    TotalShotsFired = 0;
    SuccessfulHits = 0;
    TotalAccuracyScore = 0;
    ScoreCounter.ResetScore();
    MissCounter.ResetMisses();
    OnTargetMissed = null;
}
}