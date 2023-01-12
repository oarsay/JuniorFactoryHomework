using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private TextMeshPro countText;
    [SerializeField] private GameObject enemy;
    private Transform target;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private float enemySpeedOnAttack;
    private float rotationSpeed = 3f;

    // ENEMY ORGANIZATION
    [Range(0f, 1f)] [SerializeField] private float distanceFactor, radius, duration = 1f;

    

    void Start()
    {
        for(int i = 0; i < Random.Range(15, 50); i++)
        {
            Instantiate(enemy, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        }

        countText.text = (transform.childCount - 1).ToString();

        OrganizeEnemies();
    }

    void Update()
    {
        if(playerManager.isFight && transform.childCount > 0)
        {
            Rotate();
            Move();
        }
    }

    private void OrganizeEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            var newPosition = new Vector3(x, 0f, z);

            transform.transform.GetChild(i).localPosition = newPosition;
        }
    }

    private void Rotate()
    {
        var targetDirection = transform.position - new Vector3(target.position.x, transform.position.y, target.position.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection, Vector3.up), Time.deltaTime * rotationSpeed);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeedOnAttack * Time.deltaTime);
    }

    public void Attack(Transform player)
    {
        target = player;

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
        }
    }
}
