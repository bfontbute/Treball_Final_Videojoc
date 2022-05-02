using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);


            Destroy(transform.parent.gameObject, 0.0f);
        }
    }
}
