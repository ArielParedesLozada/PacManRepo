using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePassword : MonoBehaviour
{
    [SerializeField] private Button cambiarClaveButton;

    private void Start()
    {
        cambiarClaveButton.onClick.AddListener(OnCambiarClaveButtonClick);
    }
    private void OnCambiarClaveButtonClick()
    {
        SceneManager.LoadScene("CambiaClave");
    }
}
