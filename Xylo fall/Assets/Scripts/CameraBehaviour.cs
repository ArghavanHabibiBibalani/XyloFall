using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private const float OVERTAKINGOFFSET = 0.01f; // Offset for giving the camera priority when the last lowest ball stops moving
    private const float LOSINGDISTANCE = 50f;

    public float TweenSpeed = 10f;
    public float DefaultSpeed = 5f;

    private Transform _ballsHolder; // Must be an empty GameObject parenting all the balls on the scene
    private List<Transform> _ballsTransforms; // List of all the balls with BallsEmpty as their parent
    private bool _isLost = false; // Flag showing if the game is lost or not
    private float _finishLineY; // The y value on which the camera should stop moving downwards

    public delegate void LossHandler();
    [HideInInspector]
    public static event LossHandler OnLossDetected;

    public void Awake()
    {
        _ballsHolder = GameObject.FindGameObjectWithTag("BallsHolder").transform;
        UpdateBallsList();
        _finishLineY = GameObject.FindGameObjectWithTag("FinishLine").GetComponent<Transform>().position.y;
    }

    public void Update()
    {
        UpdateBallsList();
    }

    public void FixedUpdate()
    {
        if (transform.position.y > _finishLineY && _isLost == false)
        {
            if (transform.position.y <= GetLowestBallY() - LOSINGDISTANCE)
            {
                _isLost = true;
                OnLossDetected.Invoke();
            }
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
        _ballsTransforms = _ballsHolder.GetComponentsInChildren<Transform>().Where(t => t.tag == "ball").ToList();
    }

    public float GetLowestBallY()
    {
        return _ballsTransforms.Select(t => t.transform.position.y).Min();
    }

    private bool ShouldFollowLowestBall() // Return true if a ball is ahead of the camera
    {
        return GetLowestBallY() < transform.position.y - OVERTAKINGOFFSET;
    }
 }
