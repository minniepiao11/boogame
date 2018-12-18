using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using System.IO;
using System;
/// <summary>
/// Save And Load
/// 本篇腳本參考：http://zxxcc0001.pixnet.net/blog/post/243195373-unity---各平台檔案路徑
/// 腳本流程：按下存檔 -> 序列化存檔資料 -> 檢查資料夾是否存在 -> 將資料寫入文件
/// </summary>
[RequireComponent(typeof(PlayerData))]
public class SaveAndLoad : MonoBehaviour {

    public string savingFileName = "GameData.txt";
    private string nameAndPath;

    /// <summary>
    /// 創建一個文件資料夾
    /// </summary>
    /// <param name="filePath">File name.</param>
    public void CreateDirectory(string filePath)
    {
         
        if (File.Exists(filePath))
            return;     
        Directory.CreateDirectory(filePath);
    }
    /// <summary>
    /// 序列化存檔資料
    /// </summary>
    /// <returns>The object.</returns>
    /// <param name="PlayerData">玩家保存的資料</param>
    private string SerializeObject(object PlayerData)
    {
        string serializePlayerData = "";
        serializePlayerData = JsonConvert.SerializeObject(PlayerData);
        return serializePlayerData;
    }

    /// <summary>
    /// Deserializes the object.
    /// </summary>
    /// <returns>The object.</returns>
    /// <param name="PlayerDataString">玩家保存資料</param>
    /// <param name="_PlayerDataType">保存資料的型別</param>
    private static object DeserializeObject(string _PlayerData,Type _PlayerDataType)
    {
        object playerData = null;
        playerData = JsonConvert.DeserializeObject(_PlayerData,_PlayerDataType);
        return playerData;
    }

    /// <summary>
    /// 儲存檔案
    /// 資料參考：http://zxxcc0001.pixnet.net/blog/post/243195373-unity---各平台檔案路徑
    /// </summary>
    /// <param name="content">Content.</param>
    public void SaveData(object content)
    {
        string content_string = SerializeObject(content);//序列化輸入資料
        string filePath = Application.dataPath + "/StreamingAssets" + "/Save";

        CreateDirectory(filePath);
        nameAndPath = filePath + "/" + savingFileName;

        StreamWriter _streamwriter = File.CreateText(nameAndPath);
        _streamwriter.Write(content_string);
        _streamwriter.Close();

    }
    public object LoadData(Type dataType)
    {
        string filePath = Application.dataPath + "/StreamingAssets" + "/Save";
        //CreateDirectory(filePath, savingFileName);
        nameAndPath = filePath + "/" + savingFileName;
        StreamReader _streamReader = File.OpenText(nameAndPath);
        string data = _streamReader.ReadToEnd();
        _streamReader.Close();
        return DeserializeObject(data, dataType);
    }
}
