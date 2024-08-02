using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Soldier
    public void JumpSound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    public void ArrowSound()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void HitSound()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }

    public void DeathSound()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }

    public void BauSound()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }

    public void Sword1Sound()
    {
        audioSource.PlayOneShot(audioClips[5]);
    }

    public void Sword2Sound()
    {
        audioSource.PlayOneShot(audioClips[6]);
    }

    //Interface
    public void MouseOverSound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    //Key
    public void KeySound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
}
