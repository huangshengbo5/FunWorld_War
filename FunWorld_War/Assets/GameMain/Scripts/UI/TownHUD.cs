using Script.Game.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownHUD : MonoBehaviour
{
    public Button Btn_Enter;
    public Solider Solider_Hp;
    public Image Image_Camp;
    public TextMeshProUGUI Text_Num;
    
    
    private BaseObject parent_Obj;
    
    public void Init(BaseObject parent)
    {
        parent_Obj = parent;
        Btn_Enter.onClick.AddListener(HandlerClickEnter);
        Town ownerTown = parent_Obj as Town;
        ownerTown.DelegateTownCampChange += OnTownCampChange;
        ownerTown.DelegateTownHpChange += OnTownHpChange;
        ownerTown.DelegateTownSoliderNumChange += OnTownSoliderNumChange;
    }

    public void OnTownCampChange(CampType campType)
    {
        
    }

    public void OnTownHpChange(int curHp, int maxHp)
    {
        
    }

    public void OnTownSoliderNumChange(int curNum, int maxNum)
    {
        
    }
    void HandlerClickEnter()
    {
        var Town = parent_Obj as Town;
        GameEntry.Event.Fire(this,BattleClickTargetTownEventArgs.Create(Town));
        gameObject.SetActive(false);
    }
    
    private void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}