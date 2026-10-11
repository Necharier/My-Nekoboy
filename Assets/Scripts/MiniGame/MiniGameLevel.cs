using UnityEngine;

public class MiniGameLevel : MonoBehaviour
{
    public Transform begin;

    
    public Vector3 BeginOffset => begin.position - transform.position;

    public void PlaceBeginAt(Vector3 worldPoint)
    {
        transform.position = worldPoint - BeginOffset;
    }

    private void OnDrawGizmos()
    {
        if (begin) { Gizmos.color = Color.green; Gizmos.DrawWireSphere(begin.position, 0.3f); }
        //if (end)   { Gizmos.color = Color.red;   Gizmos.DrawWireSphere(end.position, 0.3f); }
    }
}
