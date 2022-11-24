using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] movePoints;
    [SerializeField] float speed = 5f;
    private bool canMoveRight = false;

    [SerializeField] float shootRange = 10f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private Transform aimTransform;
    private Attack attack;
    private bool isReloaded = false;
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();
        CheckCanMoveRight();
        MoveToward();
        Aim();
    }

    private void Reload()
    {
        attack.GetBullet = attack.GetClipSize;
        isReloaded = false;
    }

    private void MoveToward()
    {
        if(Aim() && attack.GetBullet > 0)
        {
            return;
        }
        if(!canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[0].position, speed * Time.deltaTime);
            // transform.rotation = Quaternion.LookRotation(Vector3.back);
            LookTheTarget(movePoints[0].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[1].position, speed * Time.deltaTime);
            // transform.rotation = Quaternion.LookRotation(Vector3.forward);
            LookTheTarget(movePoints[1].position);
        }
    }

    private void EnemyAttack()
    {
        if(attack.GetBullet <= 0 && !isReloaded)
        {
            Invoke("Reload", 5f); // 5 saniye sonra reload methodunu çaðýrýr.
            isReloaded = true;
        }
        if (attack.getCurrentFireRate <= 0f && attack.GetBullet > 0 && Aim())
        {
            attack.Fire();
        }
    }
    private void CheckCanMoveRight()
    {
        if(Vector3.Distance(transform.position, movePoints[0].position) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if(Vector3.Distance(transform.position, movePoints[1].position) <= 0.1f)
        {
            canMoveRight= false;
        }
    }

    private bool Aim()
    {
        bool hit = Physics.Raycast(aimTransform.position,transform.forward,shootRange, shootLayer);
        return hit;
    }

    private void LookTheTarget(Vector3 newTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation(newTarget - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
