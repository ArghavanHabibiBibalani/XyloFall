using UnityEngine;

public class BarsCollisionActions : MonoBehaviour
{
    private const float MINIMUMVELOCITY = 8f; // magnitude of minimum velocity each ball should have in order to make a sound

    public BarColor Color;
    public GameObject NoteParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && collision.relativeVelocity.magnitude > MINIMUMVELOCITY)
        {
            float volume = (collision.relativeVelocity.magnitude - MINIMUMVELOCITY) * 0.05f; // Adjust volume according to velocity

            // Limit the volume between 0.2 and 1
            if (volume > 1) {  volume = 1; }
            if (volume < 0.2f) { volume = 0; }

            if (volume > 0.2f)
            {
                AudioManager.instance.PlaySoundOneShot("bar" + Color.ToString(), volume);
                var Notes = Instantiate(NoteParticles, collision.contacts[0].point, Quaternion.identity);
                Notes.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
public enum BarColor
{
    C,D,E,F,G,A,B,C2
}
