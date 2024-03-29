using UnityEngine;

public class Unit : MonoBehaviour
{

    private MoveAction moveAction;

    [SerializeField] private GameObject outline;
    private GridPosition gridPosition;
    public GridPosition GridPosition => gridPosition;
    public MoveAction MoveAction => moveAction;
    private BaseAction[] baseActions;
    public BaseAction[] GetBaseActions => baseActions;
    private int actionPoints = 2;

    public int ActionPoints => actionPoints;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        baseActions = GetComponents<BaseAction>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGrid(this);
    }

    void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
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

    public bool CanSpendActionPoints(BaseAction baseAction) => actionPoints - baseAction.GetActionPointsCost() >= 0;

    public void SpendActionPoints(int pointAmount) => actionPoints -= pointAmount;
}
