using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    public Unit[] Units { get; set;}

    public Unit SelectedUnit => selectedUnit;
    [SerializeField] private LayerMask unitLayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePos());
            if (selectedUnit.MoveAction.IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.MoveAction.Move(LevelGrid.Instance.GetWorldPosition(mouseGridPosition));
            }
        }
    }

    //Handle unit selection from mouseposition raycast
    public bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, unitLayer) && hit.collider.TryGetComponent(out Unit newSelectedUnit))
        {
            if (selectedUnit != null)
                selectedUnit.SetOutline(false);

            selectedUnit = newSelectedUnit;
            selectedUnit.SetOutline(true);

            return true;
        }

        return false;
    }


}