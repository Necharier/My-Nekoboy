using UnityEngine;

public class CamerBehaviour : MonoBehaviour
{
    public Vector3 camOffSet = new(0f, 1.2f, -2.6f);

    private Transform target;
    private Vector3 savedPosition;
    private Quaternion savedRotation;

    
    public void Follow(Transform newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogWarning("CamerBehaviour: цель не задана", this);
            return;
        }

        
        if (target == null)
        {
            savedPosition = transform.position;
            savedRotation = transform.rotation;
        }

        target = newTarget;
        UpdateCamera(); 
    }

    
    public void ResetCamera()
    {
        if (target == null) return;

        target = null;
        transform.SetPositionAndRotation(savedPosition, savedRotation);
    }

    private void LateUpdate()
    {
        if (target != null)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        transform.position = target.TransformPoint(camOffSet);
        transform.LookAt(target);
    }
}