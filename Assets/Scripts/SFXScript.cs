using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public static SFXScript Instance;
    public AudioClip ballBounceSound;
    public AudioClip brickBreakSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void playBallBounceSound()
    {
        audioSource.clip = ballBounceSound;
        audioSource.Play();
    }

    public void playBrickBreakSound()
    {
        audioSource.clip = brickBreakSound;
        audioSource.Play();
    }
    public void playVictorySound()
    {
        audioSource.clip = victorySound;
        audioSource.Play();
    }
    public void playGameOverSound()
    {
        audioSource.clip = gameOverSound;
        audioSource.Play();
    }
}
