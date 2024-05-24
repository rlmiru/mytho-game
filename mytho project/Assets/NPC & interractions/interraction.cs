using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float interactionRange = 1f;
    public KeyCode interactKey = KeyCode.Return;
    public GameObject interactionUI;
    public Text descriptionText; 
    [TextArea]
    public string description;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactionUI.SetActive(false);
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
            interactionUI.SetActive(false);
        }
    }

    private void Interact()
    {
        interactionUI.SetActive(true);
        descriptionText.text = description;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}