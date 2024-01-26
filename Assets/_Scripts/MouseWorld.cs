using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance { get; private set; }

    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        transform.position = GetMousePos();
    }

    public static Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Instance.layerMask);
        return hit.point;
    }
}
