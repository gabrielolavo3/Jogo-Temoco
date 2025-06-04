using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarPecaUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public RectTransform alvo;
    public float distanciaDeEncaixe = 50f;

    private Vector2 posicaoInicial;
    private bool _encaixada = false; // agora é privado com underline

    public bool encaixada => _encaixada; // isso permite que outros scripts leiam se a peça foi encaixada

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_encaixada) return;
        posicaoInicial = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_encaixada) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_encaixada) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_encaixada) return;

        canvasGroup.blocksRaycasts = true;

        float distancia = Vector2.Distance(rectTransform.anchoredPosition, alvo.anchoredPosition);
        if (distancia <= distanciaDeEncaixe)
        {
            rectTransform.anchoredPosition = alvo.anchoredPosition;
            _encaixada = true;
        }
        else
        {
            rectTransform.anchoredPosition = posicaoInicial;
        }
    }
}
