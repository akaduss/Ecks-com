using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Unit unit;

    private const string IS_WALKING = "IsWalking";

    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Animator animator;

    private void Awake()
    {
        targetPos = transform.position;
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();

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

}
