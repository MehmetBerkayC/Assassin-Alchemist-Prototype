using UnityEngine;
using TMPro;

public class UIPasswordPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI passwordTextObject;
    [SerializeField] UnityEngine.GameObject passwordPanel;

    Door _currentDoor;
    string _currentPassword = "";
    string _enteredPassword = "";

    private void OnEnable()
    {
        UpdateDisplay();
    }

    public void TogglePasswordPanel()
    {
        passwordPanel.SetActive(!passwordPanel.activeInHierarchy);
    }

    public void SetPanelDoorAndPassword(Door door)
    {
        _currentDoor = door;
        _currentPassword = door.GetDoorPassword();
        // For now, don't have a pad - this will do
        TogglePasswordPanel();
    }

    public void OnClick_Number(string number)
    {
        if (_enteredPassword.Length >= 4)
        {
            return;
        }
        _enteredPassword = _enteredPassword + $"{number}";
        UpdateDisplay();
    }

    public void OnClick_Delete() // Reset password display
    {
        ResetPassword();
    }

    public void OnClick_Enter() // Check password
    {
        if(_currentPassword != _enteredPassword)
        {
            ResetPassword();
        }
        else
        {
            _currentDoor.OpenDoor();
            // Close UI and play audio?
            TogglePasswordPanel();
        }
    }

    private void ResetPassword()
    {
        _enteredPassword = "";
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        passwordTextObject.text = _enteredPassword;
    }
}
