using UnityEngine;

public static class DesbloqueioDeFases
{
    private const string FASE2_KEY = "Fase2Liberada_V3";
    private const string FASE3_KEY = "Fase3Liberada_V3";

    public static void ChecarDesbloqueio(string nomeFase, int pontos)
    {
        Debug.Log($"Verificando desbloqueio para {nomeFase} com {pontos} pontos");

        if (nomeFase == "Fase1" && pontos >= 200)
        {
            PlayerPrefs.SetInt(FASE2_KEY, 1);
            Debug.Log("FASE 2 DESBLOQUEADA!");
        }
        else if (nomeFase == "Fase2" && pontos >= 350)
        {
            PlayerPrefs.SetInt(FASE3_KEY, 1);
            Debug.Log("FASE 3 DESBLOQUEADA!");
        }

        PlayerPrefs.Save();
        DebugSalvarStatus();
    }

    public static bool Fase2EstaLiberada() => PlayerPrefs.GetInt(FASE2_KEY, 0) == 1;

    public static bool Fase3EstaLiberada() => PlayerPrefs.GetInt(FASE3_KEY, 0) == 1;

    private static void DebugSalvarStatus()
    {
        Debug.Log($"Status Salvo - Fase2: {PlayerPrefs.GetInt(FASE2_KEY)} | Fase3: {PlayerPrefs.GetInt(FASE3_KEY)}");
    }
}
