using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class OptionToggle : MonoBehaviour
{
    [SerializeField] GameObject optionUI;
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject raiderPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] PlayerMovement player;


    public InputActionReference ESCAction;

    private bool isOpen = false;

    private void OnEnable()
    {
        if (ESCAction != null)
        {
            ESCAction.action.performed += OnEsc;
            ESCAction.action.Enable();

        }
    }

    private void OnEsc(InputAction.CallbackContext ctx)
    {
        if (isOpen && optionUI.activeSelf)
        {
            UIOnOff(false);
        }
        else if (!isOpen && !optionUI.activeSelf)
        {
            if (!levelPanel.activeSelf && !upgradePanel.activeSelf && !raiderPanel.activeSelf && !WinPanel.activeSelf && !LosePanel.activeSelf)
                UIOnOff(true);
        }
        
    }

    public void UIOnOff(bool b)
    {
        isOpen = b;
        if (b) optionUI.GetComponent<OptionUI>().Init();
        if (optionUI.GetComponent<OptionUI>().IsReallyResetOptionOpened()) optionUI.GetComponent<OptionUI>().CloseReallyResetOption();
        if (optionUI.GetComponent<OptionUI>().IsReallyExitGameOpened()) optionUI.GetComponent<OptionUI>().CloseReallyExitGame();
        if (optionUI.GetComponent<OptionUI>().IsReallyBackToMainOpened()) optionUI.GetComponent<OptionUI>().CloseReallyBackToMain();

        optionUI.SetActive(b);

        if (b)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (b)
        {
            player.DisablePlayerAction();
        }
        else
        {
            player.EnablePlayerAction();
        }
    }

    public bool GetIsOpen() => isOpen;
}
