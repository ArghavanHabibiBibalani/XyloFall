using System;
using UnityEngine;

public class ActivateMallet : MonoBehaviour
{
    public delegate void MalletTouchedHandler(Collider malletCollider);

    public static event MalletTouchedHandler OnMalletTouched;
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "Mallet")
                {
                    OnMalletTouched?.Invoke(hit.collider);
                }
            }
        }
    }
}
