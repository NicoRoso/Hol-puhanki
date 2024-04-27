using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;

    private PlayerInput _playerInput;

    [SerializeField] private TMP_Text _nameCharacter;
    [SerializeField] private TMP_Text _dialogeText;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.DialogueActivator.NextSentence.performed += ctx => DisplayNextSentence();
        sentences = new Queue<string>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void StartDialogue(Dialog dialogue)
    {
        _animator.SetBool("IsOpen", true);

        _nameCharacter.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogeText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            _dialogeText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        _animator.SetBool("IsOpen", false);
        FindObjectOfType<DialogTrigger>().enabled = true;
    }
}
