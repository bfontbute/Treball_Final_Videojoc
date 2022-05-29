using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    int current;
    [SerializeField]
    private float speed;


    void Start()
    {
        current = 0;
    }


    void Update()
    {
        if (transform.position != waypoints[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, speed * Time.deltaTime);
        }
        else
            current = (current + 1) % waypoints.Length;
    }

}
