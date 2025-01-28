using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    [SerializeField] private AudioClip checkpointSound;
    private AudioSource audioSource;
    private ParticleSystem particulas;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particulas = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController gameController = FindObjectOfType<GameController>();
            Soldier player = FindObjectOfType<Soldier>();
            gameController.Checkpoint(player.transform.position.x, player.transform.position.y, player.GetVida());
            particulas.GetComponent<ParticleSystem>().Stop();
            audioSource.PlayOneShot(checkpointSound);
        }
    }
}
