using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BallsManager : MonoBehaviour
{
    public GameObject ballsHolder;
    [SerializeField] GameObject ball;
    public float speedBall = 10;
    public float moveSpeed = 5;
    public float gravityModifier = 1;
    //---------------------------------------------------------------

    private Transform player;
    private Rigidbody targetRigidbody;
    private Rigidbody rb;
    private Vector3 previousVelocity;
    private int numberOfBalls;

    void Start()
    {
        player= transform;
        numberOfBalls = transform.childCount;
        Physics.gravity *= gravityModifier;
        //-----------------------------------------------

        rb = GetComponent<Rigidbody>();
        previousVelocity = rb.velocity;
    }

    private IEnumerator MakeBalls(int number)
    {
        Vector3 currentAcceleration = (rb.velocity - previousVelocity) / Time.fixedDeltaTime;
        previousVelocity = rb.velocity;
        Vector3 newPos;
        numberOfBalls = ballsHolder.transform.childCount;
        //instantiate balls and move them
        for (int i = 1; i < number + 1 ; i++)
        {
            if(i%2==0)
            {
                newPos = new Vector3(transform.position.x + i+15, transform.position.y -10, transform.position.z);
            }
            else
            {
                newPos = new Vector3(transform.position.x - i-15, transform.position.y -10 , transform.position.z);
            }
            
            GameObject targetObject =  Instantiate(ball,transform.position,Quaternion.identity,ballsHolder.transform);
            targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, newPos, moveSpeed * Time.deltaTime);
        }
        
        yield return null;
        // add rigidbody,collider and ball manager script to the new balls
        for (int i = numberOfBalls; i < ballsHolder.transform.childCount; i++)
        {
            ballsHolder.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            
            ballsHolder.transform.GetChild(i).gameObject.AddComponent<SphereCollider>();

            targetRigidbody = ballsHolder.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
            targetRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            targetRigidbody.AddForce(currentAcceleration, ForceMode.Acceleration);
            BallsManager secondScript =  ballsHolder.transform.GetChild(i).gameObject.AddComponent<BallsManager>();
            secondScript.ballsHolder = ballsHolder;
            secondScript.ball = ball;
        }
        numberOfBalls = ballsHolder.transform.childCount;
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            var gateManager = other.GetComponent<GateManager>();

            StartCoroutine(MakeBalls(1 + gateManager.randomNum));
        }
    }
 
}
