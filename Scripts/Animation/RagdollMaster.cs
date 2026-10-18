using UnityEngine;

public class RagdollMaster : MonoBehaviour
{
    [SerializeField] private Rigidbody originalBody;
    [SerializeField] private Collider[] originalCollider;
    [SerializeField] private Collider[] attackCollider;
    [SerializeField] private Animator animator;

    void Awake()
    {
        SetRagdollState(false);    
    }

    void OnEnable()
    {
        SetRagdollState(false);
    }

    public void SetRagdollState(bool b)
    {
        animator.enabled = !b;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();   
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !b;
        }
        foreach(Collider collider in colliders)
        {
            collider.enabled = b;
        }

        originalBody.isKinematic = b;
        foreach (Collider collider in originalCollider)
            collider.enabled = !b;
        foreach (Collider collider in attackCollider)
            collider.enabled = false;
    }
}
