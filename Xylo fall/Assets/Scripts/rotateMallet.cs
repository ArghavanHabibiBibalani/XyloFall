using System.Collections;
using UnityEngine;

public class RotateMallet : MonoBehaviour
{
    public float DefaultAngleLimit;
    public float SmoothingModifier;

    private float _currentStartingAngle;
    private bool _isLocked = false;

    private float _time = 0f;

    private void Awake()
    {
        _currentStartingAngle = DefaultAngleLimit;
        transform.rotation = Quaternion.Euler(0, 0, DefaultAngleLimit);
        ActivateMallet.OnMalletTouched += OnTouch;
    }

    void Update()
    {
        if (_isLocked && _time < 1)
        {
            _time += Time.deltaTime * SmoothingModifier;
        }
    }
    void OnTouch(Collider malletCollider)
    {
        if (malletCollider.Equals(GetComponent<SphereCollider>()) && !_isLocked)
        {
            _isLocked = true;
            StartCoroutine(ToggleRotation(_currentStartingAngle, -_currentStartingAngle));
        }
    }
    IEnumerator ToggleRotation(float startAngle, float targetAngle)
    {
        while (_time < 1)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, _currentStartingAngle), Quaternion.Euler(0, 0, -_currentStartingAngle), _time);
            yield return null;
        }
        _currentStartingAngle = -_currentStartingAngle;
        _time = 0f;
        _isLocked = false;
    }
}
