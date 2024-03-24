using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button button;
    [SerializeField] private Image selectedVisual;
    private BaseAction baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMesh.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.selectedAction = baseAction;
        }
        );
    }


    public void UpdateSelectedVisual()
    {
        selectedVisual.gameObject.SetActive(UnitActionSystem.Instance.selectedAction == baseAction);
    }
}
