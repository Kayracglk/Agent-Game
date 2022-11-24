using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public GameObject owner;
    void Update()
    {
        transform.position += transform.up * speed *Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
