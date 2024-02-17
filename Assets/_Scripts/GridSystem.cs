using TMPro;
using UnityEngine;

public class GridSystem
{
    private static readonly float cellSize = 2f;
    private Grid[,] gridArray;

    public GridSystem(int width, int height)
    {
        gridArray = new Grid[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                //Debug.Log(x + ", " + z);
                gridArray[x, z] = new Grid(this, new GridPosition(x, z));
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return new GridPosition(Mathf.RoundToInt(worldPos.x / cellSize), Mathf.RoundToInt(worldPos.z / cellSize));
    }

    public Grid GetGridFromWorldPos(Vector3 worldPos)
    {
        GridPosition gridPosition = GetGridPosition(worldPos);

        return gridArray[gridPosition.x, gridPosition.z];
    }

    public Grid GetGridFromGridPosition(GridPosition gridPosition)
    {
        return gridArray[gridPosition.x, gridPosition.z];
    }

    public void DebugGridPrefabs(Transform debugPrefab)
    {
        foreach (Grid grid in gridArray)
        {
            Transform prefab = Transform.Instantiate(debugPrefab, GetWorldPosition(grid.GridPosition), Quaternion.identity);
            prefab.GetComponent<GridDebugObject>().SetGridObject(grid);
        }
    }  
    
    public bool IsWithinGrid(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < gridArray.GetLength(0) && 
            gridPosition.z >= 0 && gridPosition.z < gridArray.GetLength(1);
    }

}
