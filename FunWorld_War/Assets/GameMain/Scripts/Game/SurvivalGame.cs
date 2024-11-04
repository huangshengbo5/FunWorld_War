using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

public class SurvivalGame : GameBase
{
    private float m_ElapseSeconds = 0f;

    public override GameMode GameMode => GameMode.Survival;

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);

        m_ElapseSeconds += elapseSeconds;
        if (m_ElapseSeconds >= 1f) m_ElapseSeconds = 0f;
    }
}