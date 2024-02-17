using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Unit unit;

    private const string IS_WALKING = "IsWalking";

    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Animator animator;

    [SerializeField] private int maxMoveDistance = 4;

    private void Awake()
    {
        targetPos = transform.position;
        unit = GetComponent<Unit>();
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        GetMoveAction();
    }

    public void GetMoveAction()
    {
        const float stopDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPos) > stopDistance)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            moveDir.y = 0;
            transform.position += moveSpeed * Time.deltaTime * moveDir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotationSpeed);
            animator.SetBool(IS_WALKING, true);
            Move(targetPos);
        }
        else
        {
            transform.position = targetPos;
            animator.SetBool(IS_WALKING, false);
        }
    }

    public void Move(Vector3 targetPos)
    {
        targetPos.y = 0;
        this.targetPos = targetPos;
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

}
