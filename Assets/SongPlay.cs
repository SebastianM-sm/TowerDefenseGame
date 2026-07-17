using UnityEngine;

public class SongPlay : MonoBehaviour
{
    public AudioSource song;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        song.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
