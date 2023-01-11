using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    private Transform player;
    public Transform enemy;
    private PlayerMovement playerMovement;
    private int currentFellowNum;
    [SerializeField] private TextMeshPro countText;
    [SerializeField] private GameObject fellow;

    // FELLOW ORGANIZATION
    [Range(0f, 1f)] [SerializeField] private float distanceFactor, radius, duration = 1f;

    public bool isFight;


    void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();
        player = transform;
        UpdateFellowNumber();
    }

    void Update()
    {

    }

    private void SpawnFellows(int neededFellowNum)
    {
        for(int i = 0; i < neededFellowNum; i++)
        {
            Instantiate(fellow, transform.position, Quaternion.identity, transform).GetComponent<Animator>().SetBool("run", true);
        }
        UpdateFellowNumber();
        OrganizeFellows();
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
            // **************************************************************
            // DISABLE PLAYER'S BOXCOLLIDER UNTIL THE END OF A COROUTINE
            // **************************************************************

            int newFellowNum, neededFellowNum;
            var gateManager = other.GetComponent<GateManager>();
            newFellowNum = gateManager.ApplyGateEffect(currentFellowNum);
            neededFellowNum = newFellowNum - currentFellowNum;
            currentFellowNum = newFellowNum;

            SpawnFellows(neededFellowNum);
        }
        else if(other.CompareTag("Enemy"))
        {
            enemy = other.transform;
            Debug.Log("position " + enemy.position);
            isFight = true;
            other.transform.GetChild(2).GetComponent<EnemyManager>().Attack(transform);
        }
    }

    private void OrganizeFellows()
    {
        // Pass 0th element because it is Count Label.
        for(int i = 1; i < player.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            var newPosition = new Vector3(x, -0.5f, z);

            player.transform.GetChild(i).DOLocalMove(newPosition, duration).SetEase(Ease.OutBack);
        }
    }

}
