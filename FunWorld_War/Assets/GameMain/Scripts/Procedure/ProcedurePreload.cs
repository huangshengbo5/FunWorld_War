﻿using GameFramework;
using GameFramework.Event;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;


public class ProcedurePreload : ProcedureBase
{
    private Dictionary<string, bool> m_LoadedFlag = new();

    public override bool UseNativeDialog => true;

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
        GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
        GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
        GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
        GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
        m_LoadedFlag.Clear();
        PreloadResources();
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
        GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
        GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
        GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
        GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

        base.OnLeave(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        foreach (var loadedFlag in m_LoadedFlag)
            if (!loadedFlag.Value)
                return;

        procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Main"));
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

    private void PreloadResources()
    {
        // Preload configs
        LoadConfig("DefaultConfig");

        // // Preload data tables
        foreach (var dataTableName in DataTable_Config.DataTableNames)
            LoadDataTable(dataTableName);
        
        // // Preload dictionaries
        // LoadDictionary("Default");
        //
        // // Preload fonts
        // LoadFont("MainFont");
    }

    private void LoadConfig(string configName)
    {
        var configAssetName = AssetUtility.GetConfigAsset(configName, false);
        m_LoadedFlag.Add(configAssetName, false);
        GameEntry.Config.ReadData(configAssetName, this);
    }

    private void LoadDataTable(string dataTableName)
    {
        var dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
        m_LoadedFlag.Add(dataTableAssetName, false);
        GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
    }

    private void LoadDictionary(string dictionaryName)
    {
        var dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
        m_LoadedFlag.Add(dictionaryAssetName, false);
        GameEntry.Localization.ReadData(dictionaryAssetName, this);
    }

    private void LoadFont(string fontName)
    {
        m_LoadedFlag.Add(Utility.Text.Format("Font.{0}", fontName), false);
        GameEntry.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset,
            new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Font.{0}", fontName)] = true;
                    //UGuiForm.SetMainFont((Font)asset);
                    Log.Info("Load font '{0}' OK.", fontName);
                },
                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName,
                        errorMessage);
                }));
    }

    private void OnLoadConfigSuccess(object sender, GameEventArgs e)
    {
        var ne = (LoadConfigSuccessEventArgs)e;
        if (ne.UserData != this) return;

        m_LoadedFlag[ne.ConfigAssetName] = true;
        Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
    }

    private void OnLoadConfigFailure(object sender, GameEventArgs e)
    {
        var ne = (LoadConfigFailureEventArgs)e;
        if (ne.UserData != this) return;

        Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName,
            ne.ConfigAssetName, ne.ErrorMessage);
    }

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        var ne = (LoadDataTableSuccessEventArgs)e;
        if (ne.UserData != this) return;

        m_LoadedFlag[ne.DataTableAssetName] = true;
        Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
    }

    private void OnLoadDataTableFailure(object sender, GameEventArgs e)
    {
        var ne = (LoadDataTableFailureEventArgs)e;
        if (ne.UserData != this) return;

        Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName,
            ne.DataTableAssetName, ne.ErrorMessage);
    }

    private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
    {
        var ne = (LoadDictionarySuccessEventArgs)e;
        if (ne.UserData != this) return;

        m_LoadedFlag[ne.DictionaryAssetName] = true;
        Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
    }

    private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
    {
        var ne = (LoadDictionaryFailureEventArgs)e;
        if (ne.UserData != this) return;

        Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName,
            ne.DictionaryAssetName, ne.ErrorMessage);
    }
}