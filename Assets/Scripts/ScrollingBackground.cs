using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Scrolling")]
    public float scrollSpeed = 2f;
    public bool useObstacleSpeed = true; 

    [Header("Setup")]
    public float backgroundWidth = 20f; 

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float currentSpeed = useObstacleSpeed ? Obstacle.globalSpeed : scrollSpeed;

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x <= startPosition.x - backgroundWidth)
        {
            transform.position = startPosition;
        }
    }
}