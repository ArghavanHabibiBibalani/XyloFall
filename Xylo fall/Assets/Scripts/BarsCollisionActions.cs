using UnityEngine;

public class BarsCollisionActions : MonoBehaviour
{
    private const float MINIMUMVELOCITY = 8f; // magnitude of minimum velocity each ball should have in order to make a sound

    public BarColor color;
    public GameObject NoteParticles;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && collision.relativeVelocity.magnitude > MINIMUMVELOCITY)
        {
            float volume = (collision.relativeVelocity.magnitude - MINIMUMVELOCITY) * 0.02f; // Adjust volume according to velocity

            // Limit the volume between 0 and 1
            if (volume <  0) { volume = 0; }
            else if (volume > 1) {  volume = 1; }

            _audioManager.PlaySound("bar" + color.ToString(), volume, 1);
            var Notes = Instantiate(NoteParticles, collision.contacts[0].point, Quaternion.identity);
            Notes.GetComponent<ParticleSystem>().Play();
        }
    }
}
public enum BarColor
{
    C,D,E,F,G,A,B,C2
}
