using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static float globalSpeed = 5f; 
    public float lifetime = 10f;

    [Header("Audio")]
    public AudioClip[] spawnSounds; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (spawnSounds != null && spawnSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnSounds.Length);
            audioSource.PlayOneShot(spawnSounds[randomIndex]);
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.left * globalSpeed * Time.deltaTime;
    }
}