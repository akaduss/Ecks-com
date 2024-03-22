using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += CreateActionButtons;
        UnitActionSystem.Instance.OnSelectedActionChanged += UpdateSelectedVisual;
    }

    private void CreateActionButtons()
    {
        var selectedUnit = UnitActionSystem.Instance.SelectedUnit;

        foreach(Transform transform in actionButtonContainerTransform)
        {
            Destroy(transform.gameObject);
        }

        foreach(var action in selectedUnit.GetBaseActions)
        {
            Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            actionButton.GetComponent<ActionButtonUI>().SetBaseAction(action);
        }

    }

    private void UpdateSelectedVisual()
    {
        foreach(ActionButtonUI button in actionButtonContainerTransform)
        {
            //UnitActionSystem.Instance.selectedAction
        }
    }


}
