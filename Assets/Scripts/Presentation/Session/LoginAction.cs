using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LoginAction : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private InputField nombreInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Button ingresarButton;
    [SerializeField] private Text mensajeTexto; // Text para mostrar errores

    [Inject] private IDatabase<PlayerEntity> _database;
    [Inject] private IPlayerSessionProvider _strategyProvider;

    private void Start()
    {
        ingresarButton.onClick.AddListener(OnIngresarClick);
    }

    private void OnIngresarClick()
    {
        string nombre = nombreInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(password))
        {
            MostrarMensaje("Nombre y contraseña requeridos.");
            return;
        }

        try
        {
            bool isNew = _database.FindByName(nombre) == null;
            ISetPlayerSession sessionSetter = _strategyProvider.GetPlayerSession(nombre, password, isNew);
            sessionSetter.SetSession(nombre);
            SceneManager.LoadScene("Level1");
        }
        catch (System.Exception ex)
        {
            MostrarMensaje($"❌ {ex.Message}");
        }
    }

    private void MostrarMensaje(string msg)
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = msg;
        }
        else
        {
            Debug.LogWarning(msg);
        }
    }
}
