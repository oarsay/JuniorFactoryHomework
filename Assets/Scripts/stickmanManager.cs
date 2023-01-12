using UnityEngine;

public class stickmanManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Red"))
        {
            //Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
