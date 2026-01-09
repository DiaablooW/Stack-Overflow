using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;

    private float spriteWidth;
    private Vector3 startPosition;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;  
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < startPosition.x - spriteWidth + 25)
        {
            transform.position += new Vector3(spriteWidth * 2f, 0f, 0f);
        }
    }
}
