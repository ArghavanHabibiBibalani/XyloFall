using System;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    private static bool _isCalculatingBonus = false;

    public GameObject NoteParticles; // Reference to the NoteParticles prefab for instantiating

    [HideInInspector]
    public static event EventHandler OnWinDetected; // Winning event

    [HideInInspector]
    public static int BonusCount = 0; // Used in scoring, also used as an iteration index for playing sounds when each bonus ball passes the finish line

    private AudioManager _audioManager;
    private Transform _finishingParticlesTransform;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _finishingParticlesTransform = GameObject.FindGameObjectWithTag("FinishingParticles").transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Vector3 particlePosition = new Vector3(other.gameObject.transform.position.x, transform.position.y, 0);
            if (_isCalculatingBonus == false)
            {
                _isCalculatingBonus = true;
                OnWinDetected?.Invoke(this, EventArgs.Empty);
                _audioManager.PlaySound("sweep", 1, 1);
                _finishingParticlesTransform.position = particlePosition;
                GetComponentInChildren<ParticleSystem>().Play();
            }
            else
            {
                var Notes = Instantiate(NoteParticles, particlePosition, Quaternion.identity);
                if (BonusCount < 8) { _audioManager.PlaySound(BonusCount - 1, 1, 1); }
                else { _audioManager.PlaySound(7, 1, 1); }
                Notes.GetComponent<ParticleSystem>().Play();
            }
            BonusCount++;
        }
    }
}
