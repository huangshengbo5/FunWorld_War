using Script.Game.Base;
using UnityEngine;
using UnityEngine.UI;

public class TownHUD : MonoBehaviour
{
    public Button Btn_Enter;
    private BaseObject parent_Obj;
    public void Init(BaseObject parent)
    {
        parent_Obj = parent;
        Btn_Enter.onClick.AddListener(HandlerClickEnter);
    }

    void HandlerClickEnter()
    {
        var baseTown = parent_Obj as BaseTown;
        GameEntry.Event.Fire(this,BattleClickTargetTownEventArgs.Create(baseTown));
        gameObject.SetActive(false);
    }
    
    private void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}