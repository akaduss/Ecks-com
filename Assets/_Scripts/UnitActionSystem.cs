using Unity.VisualScripting;
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
            HandleUnitSelection();

            //Move selected unit to mouse position if grid is not occupied by another unit
            if (selectedUnit != null && MouseWorld.GetMousePos() != null)
            {
                //Move selected unit to grid position
                Vector3 targetPos = MouseWorld.GetMousePos();
                GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(targetPos);
                if (LevelGrid.Instance.GetUnitByGridPosition(gridPosition) == null)
                {
                    //selectedUnit.Move(GridSystem.GetWorldPosition(gridPosition));
                }
            }
        }
    }

    //Handle unit selection from mouseposition raycast
    public void HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, unitLayer))
        {
            //Select new unit, unselect old unit
            if (selectedUnit != null)
            {
                selectedUnit.SetOutline(false);
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    selectedUnit = hit.collider.GetComponent<Unit>();
                    selectedUnit.SetOutline(true);
                }
            }
            else
            {
                //Select new unit if no unit is selected
                if (hit.collider.TryGetComponent(out selectedUnit))
                {
                    selectedUnit.SetOutline(true);
                }
            }
        }
    }


}