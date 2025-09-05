using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class BirdAI : MonoBehaviour
{
    public NeuralNetwork brain;
    public int genomeIndex = -1;
    public float gravity = -20f;
    public float strength = 6f;

    private Vector3 direction;
    private SpriteRenderer sr;

    [HideInInspector] public bool isAlive = true;
    [HideInInspector] public int pipesPassed = 0;
    [HideInInspector] public float timeAlive = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Initialize(NeuralNetwork brain, int index)
    {
        this.brain = brain;
        this.genomeIndex = index;
        isAlive = true;
        pipesPassed = 0;
        timeAlive = 0f;
        direction = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
        sr.enabled = true;
    }

    private void Update()
    {
        if (!isAlive) return;

        // Find nearest pipe
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        Pipes nearest = null;
        float nearestX = float.MaxValue;

        foreach (var p in pipes)
        {
            if (p.transform.position.x > transform.position.x && p.transform.position.x < nearestX)
            {
                nearestX = p.transform.position.x;
                nearest = p;
            }
        }

        float[] inputs = new float[4];

        if (nearest != null)
        {
            // Use the score trigger (gap center) instead of pipe root
            Transform gap = nearest.transform.Find("ScoreGate");
            float dx = nearest.transform.position.x - transform.position.x;
            float dy = (gap != null ? gap.position.y : nearest.transform.position.y) - transform.position.y;

            inputs[0] = transform.position.y;
            inputs[1] = dx;
            inputs[2] = dy;
            inputs[3] = direction.y;
        }

        // Neural net decides
        float output = brain.FeedForward(inputs);
        if (output > 0.5f)
        {
            Flap();
        }

        // Apply physics
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Rotate bird for visuals
        float angle = Mathf.Clamp(direction.y * 5f, -90f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        timeAlive += Time.deltaTime;
    }

    private void Flap()
    {
        direction.y = strength;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;

        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (other.CompareTag("Scoring"))
        {
            pipesPassed++;
        }
    }

    public void Die()
    {
        isAlive = false;
        sr.enabled = false;
        EvolutionManager.Instance.ReportDeath(genomeIndex, pipesPassed, timeAlive);
    }
}
