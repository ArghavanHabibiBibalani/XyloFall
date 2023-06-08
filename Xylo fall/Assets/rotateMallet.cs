using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class rotateMallet : MonoBehaviour
{
    public bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position.x;

        if (isActive)
        {
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0);

                if(screenTouch.phase == TouchPhase.Moved)
                {
                    if (screenTouch.deltaPosition.x < pos + 1 || screenTouch.deltaPosition.x > pos + 1)
                    {
                        transform.Rotate(screenTouch.deltaPosition.y, screenTouch.deltaPosition.x, 0f);////////////
                    }

                }

                if(screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                }

            }
        }

        
    }
}
