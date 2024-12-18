using System;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class BattleWinForm : UIFormLogic
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        AddEvent();
    }

    void AddEvent()
    {
        GameEntry.Event.Subscribe(BattleClickPlayerTownEventArgs.EventId,HandlerBattleClickPlayerTown);
    }

    void RemoveEvent()
    {
        GameEntry.Event.Unsubscribe(BattleClickPlayerTownEventArgs.EventId,HandlerBattleClickPlayerTown);
    }

    void HandlerBattleClickPlayerTown(object o,EventArgs e)
    {

    }
    
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        RemoveEvent();
    }
}