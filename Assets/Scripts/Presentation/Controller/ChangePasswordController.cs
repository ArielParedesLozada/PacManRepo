using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangePasswordController : MonoBehaviour
{
    public InputField inputNombre;
    public InputField inputNuevaPassword;
    public Button cambiarBtn;
    public Text mensajeTexto;

    private SQLitePlayerDatabase _database;

    [Inject]
    public void Construct(IDatabase<PlayerEntity> db)
    {
        _database = db as SQLitePlayerDatabase;
    }

    private void Start()
    {
        cambiarBtn.onClick.AddListener(OnCambiarClick);
    }

    private void OnCambiarClick()
    {
        string nombre = inputNombre.text.Trim();
        string nueva = inputNuevaPassword.text.Trim();

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(nueva))
        {
            Mostrar("Campos requeridos.");
            return;
        }

        var action = new ChangePasswordAction(_database);
        if (action.TryChangePassword(nombre, nueva, out string error))
        {
            Mostrar("Contraseña actualizada.");
        }
        else
        {
            Mostrar(error);
        }
    }

    private void Mostrar(string mensaje)
    {
        if (mensajeTexto != null)
            mensajeTexto.text = mensaje;
    }
}
