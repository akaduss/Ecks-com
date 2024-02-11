using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject outline;
    private Vector3 targetPos;
    private GridPosition gridPosition;
    public GridPosition GridPosition => gridPosition;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGrid(this);
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

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(targetPos);
        if (newGridPosition.Equals(GridPosition) == false)
        {
            LevelGrid.Instance.ChangeUnitGridPosition(this, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void SetOutline(bool value)
    {
        outline.SetActive(value);
    }

    public void Move(Vector3 targetPos)
    {
        targetPos.y = 0;
        this.targetPos = targetPos;
    }
}
