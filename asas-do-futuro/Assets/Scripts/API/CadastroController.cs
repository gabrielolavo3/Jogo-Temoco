using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CadastroController : MonoBehaviour
{
    public InputField emailCadastroField;
    public InputField senhaCadastroField;
    public Button cadastroButton;
    public Button voltarParaLoginButton;

    private const string cadastroURL = "http://localhost:3000/api/usuario/cadastro";

    void Start()
    {
        cadastroButton.onClick.AddListener(() => StartCoroutine(FazerCadastro()));
        voltarParaLoginButton.onClick.AddListener(() => SceneManager.LoadScene("LoginScene"));
    }

    IEnumerator FazerCadastro()
    {
        string email = emailCadastroField.text;
        string senha = senhaCadastroField.text;

        var json = JsonUtility.ToJson(new Usuario(email, senha));
        var request = new UnityWebRequest(cadastroURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Cadastro realizado com sucesso");
            SceneManager.LoadScene("LoginScene");
        }
        else
        {
            Debug.LogError("Erro no cadastro: " + request.error);
        }
    }

    [System.Serializable]
    public class Usuario
    {
        public string email;
        public string senha;

        public Usuario(string email, string senha)
        {
            this.email = email;
            this.senha = senha;
        }
    }
}
