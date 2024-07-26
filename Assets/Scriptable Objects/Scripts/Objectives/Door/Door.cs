using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] UIPasswordPanel passwordPanel;
    [SerializeField] Collider2D collider2d; // change for 3d collisions

    public string Password { get; private set;}

    private void Awake()
    {
        TryGetComponent(out collider2d);
    }

    private void Start()
    {
        Password password = new Password();
        Password = password.GetPassword();
        Debug.Log("Password is: " + Password);
    }

    public string GetDoorPassword() // For password Note
    {
        return Password;
    }

    // Implement a note and UI(payday2 security pad alike) that uses this
    public void OpenDoor()
    {
        // Open
        collider2d.enabled = !collider2d.isActiveAndEnabled;
    }

    public void Interact() // Separate the keypad and door logic
    {
        passwordPanel.SetPanelDoorAndPassword(this);
    }
}
