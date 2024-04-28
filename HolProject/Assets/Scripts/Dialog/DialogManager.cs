using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System.Collections;
using System;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;

    private PlayerInput _playerInput;
    private int currentSentenceIndex = 0;

    [SerializeField] private TMP_Text _nameCharacter;
    [SerializeField] private TMP_Text _dialogeText;

    [SerializeField] private Animator _animator;

    [SerializeField] private AudioClip[] voiceClips;
    public static Action<AudioClip> OnVoiceSounded;

    [SerializeField] GameObject _bookDung;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.DialogueActivator.NextSentence.performed += ctx => DisplayNextSentence();
        sentences = new Queue<string>();
        _bookDung.SetActive(false);
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
        currentSentenceIndex = 0;

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

        if (currentSentenceIndex < voiceClips.Length)
        {
            AudioClip clip = voiceClips[currentSentenceIndex];
            if (clip != null)
            {
                OnVoiceSounded?.Invoke(clip);
            }
            currentSentenceIndex++;
        }
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
        _bookDung.SetActive(true);
    }
}
