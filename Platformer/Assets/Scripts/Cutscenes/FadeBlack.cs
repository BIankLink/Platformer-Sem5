using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    CanvasGroup g;
    [SerializeField] float duration;
    [SerializeField] GameObject splashArt;

    private void Awake()
    {
        g = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        g.DOFade(1, duration);
    }
    // Update is called once per frame
    void Update()
    {
        if (g.alpha == 1)
        {
            splashArt.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        g.DOFade(0, duration);
    }
}
