using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoringTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BirdAI bird = other.GetComponent<BirdAI>();
        if (bird != null && bird.isAlive)
        {
            bird.pipesPassed++;
        }
    }
}
