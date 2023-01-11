using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private TextMeshPro countText;
    [SerializeField] private GameObject enemy;

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
        
    }

    private void OrganizeEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            var newPosition = new Vector3(x, -0.5f, z);

            transform.transform.GetChild(i).localPosition = newPosition;
        }
    }
}
