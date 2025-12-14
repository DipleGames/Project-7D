using UnityEngine;

public class ResourceInteraction : MonoBehaviour
{
    private Animator animator;
    private ResourceNode targetNode;

    private float gatherTimer = 0f;
    private bool isHolding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (targetNode != null)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (!isHolding)
                {
                    isHolding = true;
                    animator.SetBool("isChopping", true);
                    UIManager.Instance.ShowGatherUI();
                }

                gatherTimer += Time.deltaTime;
                UIManager.Instance.SetGatherProgress(gatherTimer / targetNode.gatherTime);

                if (gatherTimer >= targetNode.gatherTime)
                {
                    OnGatherComplete();
                }
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                ResetGathering();
            }
        }
        else
        {
            ResetGathering();
        }
    }

    void OnGatherComplete()
    {
        animator.SetBool("isChopping", false);
        Destroy(targetNode.gameObject);
        PlayerInventory.Instance.AddResource(targetNode.Data, targetNode.getAmount);
        targetNode = null;
        ResetGathering();
    }

    void ResetGathering()
    {
        isHolding = false;
        animator.SetBool("isChopping", false);
        gatherTimer = 0f;
        UIManager.Instance.HideGatherUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceNode node))
        {
            targetNode = node;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ResourceNode node) && node == targetNode)
        {
            targetNode = null;
            ResetGathering();
        }
    }
}
