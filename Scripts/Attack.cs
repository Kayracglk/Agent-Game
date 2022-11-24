using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float currentFireRate = 0f;
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private int MaxAmmoCount = 10;
    [SerializeField] private bool isPlayer = false;
    public float getCurrentFireRate
    {
        get { return currentFireRate; }
        set { currentFireRate = value; }
    }
    public int GetBullet
    {
        get { return ammoCount; }
        set { ammoCount = value; 
              if(ammoCount >MaxAmmoCount)
                ammoCount = MaxAmmoCount;
        }
    }
    public int GetClipSize
    {
        get { return MaxAmmoCount; }
    }
    // Start is called before the first frame update
    void Start()
    {
        ammoCount = MaxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFireRate > 0f)
        {
            currentFireRate -= Time.deltaTime;
        }
        if(isPlayer)
        {
            if (Input.GetMouseButtonDown(0) && currentFireRate <= 0 && ammoCount > 0)
            {
                Fire();
            }
        }
        

    }
    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = 90f;
        if (difference >= 90f)
        {
            targetRotation = 90f;
        }
        else if (difference < 90f)
        {
            targetRotation = -90f;
        }
        currentFireRate = fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation));
        bulletClone.GetComponent<Bullet>().owner = gameObject;
        ammoCount--;
    }
}
