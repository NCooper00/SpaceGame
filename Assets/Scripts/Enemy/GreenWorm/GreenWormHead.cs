using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWorm : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private float segmentDistance = 0.5f; // Distance between segments

    [SerializeField] private GameObject[] segmentPrefabs; // Array of segment prefabs

    private Transform player;
    private Queue<Vector3> positions; // Queue to store positions of the segments
    private List<Transform> segments; // List to store instantiated segment transforms

    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        positions = new Queue<Vector3>();
        segments = new List<Transform>();

        // Instantiate the segments at the head's position
        foreach (var prefab in segmentPrefabs)
        {
            GameObject segment = Instantiate(prefab, transform.position, Quaternion.identity);
            segments.Add(segment.transform);
            positions.Enqueue(segment.transform.position);
        }
    }

[SerializeField] private float initialSegmentDistance = 0.3f; // Initial distance between head and first segment

void Update()
{
    // Move the head towards the player
    var direction = player.position - transform.position;
    transform.up = Vector3.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);
    transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);

    // Update the positions of the segments
    UpdateSegments();
}

void UpdateSegments()
{
    // Store the current position of the head
    positions.Enqueue(transform.position);

    // Remove the oldest position if the queue exceeds the number of segments
    if (positions.Count > segments.Count + 1)
    {
        positions.Dequeue();
    }

    // Update the positions and rotations of the segments based on the stored positions
    for (int i = 0; i < segments.Count; i++)
    {
        Transform currentSegment = segments[i];
        Transform previousSegment = i == 0 ? transform : segments[i - 1];

        // Calculate the target position for the current segment
        float distance = i == 0 ? initialSegmentDistance : segmentDistance;
        Vector3 targetPosition = previousSegment.position - previousSegment.up * distance;

        // Move the segment towards the target position
        currentSegment.position = Vector3.MoveTowards(currentSegment.position, targetPosition, speed * Time.deltaTime);

        // Rotate the segment to face the previous segment
        Vector3 directionToPrevious = previousSegment.position - currentSegment.position;
        if (directionToPrevious != Vector3.zero)
        {
            currentSegment.up = Vector3.Lerp(currentSegment.up, directionToPrevious, rotationSpeed * Time.deltaTime);
        }
    }
}

}
