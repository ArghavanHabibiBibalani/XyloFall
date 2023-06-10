using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float OVERTAKINGOFFSET = 0.01f; // Offset for giving the camera priority when the last lowest ball stops moving
    private static bool _hasWon = false;

    public Transform BallsEmpty; // Must be an empty GameObject parenting all the balls on the scene
    public float TweenSpeed = 10f;
    public float DefaultSpeed = 5f;

    private List<Transform> _ballsTransforms; // List of all the balls with BallsEmpty as their parent

    public void Awake()
    {
        UpdateBallsList();
        TriggerWin.OnWinDetected += OnWin;
    }

    public void Update()
    {
        UpdateBallsList();
    }

    public void FixedUpdate()
    {
        if (_hasWon == false)
        {
            if (ShouldFollowLowestBall())
            {
                var desiredPosition = new Vector3(transform.position.x, GetLowestBallY(), transform.position.z);
                Vector3 tweenedPosition = Vector3.Lerp(transform.position, desiredPosition, TweenSpeed * Time.deltaTime);
                transform.position = tweenedPosition;
            }
            else
            {
                Vector3 tempVector = transform.position;
                tempVector.y -= DefaultSpeed * Time.deltaTime;
                transform.position = tempVector;
            }
        }
    }

    public void UpdateBallsList()
    {
        _ballsTransforms = BallsEmpty.GetComponentsInChildren<Transform>().Where(t => t.tag == "ball").ToList();
    }

    private float GetLowestBallY()
    {
        return _ballsTransforms.Select(t => t.transform.position.y).Min();
    }

    private bool ShouldFollowLowestBall()
    {
        return GetLowestBallY() < transform.position.y - OVERTAKINGOFFSET;
    }

    private void OnWin(object sender, EventArgs args) { _hasWon = true; }
 }
