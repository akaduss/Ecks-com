using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    private GridPosition gridPosition;
    public GridPosition GridPosition => gridPosition;

    [SerializeField] private Animator animator;

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
}
