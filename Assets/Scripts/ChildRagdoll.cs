using UnityEngine;

public class ChildRagdoll : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponentInParent<RagdollMe>()?.Ragdoll();
        enabled = false;
    }
}
