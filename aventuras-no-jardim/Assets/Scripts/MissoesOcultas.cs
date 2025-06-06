using UnityEngine;

public static class MissoesOcultas
{
    // Para Procurador Mestre
    private static bool encontrouGalho => PlayerPrefs.GetInt("Pista_Galho", 0) == 1;
    private static bool encontrouPegadas => PlayerPrefs.GetInt("Pista_Pegadas", 0) == 1;
    private static bool encontrouGato => PlayerPrefs.GetInt("Pista_Gato", 0) == 1;

    // Para Curioso por Natureza
    private static bool encontrouQualquerPista => PlayerPrefs.GetInt("CuriosoPorNatureza", 0) == 1;

    // Para Descoberta Incansável
    private static bool acessouFase1 => PlayerPrefs.GetInt("Acessou_Fase1", 0) == 1;
    private static bool acessouFase2 => PlayerPrefs.GetInt("Acessou_Fase2", 0) == 1;
    private static bool acessouFase3 => PlayerPrefs.GetInt("Acessou_Fase3", 0) == 1;

    // Método chamado quando uma pista é encontrada
    public static void EncontrouPista(string nomeDaPista)
    {
        PlayerPrefs.SetInt("Pista_" + nomeDaPista, 1);

        // Marca que encontrou qualquer pista (Curioso por Natureza)
        if (!encontrouQualquerPista)
        {
            PlayerPrefs.SetInt("CuriosoPorNatureza", 1);
            Debug.Log("[MissoesOcultas] Missão 'Curioso por Natureza' concluída!");
        }

        // Verifica se completou todas as pistas da Fase1 (Procurador Mestre)
        if (encontrouGalho && encontrouPegadas && encontrouGato)
        {
            if (PlayerPrefs.GetInt("ProcuradorMestre", 0) == 0)
            {
                PlayerPrefs.SetInt("ProcuradorMestre", 1);
                Debug.Log("[MissoesOcultas] Missão 'Procurador Mestre' concluída!");
            }
        }

        PlayerPrefs.Save();
    }

    // Método chamado quando o jogador acessa uma fase
    public static void AcessouFase(string nomeDaFase)
    {
        PlayerPrefs.SetInt("Acessou_" + nomeDaFase, 1);

        // Verifica se acessou todas as fases
        if (acessouFase1 && acessouFase2 && acessouFase3)
        {
            if (PlayerPrefs.GetInt("DescobertaIncansavel", 0) == 0)
            {
                PlayerPrefs.SetInt("DescobertaIncansavel", 1);
                Debug.Log("[MissoesOcultas] Missão 'Descoberta Incansável' concluída!");
            }
        }

        PlayerPrefs.Save();
    }
}
