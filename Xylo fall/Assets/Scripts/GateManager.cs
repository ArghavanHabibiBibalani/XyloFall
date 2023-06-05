using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public TextMeshPro gateNum;
    public int randomNum;

    void Start()
    {

        randomNum =Random.Range(2,5);
        gateNum.text = "X" + randomNum.ToString();
        randomNum -= 1;
        

    }
}
