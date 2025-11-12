using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioSource audioSources;
    public AudioClip[] audioClips;
    public float textSpeed;
    public GameObject dialoguePanel;

    private int Index;

    private void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
        audioSources = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[Index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[Index];
            }
        }
    }

    void startDialogue()
    {
        Index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (audioClips != null && audioClips.Length > Index && audioClips[Index] != null)
        {
            audioSources.PlayOneShot(audioClips[Index]);
        }
        foreach (char c in lines[Index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (Index < lines.Length - 1)
        {
            Index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            dialoguePanel.gameObject.SetActive(false);
        }
    }
}
