using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonUIs = new();

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStart;

        CreateActionButtons();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedUnitChanged()
    {
        CreateActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnSelectedActionChanged()
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStart()
    {
        UpdateActionPoints();
    }

    private void CreateActionButtons()
    {
        var selectedUnit = UnitActionSystem.Instance.SelectedUnit;

        foreach(Transform transform in actionButtonContainerTransform)
        {
            Destroy(transform.gameObject);
        }

        actionButtonUIs.Clear();

        foreach(var action in selectedUnit.GetBaseActions)
        {
            Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            actionButton.GetComponent<ActionButtonUI>().SetBaseAction(action);
            actionButtonUIs.Add(actionButton.GetComponent<ActionButtonUI>());
        }

    }
    //onselectedactionchanged
    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButton in actionButtonUIs)
        {
            actionButton.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        actionPointsText.text = "Action Points: " + UnitActionSystem.Instance.SelectedUnit.ActionPoints;
    }

}
