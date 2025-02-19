using UnityEngine;

public struct JointData
{
    public Joint joint;
    public float previousForce;

    public JointData(Joint newJoint)
    {
        this.joint = newJoint;
        previousForce = 0f;
    }
}

public class RagdollMe : MonoBehaviour
{
    Animator animator;
    public JointData[] jointData;

    void Start()
    {
        animator = GetComponent<Animator>();

        GetAllJoints();
        AddChildRagdoll();
        SetChildColliders(false);
    }

    private void Update()
    {
        float scoreTotal = 0f;

        for (int i = 0; i < jointData.Length; i++)
        {
            var joint = jointData[i];

            float newScore = Mathf.Abs(joint.previousForce - joint.joint.currentForce.magnitude);

            scoreTotal += newScore;

            joint.previousForce = joint.joint.currentForce.magnitude;
        }
    }

    Joint[] GetAllJoints()
    {
        Joint[] joints = GetComponentsInChildren<Joint>();

        jointData = new JointData[joints.Length];

        for (int i = 0; i < jointData.Length; i++)
        {
            Joint joint = joints[i];
            jointData[i] = new JointData(joint);
        }

        return joints;
    }

    void AddChildRagdoll()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.gameObject.AddComponent<Rigidbody>();
        }
    }

    void SetChildColliders(bool enabled)
    {
        Collider currentCol = GetComponent<Collider>();

        foreach (var col in GetComponentsInChildren<Collider>())
        {
            if (col == currentCol)
            {
                currentCol.enabled = !enabled;
            }

            else 
            {
                col.enabled = enabled;
            }
        }
    }

    public void Ragdoll(Vector3 impact)
    {
        if (impact.magnitude < 3f)
        {
            return;
        }

        SetChildColliders(true);

        animator.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ragdoll(collision.relativeVelocity);
    }
}
