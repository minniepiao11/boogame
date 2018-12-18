using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
namespace WeatherStation
{
    /// <summary>
    /// Custom JSON Object
    /// 氣象站資料
    /// </summary>
    public class JObject
    {
        public class channel
        {
            public string name = "ESP8266";
            public float latitude = 0.0f;
            public float longitude = 0.0f;
            public int id = 147306;
            public string description = "temp_changed";
            public string field1 = "Wind Direction";
            public string field2 = "Average Wind Speed (One Minute)";
            public string field3 = "Max Wind Speed (Five Minutes)";
            public string field4 = "Rain Fall (One Hour)";
            public string field5 = "Rain Fall (24 Hour)";
            public string field6 = "Temperature";
            public string field7 = "Humidity";
            public string field8 = "Barometric Pressure";
            public string created_at = "2016-08-20T23:08:41Z";
            public string updated_at = "2018-10-27T12:27:26Z";
            public int last_entry_id = 33939;
        }
        public class feeds
        {
            public string created_at = null;
            public int entry_id = 33840;

            /// <summary>
            /// Wind Direction 0~360
            /// </summary>
            public int field1;

            /// <summary>
            /// Average Wind Speed (One Minute)
            /// </summary>
            public float field2;

            /// <summary>
            /// Max Wind Speed (Five Minutes)
            /// </summary>
            public float field3;

            /// <summary>
            /// Rain Fall (One Hour)
            /// </summary>
            public float field4;

            /// <summary>
            /// Rain Fall (24 Hour)
            /// </summary>
            public float field5;

            /// <summary>
            /// Temperature
            /// </summary>
            public float field6;

            /// <summary>
            /// Humidity
            /// </summary>
            public int field7;

            /// <summary>
            /// Barometric Pressure
            /// </summary>
            public float field8;

        }
        public channel Channel = new channel();
        public List<feeds> Feeds = new List<feeds>();
    }
    /// <summary>
    /// Save to local file.
    /// </summary>
    public class SaveToLocalFile
    {
        static string path;
        public static string FilePath
        {
            get
            {
                if (path == "")
                    return Application.dataPath + "/StreamingAssets" + "/Save " + "/SavingData.txt";
                else{ return path;}  
            }
            set
            {
                path = value;
            }
        }
        /// <summary>
        /// 檢查所有存檔資料夾路徑是否存在，若不存在則新增
        /// 存檔路徑為Application.dataPath + "/StreamingAssets + "/Save" +  "/" + fileName
        /// </summary>
        private string CheckDirectory(string fileName)
        {
            string _FilePath = Application.dataPath + "/StreamingAssets";

            CreateDirectory(FilePath);

            _FilePath = _FilePath + "/Save";
            CreateDirectory(_FilePath);

            _FilePath = _FilePath + "/" + fileName;
            CreateTextFile(_FilePath);

            FilePath = _FilePath;

            return _FilePath;
        }
        /// <summary>
        /// 檢查該存路徑檔是否存在，若不存在則新增一個資料夾
        /// </summary>
        /// <param name="FilePath">File path.</param>
        private void CreateDirectory(string FilePath)
        {
            if (File.Exists(FilePath))
                return;
            Directory.CreateDirectory(FilePath);
        }
        /// <summary>
        /// 檢查存檔是否存在，若不存在則新增一個
        /// </summary>
        /// <param name="textFilePath">Text file path.</param>
        private void CreateTextFile(string textFilePath)
        {
            if (File.Exists(textFilePath))
                return;
            File.CreateText(textFilePath);
        }
        /// <summary>
        /// 序列化想要存檔的物件(把物件轉為JSON格式的字串)
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="JSONObject">JSONO bject.</param>
        public static string SerializeObject(object JSONObject)
        {
            return JsonConvert.SerializeObject(JSONObject);
        }
        public void SaveData(string FileName, object _JSONObject)
        {
            string serializeJSONObject = SerializeObject(_JSONObject);

            StreamWriter _streamWriter =
                File.CreateText(
                CheckDirectory(FileName));

            _streamWriter.Write(serializeJSONObject);
            _streamWriter.Close();
        }
    }
    /// <summary>
    /// Load data.need to convert type to JObject
    /// </summary>
    public static class LoadData
    {
        public static JObject GameData = new JObject();
        public static JObject ReadFileFromLocalFile()
        {
            string filepath = SaveToLocalFile.FilePath;
            StreamReader streamReader = File.OpenText(filepath);
            string data = streamReader.ReadToEnd();
            return loadDataToGameScene(data);
        }
        /// <summary>
        /// Reads the Data from internet.but need to use "StartCoroutine"
        /// every 30 second read data once
        /// </summary>
        /// <returns>The file from internet.</returns>
        /// <param name="GameData">the data you want to replace</param>
        public static IEnumerator ReadFileFromInternet()
        {
            //https://api.thingspeak.com/channels/147306/feeds.json?results=2
            string path = "https://api.thingspeak.com/channels/147306/feeds.json?results=2";
            WWW www = new WWW(path);
            float time = 0;
            yield return www;

            if (www.error == null)
                GameData = loadDataToGameScene(www.text);
            else
                Debug.Log("WWW.Error +" + www.error);
            
            while (true)
            {
                if (time < 15)
                {
                    time += 1;
                    yield return new WaitForSeconds(1f);
                }  
                else
                {
                    time = 0;
                    www = new WWW(path);
                    yield return www;

                    if (www.error == null)
                        GameData = loadDataToGameScene(www.text);
                    else
                        Debug.Log("WWW.Error +" + www.error);
                    
                }

            }

        }

        public static object DeserializedObjet(String _SerializeObject ,Type DataType)
        {
            object DeserializeData = null;
            DeserializeData = JsonConvert.DeserializeObject(_SerializeObject, DataType);
            return DeserializeData;
        }

        public static JObject loadDataToGameScene(string Data)
        {
            JObject JSONData = (JObject)DeserializedObjet(Data, typeof(JObject));
            GameData = JSONData;
            //如果物件不為空則
            if(JSONData.Feeds.Count == 0)
            {
                Debug.Log("Do Nothing");
            }else{
                Debug.Log("feeds"+"["+ (JSONData.Feeds.Count - 1) + "]");
                Debug.Log("created_at : " + JSONData.Feeds[JSONData.Feeds.Count - 1].created_at);
                Debug.Log("entry_id : " + JSONData.Feeds[JSONData.Feeds.Count - 1].entry_id);  
                Debug.Log("field1 : " + JSONData.Feeds[JSONData.Feeds.Count - 1].field1);
                Debug.Log("field2 : " + JSONData.Feeds[JSONData.Feeds.Count - 1].field2);
            }

            //show JSON type data in string
            //Debug.Log(SaveToLocalFile.SerializeObject(JSONData));
            return JSONData;
             

        }
    }
}