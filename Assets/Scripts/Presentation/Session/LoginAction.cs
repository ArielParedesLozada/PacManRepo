using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LoginAction : MonoBehaviour
{
    [SerializeField] private InputField nombreInput;
    [SerializeField] private InputField claveInput;

    [SerializeField] private Button ingresarButton;

    private IDatabase<PlayerEntity> _database;
    private IPlayerSessionProvider _strategyProvider;
    [Inject]
    public void Construct(
        IPlayerSessionProvider provider
    )
    {
        _strategyProvider = provider;
    }

    private void Start()
    {
        ingresarButton.onClick.AddListener(OnIngresarClick);
    }

    private void OnIngresarClick()
    {
        string nombre = nombreInput.text.Trim();
        string clave = claveInput.text.Trim();

        if (string.IsNullOrEmpty(nombre))
        {
            Debug.LogWarning("⚠️ El nombre no puede estar vacío.");
            return;
        }

        try
        {
            ISetPlayerSession setPlayer = _strategyProvider.GetPlayerSession(nombre, clave);
            setPlayer.SetSession(nombre, clave);
            SceneManager.LoadScene("Level1");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Error al iniciar sesión: {ex.Message}");
        }
    }
}
