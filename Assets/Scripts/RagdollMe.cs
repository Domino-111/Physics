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
    Joint[] jointData;

    void Start()
    {
        GetComponent<Animator>();
    }

    private void Update()
    {
        float scoreTotal = 0f;

        for (int i = 0; i < jointData.Length; i ++)
        {
            var joint = jointData[i];

            float newScore = Mathf.Abs(joint.previousForce - joint.joint.currentForce.magnitude);

            joint.previousForce = joint.joint.currentForce.magnitude;
        }
    }

    Joint[] GetAllJoints()
    {
        Joint[] joints = GetComponentsInChildren<Joint>();

        JointData = new JointData[joints.Length];

        for (int i = 0; i < jointData.Length; i ++)
        {
            Joint joint = joints[i];
            jointData[i] = new JointData(joint);
        }

        return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.enabled = false;
    }
}
