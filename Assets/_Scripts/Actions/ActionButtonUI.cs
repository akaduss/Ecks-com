using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button button;
    [SerializeField] public Image selectedVisual;


    private void Start()
    {
        selectedVisual.enabled = false;
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        textMesh.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.selectedAction = baseAction;
        }
        );
    }
}
