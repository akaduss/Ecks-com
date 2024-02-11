using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer transposer;

    //make all serialized fields private
    [SerializeField] private float zoomLerpingValue = 90f;
    [SerializeField] private float minFollowY = 1f;
    [SerializeField] private float maxFollowY = 10f;
    [SerializeField] private float moveSpeed = 10f;

    private void Start()
    {
        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.y = 5f;
    }

    void Update()
    {
        HandleMovement();

        HandleRotation();

        HandleZooming();
    }

    private void HandleMovement()
    {
        // if input wasd pressed set vecter3 movedir
        Vector3 moveDir = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVector = transform.forward * moveDir.z + transform.right * moveDir.x;
        transform.position += moveSpeed * Time.deltaTime * moveVector;
    }

    private void HandleRotation()
    {
        // rotate camera with q and e on y axis
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        }
    }

    private void HandleZooming()
    {
        float newOffset = transposer.m_FollowOffset.y;

        if (Input.mouseScrollDelta.y != 0)
        {
            newOffset += Mathf.Sign(Input.mouseScrollDelta.y);
            newOffset = Mathf.Clamp(newOffset, minFollowY, maxFollowY);

        }
        transposer.m_FollowOffset.y = Mathf.Lerp(transposer.m_FollowOffset.y, newOffset, zoomLerpingValue * Time.deltaTime);
    }
}
