using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float jump;
    [SerializeField] private float speed;
    //[SerializeField] private float turnSpeed = 15f;
    [SerializeField] Transform _transform;
    [SerializeField] private Transform[] rayStartPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        _transform = transform;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        /*if (transform.eulerAngles.y > 0.1f && transform.eulerAngles.y <= 89f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0.1f, 0f), turnSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 179.9f, 0f), turnSpeed * Time.deltaTime);
        }*/
    }

    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.A)) // getkeydown -> bas çek için // getkey -> basýlý tutma için
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
            // rb.velocity = new Vector3(Mathf.Clamp( speed , 0f , 15f), rb.velocity.y, 0f); // Mathf.clamp -> speed eðer sabit gelmiyorsa onu verilen min ve max deðerde sabitliyor.
            _transform.rotation = Quaternion.LookRotation(Vector3.back); 
            // transform.rotation = Quaternion.Euler(0f, 90f, 0f);// de kullanýlabilir
            // _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(0f, 179.9f, 0f), turnSpeed * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0f);
            _transform.rotation = Quaternion.LookRotation(Vector3.forward);
            // _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(0f, 0.1f, 0f), turnSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            rb.velocity = new Vector3(0f, jump, 0f);
        }
    }
    private bool OnGroundCheck()
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoint.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.20f);
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up * 0.20f, Color.blue);
            if (hit)
            {
                return true;
            }
        }

        return false;

    }
}
