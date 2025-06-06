using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTravaFase : MonoBehaviour
{
    [Header("Referências do Painel")]
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private Text tituloText;
    [SerializeField] private Text requisitosText;
    [SerializeField] private Button cancelarBtn;

    private void Awake()
    {
        // Garante que o painel começa desativado
        popUpPanel.SetActive(false);

        // Configura o botão para fechar o painel
        cancelarBtn.onClick.AddListener(FecharPopUp);
    }
    
    // Mostra o PopUp com título e descrição personalizáveis.
    
    public void MostrarPopUp(string titulo, string requisitos)
    {
        tituloText.text = titulo;
        requisitosText.text = requisitos;
        popUpPanel.SetActive(true);
    }
    
    // Oculta o PopUp.
    
    public void FecharPopUp()
    {
        popUpPanel.SetActive(false);
    }
}
