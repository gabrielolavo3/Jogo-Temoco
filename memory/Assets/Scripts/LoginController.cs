using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

[System.Serializable]
public class LoginResponse
{
    public string token;
    public int idUsuario;
}

public class LoginController : MonoBehaviour
{
    public InputField emailLoginField;
    public InputField senhaLoginField;
    public Button loginButton;
    public Button irParaCadastroButton;
    private const string loginURL = "http://localhost:3000/api/usuario/login";

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() => StartCoroutine(FazerLogin()));
        irParaCadastroButton.onClick.AddListener(() => SceneManager.LoadScene("CadastroScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FazerLogin()
    {
        string email = emailLoginField.text;
        string senha = senhaLoginField.text;

        var json = JsonUtility.ToJson(new Usuario(email, senha));
        var request = new UnityWebRequest(loginURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login bem-sucedido");
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            PlayerPrefs.SetString("token", response.token);
            PlayerPrefs.SetInt("idUsuario", response.idUsuario);
            PlayerPrefs.Save();
            Debug.Log($"Token recebido: {response.token}\nID do usuário: {response.idUsuario}");
            SceneManager.LoadScene("SelectDifficulty");
        }
        else
        {
            Debug.LogError("Erro no login: " + request.error);
        }
    }
}
