using System;
using UnityEngine;

public class Spawner_Solider : MonoBehaviour
{
    public int Soldier_Id;

    private DRNPC NpcConfig;
    private void Start()
    {
        var NpcConfigs = GameEntry.DataTable.GetDataTable<DRNPC>();
        NpcConfig = NpcConfigs.GetDataRow(Soldier_Id);
        
        //var SoliderPath = AssetUtility.GetModelAsset(NpcConfig.model);
    }
    
    protected Solider CreateSolider()
    {
        // var solider = (GameObject)Instantiate(ObjSolider);
        // solider.name = string.Format("Solider_{0}_{1}",ownerCamp.ToString(),index) ;
        // var soliderTans = solider.GetComponent<Transform>();
        // soliderTans.position = GetSoliderPosition();
        // soliderTans.localScale = Vector3.one;
        // soliderTans.rotation = Quaternion.identity;
        // var soliderCom = solider.GetComponent<Solider>();
        // soliderCom.OwnerTown = null;
        // return soliderCom;
        return null;
    }
}