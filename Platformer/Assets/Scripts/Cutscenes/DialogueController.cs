using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    [SerializeField]private int index =0;
    public float dialogueSpeed;
    bool writing;
    [SerializeField] float fadeDuration=1f;
    [SerializeField] CanvasGroup group;
    public void OnEnable()
    {
        group.DOFade(1, fadeDuration);
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                NextSentence();
                Disable();
            }
        }
    }
    void Disable()
    {
        if (index > sentences.Length-1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }
   
    void NextSentence()
    {
        if (index <= sentences.Length-1 && !writing)
        {
            dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }
    IEnumerator WriteSentence()
    {
        foreach(char Character in sentences[index].ToCharArray())
        {
            writing = true;
            dialogueText.text += Character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        writing = false;
        index++;
    }
    
}
