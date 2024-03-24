using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public Action OnSelectedUnitChanged;
    public Action OnSelectedActionChanged;
    public Action<bool> OnBusyChanged;
    public Action OnActionStarted;

    public BaseAction selectedAction;

    [SerializeField] private Unit selectedUnit;
    public Unit[] Units { get; set;}

    public Unit SelectedUnit => selectedUnit;
    [SerializeField] private LayerMask unitLayer;
    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        OnSelectedUnitChanged?.Invoke();
        selectedAction = selectedUnit.MoveAction;
        OnSelectedActionChanged?.Invoke();
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

        if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
        {
            return;
        }

        if (selectedUnit.CanSpendActionPoints(selectedAction) == false) return;

        selectedUnit.SpendActionPoints(selectedAction.GetActionPointsCost());

        SetBusy();
        selectedAction.TakeAction(mouseGridPosition, SetNotBusy);

        OnActionStarted?.Invoke();
    }


    //Handle unit selection from mouseposition raycast
    public bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, unitLayer) && hit.collider.TryGetComponent(out Unit newSelectedUnit))
        {
            if (selectedUnit == newSelectedUnit) return false;

            selectedUnit = newSelectedUnit;
            selectedAction = newSelectedUnit.MoveAction;

            OnSelectedActionChanged?.Invoke();
            OnSelectedUnitChanged?.Invoke();

            return true;
        }

        return false;
    }

    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(true);
    }

    private void SetNotBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(false);
    }

}