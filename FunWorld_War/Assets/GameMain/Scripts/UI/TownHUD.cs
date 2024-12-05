using GameFramework.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownHUD : MonoBehaviour
{
    public Button Btn_Enter;
    public Slider Solider_Hp;
    public Image Image_Camp;
    public TextMeshProUGUI Text_Num;
    private BaseObject parent_Obj;
    
    public void Init(BaseObject parent)
    {
        Btn_Enter.gameObject.SetActive(false);
        parent_Obj = parent;
        Btn_Enter.onClick.AddListener(HandlerClickEnter);
        Town ownerTown = parent_Obj as Town;
        ownerTown.DelegateTownCampChange += OnTownCampChange;
        ownerTown.DelegateTownHpChange += OnTownHpChange;
        ownerTown.DelegateTownSoliderNumChange += OnTownSoliderNumChange;
    }

    public void OnTownCampChange(CampType campType)
    {
        var assetName = Common.GetCampImagePath(campType);
        var fullPath = AssetUtility.GetTextureAsset(assetName);
        LoadAssetCallbacks callBack = new LoadAssetCallbacks((string assetName,object asset,float duration,object userData) =>
        {
            if (asset != null)
            {
                var texture = asset  as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                if (texture != null)
                {
                    Image_Camp.sprite = sprite;
                }
            }
        });
        GameEntry.Resource.LoadAsset(fullPath,callBack);
    }

    public void OnTownHpChange(int curHp, int maxHp)
    {
        Solider_Hp.value = curHp / maxHp;
    }

    public void OnTownSoliderNumChange(int curNum, int maxNum)
    {
        Text_Num.text = string.Format("{0}/{1}", curNum, maxNum);
    }
    void HandlerClickEnter()
    {
        var Town = parent_Obj as Town;
        GameEntry.Event.Fire(this,BattleClickTargetTownEventArgs.Create(Town));
        Btn_Enter.gameObject.SetActive(false);
    }
    
    private void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }

    public void ShowOperateBtn()
    {
        Btn_Enter.gameObject.SetActive(true);
    }
    
}