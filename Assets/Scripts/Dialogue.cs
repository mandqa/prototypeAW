using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject InteractPrompt;
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject NextLinePrompt;
    [SerializeField] private string[] DialogueLines;
    [SerializeField] private float TypeSpeed = 0.0f;

    [SerializeField] private PlayerMovement playerMovement;
    
    private int lineIndex = 0;

    private bool isInteracting = false;
    private bool canInteract = false;

    // Update is called once per frame
    private void Update()
    {
        InteractPrompt.SetActive(canInteract);

        if (canInteract && !isInteracting && Input.GetKeyDown(KeyCode.Space))
            StartDialogue();

        if (isInteracting && NextLinePrompt.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
            NextLine();
    }

    private void StartDialogue()
    {
        canInteract = false;
        isInteracting = true;
        
        playerMovement.FreezePlayer();
        
        DialogueText.text = "";
        NextLinePrompt.SetActive(false);
        DialogueBox.SetActive(true);
        
        lineIndex = 0;
        StartCoroutine(WriteLine());
    }
    
    private IEnumerator WriteLine()
    {
        foreach (char c in DialogueLines[lineIndex])
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(TypeSpeed);
        }

        NextLinePrompt.SetActive(true);
    }

    private void NextLine()
    {
        if (lineIndex < DialogueLines.Length - 1)
        {
            DialogueText.text = "";
            NextLinePrompt.SetActive(false);

            lineIndex++;
            StartCoroutine(WriteLine());
        }
        else
        {
            DialogueBox.SetActive(false);
            NextLinePrompt.SetActive(false);
            isInteracting = false;
            playerMovement.UnfreezePlayer();
            canInteract = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !isInteracting)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
            {
            canInteract = false;
            }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
}
