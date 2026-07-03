using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Target")]
    [SerializeField] private Transform player;

    [Header("Camera Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 16f, -8f);
    [SerializeField] private float followSpeed = 8f;

    private Transform currentTarget;

    private void Awake()
    {
        currentTarget = player;
    }

    public void SetCameraTarget(Transform t)
    {
        currentTarget = t;
    }

    public void ResetToPlayer()
    {
        currentTarget = player;
    }

    private void LateUpdate()
    {
        if (currentTarget == null) return;

        Vector3 desiredPosition;
        Quaternion desiredRotation;

        if (currentTarget == player)
        {
            desiredPosition = player.position + offset;
            desiredRotation = Quaternion.Euler(60f, 0f, 0f);
        }
        else
        {
            desiredPosition = currentTarget.position;
            desiredRotation = currentTarget.rotation;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            followSpeed * Time.deltaTime
        );
    }
}