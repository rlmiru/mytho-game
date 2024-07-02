using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public float interactionRange = 1f;
    public KeyCode interactKey = KeyCode.Return;
    public GameObject interactionUI;
    private GameObject player;
    public GameObject inv;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= interactionRange && !inv.activeSelf)
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

}
