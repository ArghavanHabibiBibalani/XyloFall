using UnityEngine;

public class activateMallet : MonoBehaviour
{
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
                    var MalletScript = hit.collider.GetComponent<rotateMallet>();
                    MalletScript.isActive = true;
                }
            }
        }
    }
}
