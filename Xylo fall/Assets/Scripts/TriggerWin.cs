using System;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{

    public GameObject NoteParticles; // Reference to the NoteParticles prefab for instantiating

    public delegate void WinHandler();

    public static event WinHandler OnWinDetected; // Winning event

    public static event WinHandler OnLevelComplete; // Level completion event - gets invoked after the timer goes off

    [HideInInspector]
    public static int BonusCount = 0; // Used in scoring, also used as an iteration index for playing sounds when each bonus ball passes the finish line

    private bool _isCalculatingBonus = false;
    private bool _levelComplete = false;
    private float _timer = -1;
    private AudioManager _audioManager;
    private Transform _finishingParticlesTransform;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _finishingParticlesTransform = GameObject.FindGameObjectWithTag("FinishingParticles").transform;
    }

    private void Start()
    {
        BonusCount = 0;
    }

    private void Update()
    {
        if (_timer != -1)
        {
            _timer += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (_timer >= 3)
        {
            OnLevelComplete?.Invoke(); // Only invoked once per level
            _levelComplete = true;
            GameManager.instance.ProgressLevel = true;
            GameManager.instance.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Vector3 particlePosition = new Vector3(other.gameObject.transform.position.x, transform.position.y, 0);
            if (_isCalculatingBonus == false)
            {
                _isCalculatingBonus = true;
                OnWinDetected?.Invoke();
                _audioManager.PlaySoundOneShot("sweep", 1);
                _finishingParticlesTransform.position = particlePosition;
                GetComponentInChildren<ParticleSystem>().Play();
            }
            else
            {
                var Notes = Instantiate(NoteParticles, particlePosition, Quaternion.identity);
                _audioManager.PlaySoundOneShot((BonusCount % 8), 1);
                Notes.GetComponent<ParticleSystem>().Play();
                if (_levelComplete == false) { BonusCount++; }
            }
            _timer = 0;
        }
    }
}
