using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float OVERTAKINGOFFSET = 0.01f; // Offset for giving the camera priority when the last lowest ball stops moving

    public Transform BallsHolder; // Must be an empty GameObject parenting all the balls on the scene
    public float TweenSpeed = 10f;
    public float DefaultSpeed = 5f;

    private List<Transform> _ballsTransforms; // List of all the balls with BallsEmpty as their parent
    private float _finishLineY; // The y value on which the camera should stop moving downwards

    public void Awake()
    {
        UpdateBallsList();
        _finishLineY = GameObject.FindGameObjectWithTag("FinishLine").GetComponent<Transform>().position.y;
    }

    public void Update()
    {
        UpdateBallsList();
    }

    public void FixedUpdate()
    {
        if (transform.position.y > _finishLineY)
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
        _ballsTransforms = BallsHolder.GetComponentsInChildren<Transform>().Where(t => t.tag == "ball").ToList();
    }

    private float GetLowestBallY()
    {
        return _ballsTransforms.Select(t => t.transform.position.y).Min();
    }

    private bool ShouldFollowLowestBall() // Return true if a ball is ahead of the camera
    {
        return GetLowestBallY() < transform.position.y - OVERTAKINGOFFSET;
    }
 }
