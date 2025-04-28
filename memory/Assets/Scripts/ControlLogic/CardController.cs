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
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _revelarCartao;
    [SerializeField] private AudioClip _esconderCartao;
    [SerializeField] private float _velocidadePicthAudio;

    public bool IsInteractable
    {
        get; set;
    } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
            yield return new WaitForSeconds(2f);
            Hide();
        }

        StartCoroutine(AnimationCoroutine());
    }

    public void Reveal()
    {
        _animator.SetBool("revealed", true);
        
        if (_revelarCartao != null)
        {
            _audioSource.PlayOneShot(_revelarCartao);
        }
    }

    public void Hide()
    {
        _animator.SetBool("revealed", false);

        if (_esconderCartao != null)
        {
            _audioSource.pitch = _velocidadePicthAudio;
            _audioSource.PlayOneShot(_esconderCartao);
        }
    }
}