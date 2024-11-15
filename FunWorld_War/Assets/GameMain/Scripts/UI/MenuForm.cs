using UnityEngine;
using UnityGameFramework.Runtime;

public class MenuForm : UIFormLogic
{
    [SerializeField] private GameObject m_QuitButton = null;

    public void OnStartButtonClick()
    {
        
    }

    public void OnSettingButtonClick()
    {
        
    }

    public void OnAboutButtonClick()
    {
        
    }

    public void OnQuitButtonClick()
    {
        
    }
    
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
}