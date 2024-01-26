using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 targetPos;

    [SerializeField] private Animator animator;

    private void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        const float stopDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPos) > stopDistance)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            moveDir.y = 0;

            transform.position += moveSpeed * Time.deltaTime * moveDir;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotationSpeed);

            animator.SetBool(IS_WALKING, true);
        }
        else
        {
            transform.position = targetPos;
            animator.SetBool(IS_WALKING, false);

        }

        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetMousePos());
        }
    }

    private void Move(Vector3 targetPos)
    {
        targetPos.y = 0;
        this.targetPos = targetPos;
    }
}
