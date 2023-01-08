using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform player;
    private int numberOfFellows;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject fellow;


    // Start is called before the first frame update
    void Start()
    {
        UpdateFellowNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnFellows(int fellowNum)
    {
        for(int i = 0; i < fellowNum; i++)
        {
            Instantiate(fellow, transform.position, Quaternion.identity, transform);
        }
        UpdateFellowNumber();
    }

    private void UpdateFellowNumber()
    {
        //One of the children is Count Label. Don't include it
        numberOfFellows = transform.childCount - 1;

        countText.text = numberOfFellows.ToString();
    }
}
