using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateManager : MonoBehaviour
{
    // RANGES - STATIC CLASS VARIABLES FOR ALL GATES
    private static int minInclusiveForSign = 0, maxExclusiveForSign = 2;
    private static int minInclusiveForSumValue = 10, maxExclusiveForSumValue = 81; // +[10,80]
    private static int minInclusiveForMultiplier = 2, maxExclusiveForMultiplier = 5; // ×[2,4]

    // GATE VARIABLES
    [SerializeField] private TextMeshPro gateText;
    private int value;
    private int sign; //0 for (+), 1 for (×)

    void Start()
    {
        sign = Random.Range(minInclusiveForSign, maxExclusiveForSign);

        switch (sign)
        {
            case 0:
                value = Random.Range(minInclusiveForSumValue, maxExclusiveForSumValue);
                gateText.text = "+";
                break;
            case 1:
                value = Random.Range(minInclusiveForMultiplier, maxExclusiveForMultiplier);
                gateText.text = "×";
                break;
            default:
                Debug.LogError("Unknown sign value for the gate. Check for the switch-case in GateManager script.");
                break;
        }
        gateText.text += value; 
    }

    public int ApplyGateEffect(int numberOfFellows)
    {
        switch (sign)
        {
            case 0:
                return numberOfFellows + this.value;
            case 1:
                return numberOfFellows * this.value;
            default:
                Debug.LogError("Unknown sign value for the gate. Check for the switch-case in GateManager script.");
                return -1;
        }
    }
}
