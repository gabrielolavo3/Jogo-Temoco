using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{
    public int cardtype = -1;
    private Animator _animator;
    public UnityEvent<CardController> onClicked;
    public int maxCardTypes => prefabs.Count;
    [SerializeField] public List<GameObject> prefabs;
    [SerializeField] public float cardSize = 2f;

    public bool IsInteractable { get; set; } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cardtype < 0)
        {
            cardtype = UnityEngine.Random.Range(0, prefabs.Count);
        }
        Instantiate(prefabs[cardtype], transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {

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