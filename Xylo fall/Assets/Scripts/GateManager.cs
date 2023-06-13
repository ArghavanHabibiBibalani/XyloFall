using TMPro;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public TextMeshPro gateNum;
    public int Factor;

    void Awake()
    { 
        if (Factor <= 1) { Factor = Random.Range(2, 5); }
        gateNum.text = "X" + Factor.ToString();
        Factor -= 1;
    }
}
