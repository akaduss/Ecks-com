using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    public Unit[] Units { get; set;}

    public Unit SelectedUnit => selectedUnit;
    [SerializeField] private LayerMask unitLayer;
    private bool isBusy;

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
                SetBusy();
                selectedUnit.MoveAction.Move(mouseGridPosition, SetNotBusy);
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

    private void SetBusy()
    {
        isBusy = true;
    }

    private void SetNotBusy()
    {
        isBusy = false;
    }


}