using UnityEngine;
using UnityEngine.UI;

public class TownHUD : MonoBehaviour
{
    public Button Btn_Enter;
    
    public void Init(BaseObject parent)
    {
        Btn_Enter.onClick.AddListener(HandlerClickEnter);
    }

    void HandlerClickEnter()
    {
        
    }
    
    private void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}