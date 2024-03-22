using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public Action OnSelectedUnitChanged;
    public Action OnSelectedActionChanged;

    public BaseAction selectedAction;

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
        if (isBusy)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            HandleSelectedAction();
        }
    }

    private void HandleSelectedAction()
    {
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePos());
        OnSelectedActionChanged?.Invoke();

        switch (selectedAction)
        {
            case MoveAction moveAction:
                if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    moveAction.Move(mouseGridPosition, SetNotBusy);
                }
                break;

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
            selectedAction = newSelectedUnit.MoveAction;

            OnSelectedUnitChanged?.Invoke();

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