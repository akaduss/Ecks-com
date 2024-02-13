using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    //singleton
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform debugPrefab;
    private GridSystem gridSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        gridSystem = new GridSystem(10, 10);
    }

    private void Start()
    {
        gridSystem.DebugGridPrefabs(debugPrefab);
    }

    public void SetUnitAtGrid(Unit unit)
    {
        Grid grid = gridSystem.GetGridFromWorldPos(unit.transform.position);
        grid.Unit = unit;
    }

    //Clear unit at grid
    public void ClearUnitAtGrid(Unit unit)
    {
        Grid grid = gridSystem.GetGridFromWorldPos(unit.transform.position);
        grid.Unit = null;
    }

    //Change unit grid position
    public void ChangeUnitGridPosition(Unit unit, GridPosition newGridPosition)
    {
        Grid oldGrid = gridSystem.GetGridFromGridPosition(unit.GridPosition);
        Grid newGrid = gridSystem.GetGridFromGridPosition(newGridPosition);

        oldGrid.Unit = null;
        newGrid.Unit = unit;
    }

    public Unit GetUnitByGridPosition(GridPosition gridPosition)
    {
        Grid grid = gridSystem.GetGridFromGridPosition(gridPosition);
        return grid.Unit;
    }

    //Get grid position from world position
    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return gridSystem.GetGridFromWorldPos(worldPos).GridPosition;
    }

}
