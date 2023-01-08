using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Transform player;
    private int currentFellowNum;
    [SerializeField] private TextMeshPro countText;
    [SerializeField] private GameObject fellow;


    // Start is called before the first frame update
    void Start()
    {
        player = transform;
        UpdateFellowNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnFellows(int neededFellowNum)
    {
        for(int i = 0; i < neededFellowNum; i++)
        {
            Instantiate(fellow, transform.position, Quaternion.identity, transform);
        }
        UpdateFellowNumber();
    }

    private void UpdateFellowNumber()
    {
        //One of the children is Count Label. Don't include it
        currentFellowNum = transform.childCount - 1;

        countText.text = currentFellowNum.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {

            // DISABLE PLAYER'S BOXCOLLIDER UNTIL THE END OF A COROUTINE

            int newFellowNum, neededFellowNum;
            var gateManager = other.GetComponent<GateManager>();
            newFellowNum = gateManager.ApplyGateEffect(currentFellowNum);
            neededFellowNum = newFellowNum - currentFellowNum;
            currentFellowNum = newFellowNum;

            SpawnFellows(neededFellowNum);
        }
        
    }

}
