using System;
using System.Linq;
using UnityEngine;

public class TriggerLoss : MonoBehaviour
{
    [HideInInspector]
    public static event EventHandler OnLossDetected;

    private Transform _ballsHolder;
    private Renderer[] _ballsRenderers;

    private void Start()
    {
        _ballsHolder = GameObject.FindGameObjectWithTag("BallsHolder").GetComponent<Transform>();
        UpdateBalls();
    }
    private void Update()
    {
        UpdateBalls();
    }
    private void OnBecameInvisible()
    {
        foreach (Renderer r in _ballsRenderers)
        {
            if (r.isVisible) { return; }
        }
        OnLossDetected?.Invoke(this, EventArgs.Empty);
    }
    private void UpdateBalls()
    {
        _ballsRenderers = _ballsHolder.GetComponentsInChildren<Renderer>().Where(r => r.gameObject.tag == "ball").ToArray();
    }
}
