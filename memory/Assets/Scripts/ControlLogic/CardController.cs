using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{
    public int maxCardTypes => prefabs.Count;
    public int cardtype = -1;
    public UnityEvent<CardController> onClicked;
    public List<GameObject> prefabs;
    public float cardSize = 2f;

    private Animator _animator;

    public bool IsInteractable 
    {
        get; set; 
    } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        if (cardtype < 0)
        {
            cardtype = UnityEngine.Random.Range(0, prefabs.Count);
        }
        Instantiate(prefabs[cardtype], transform.position, Quaternion.identity, transform);
    }

    private void OnMouseUpAsButton()
    {
        if (IsInteractable)
        {
            onClicked.Invoke(this);
        }
    }

    public void TestAnimation()
    {
        IEnumerator AnimationCoroutine()
        {
            Reveal();
            yield return new WaitForSeconds(2);
            Hide();
        }

        StartCoroutine(AnimationCoroutine());
    }

    public void Reveal()
    {
        _animator.SetBool("revealed", true);
    }

    public void Hide()
    {
        _animator.SetBool("revealed", false);
    }
}