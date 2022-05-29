using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regal : MonoBehaviour
{
    public GameObject Hit;
    private Vector3 PositionRegalo;



    // Start is called before the first frame update
    void Start()
    {
        PositionRegalo = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Hit.SetActive(true);

        }
    }
}
