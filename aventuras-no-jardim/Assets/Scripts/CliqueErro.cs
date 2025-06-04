using UnityEngine;

public class CliqueErrado : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EstadoDoJogo.painelAberto) return;

            if (PontuacaoManager.ignorarCliqueErrado)
            {
                PontuacaoManager.ignorarCliqueErrado = false;
                return;
            }

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null)
            {
                if (PontuacaoManager.Existe)
                    PontuacaoManager.instance.RemoverPontos(50);
            }
            else
            {
                if (!hit.collider.CompareTag("Pista"))
                {
                    if (PontuacaoManager.Existe)
                        PontuacaoManager.instance.RemoverPontos(50);
                }
            }
        }
    }
}
