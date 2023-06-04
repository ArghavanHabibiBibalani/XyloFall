using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform BallsEmpty; // Must be an empty GameObject parenting all the balls on the scene
    public float TweenSpeed = 10f;
    public float DefaultSpeed = 5f;

    private List<Transform> ballsTransforms; // List of all the balls with BallsEmpty as their parent
    private float overtakingOffset; // Offset for giving the camera priority when the last lowest ball stops moving

    public void Start()
    {
        ballsTransforms = BallsEmpty.GetComponentsInChildren<Transform>().Where(t => t.tag == "ball").ToList(); // Excluding the parent itself from the list
        overtakingOffset = 0.01f;
    }

    public void FixedUpdate()
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

    private float GetLowestBallY()
    {
        return ballsTransforms.Select(t => t.transform.position.y).Min();
    }

    private bool ShouldFollowLowestBall()
    {
        return GetLowestBallY() < transform.position.y - overtakingOffset;
    }
 }
