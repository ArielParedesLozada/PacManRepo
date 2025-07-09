using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CambiaClave : MonoBehaviour
{
    [SerializeField] private InputField claveInput;

    [SerializeField] private Button claveButton;
    private PasswordChange _usecase;

    [Inject]
    public void Construct(PasswordChange passwordChange)
    {
        _usecase = passwordChange;
    }
    void Start()
    {
        claveButton.onClick.AddListener(OnCambiarClick);
    }

    private void OnCambiarClick()
    {
        string clave = claveInput.text.Trim();
        try
        {
            _usecase.ChangePassword(clave);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
