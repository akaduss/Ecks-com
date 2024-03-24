using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    private GridSystemVisualSingle[,] gridSystemVisualSingles;

    private void Start()
    {
        gridSystemVisualSingles = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth, LevelGrid.Instance.GetHeight];
        for (int x = 0; x < LevelGrid.Instance.GetWidth; x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight; z++)
            {
                var t = Instantiate(gridPrefab, LevelGrid.Instance.GetWorldPosition(new GridPosition(x,z)), Quaternion.identity);
                gridSystemVisualSingles[x, z] = t.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update()
    {
        HideAllGridPosition();
        if(UnitActionSystem.Instance.SelectedUnit == null) return;
        ShowGridPositionList(UnitActionSystem.Instance.SelectedUnit.MoveAction.GetValidActionGridPositionList());
    }

    public void HideAllGridPosition()
    {
        foreach (var gridSystemVisualSingle in gridSystemVisualSingles)
        {
            gridSystemVisualSingle.Hide();
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (var i in gridPositionList)
        {
            gridSystemVisualSingles[i.x, i.z].Show();
        }
    }
}
