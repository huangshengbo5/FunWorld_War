using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class HPBar : MonoBehaviour
{
    public Slider Slider_HP;
    public TextMeshProUGUI Text_HP;

    public void UpdatgeHP(float CurHP,float MaxHP)
    {
        Slider_HP.value = CurHP / MaxHP;
        Text_HP.text = string.Format("%d/%d", CurHP, MaxHP);
    }
    
    private void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}