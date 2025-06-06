using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{        
    public List<GameObject> prefabs;
    public int conteudoMaxCartas => prefabs.Count;
    public float tamanhoDoCartao = 2f;
    [HideInInspector] public int conteudoDoCartao = -1;
    [HideInInspector] public UnityEvent<CardController> cartaClicada;

    private Animator animator;    
    private AudioSource audioSource;
    [SerializeField] private AudioClip clipRevelarCartao;       

    public bool InteracaoPermitida
    {
        get; set;
    } = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (conteudoDoCartao < 0)
        {
            conteudoDoCartao = UnityEngine.Random.Range(0, prefabs.Count);
        }
        Instantiate(prefabs[conteudoDoCartao], transform.position, Quaternion.identity, transform);
    }

    private void OnMouseUpAsButton()
    {
        if (InteracaoPermitida)
        {
            cartaClicada.Invoke(this);
        }
    }

    public void RevelarCarta()
    {
        animator.SetBool("revealed", true);
        
        if (clipRevelarCartao != null)
        {
            audioSource.PlayOneShot(clipRevelarCartao);
        }
    }

    public void EsconderCarta()
    {
        animator.SetBool("revealed", false);
    }
}