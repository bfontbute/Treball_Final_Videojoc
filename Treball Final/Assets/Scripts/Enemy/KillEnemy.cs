using UnityEngine;

public class KillEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);


            Destroy(transform.parent.gameObject);
        }
    }
}
