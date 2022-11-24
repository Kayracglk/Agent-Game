using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    [SerializeField]private int maxhealth = 3;
    [SerializeField] private int currentHealth;

    public int GetHealth
    {
        get { return currentHealth; }
        set { currentHealth = value;
            if(currentHealth > maxhealth)
                maxhealth = currentHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && this.gameObject != other.GetComponent<Bullet>().owner)
        {
            print(other.GetComponent<Bullet>().owner);
            print(currentHealth);
            Destroy(other.gameObject);
            currentHealth--;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
