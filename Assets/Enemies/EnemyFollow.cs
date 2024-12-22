using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target; // Target to follow
    public float speed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = Random.Range(0.009f, 0.03f);
    }

    void Update()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

        // Calculate the direction to the target, but ignore the Y-axis for rotation
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Lock Y-axis to prevent tipping over

        // Check if the direction is non-zero to avoid errors
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }
}
