using System;
using UnityEngine;

public class BarsCollisionActions : MonoBehaviour
{
    public BarColor color;

    private const float MINIMUMVELOCITY = 8f; // magnitude of minimum velocity each ball should have in order to make a sound
    private AudioManager m_AudioManager;
    private ParticleSystem m_ParticleSystem;

    private void Awake()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && collision.relativeVelocity.magnitude > MINIMUMVELOCITY)
        {
            float volume = (collision.relativeVelocity.magnitude - MINIMUMVELOCITY) * 0.02f; // Adjust volume according to velocity

            // Limit the volume between 0 and 1
            if (volume <  0) { volume = 0; }
            else if (volume > 1) {  volume = 1; }

            m_AudioManager.PlaySound(Array.Find(m_AudioManager.sounds, s => s.Name == "bar" + color.ToString()).Name, volume, 1);
            m_ParticleSystem.transform.position = collision.contacts[0].point;
            m_ParticleSystem.Play();
        }
    }
}

public enum BarColor
{
    C,D,E,F,G,A,B
}
