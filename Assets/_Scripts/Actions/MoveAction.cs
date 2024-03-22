using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private const string IS_WALKING = "IsWalking";

    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Animator animator;

    [SerializeField] private int maxMoveDistance = 4;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        targetPos = transform.position;
    }

    private void Update()
    {
        if (isActive == false)
            return;

        GetMoveAction();
    }

    public void GetMoveAction()
    {
        Vector3 moveDirection = (targetPos - transform.position).normalized;

        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPos) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            animator.SetBool(IS_WALKING, true);
        }
        else
        {
            animator.SetBool(IS_WALKING, false);
            isActive = false;
            OnActionDone();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }

    public void Move(GridPosition gridPosition, Action OnActionDone)
    {
        this.OnActionDone = OnActionDone;
        this.targetPos = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }


    public bool IsValidActionGridPosition(GridPosition gridPosition) => GetGridsInRange().Contains(gridPosition);

    public List<GridPosition> GetGridsInRange()
    {
        List<GridPosition> gridsInRange = new();
        GridPosition unitGridPosition = unit.GridPosition;
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offset = new(x, z);
                GridPosition gridPos = unitGridPosition + offset;

                if (LevelGrid.Instance.IsWithinGrid(gridPos) == false)
                {
                    continue;
                }

                if (gridPos.Equals(unitGridPosition))
                {
                    continue;
                }

                if(LevelGrid.Instance.IsGridOccupied(gridPos))
                {
                    continue;
                }

                gridsInRange.Add(gridPos);
            }
        }
        return gridsInRange;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
