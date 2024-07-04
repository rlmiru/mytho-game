using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public float interactionRange = 1f;
    public KeyCode interactKey = KeyCode.Return;
    public GameObject interactionUI;
    [TextArea]
    public string description;
    public float currency = 0;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
        else
        {
            Debug.LogError("interactionUI is not assigned in the inspector.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= interactionRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                Interact();
            }
        }
        else
        {
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }

    private void Interact()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    public void Buy(int cardCost)
    {
        if (currency >= cardCost)
        {
            currency -= cardCost;
        }
    }
}
