using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrocarImagemBotao : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite imagemPressionada; // Nova imagem ao pressionar
    private Sprite imagemOriginal;   // Para armazenar a imagem original

    private Image imagemBotao;

    void Start()
    {
        imagemBotao = GetComponent<Image>();
        imagemOriginal = imagemBotao.sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imagemBotao.sprite = imagemPressionada;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imagemBotao.sprite = imagemOriginal;
    }
}