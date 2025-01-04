using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CodeStage.AntiCheat.Storage;

namespace BizzyBeeGames
{
    //_______ all tree fruit statussaved in obscuredby the good job order_________

    // GM = game manager  l= langage s = scene to open name
    // Sc = my scene manager  o=open scene name
    // MI = MAin Inventory  il= inventory list
    public class SaveManager : SingletonComponent<SaveManager>
    {
        #region Member Variables

        private List<ISaveable> saveables;
        private JSONNode loadedSave;
        float nextActionTime = 0.0f;
        float period = 3f;

        string saveURL = "http://localhost:49705/api/UploadSave";


        //private static SaveManager mInstance = null;
        #endregion

        #region Properties

        /// <summary>
        /// Path to the save file on the device
        /// </summary>
        public string SaveFilePath { get { return Application.persistentDataPath + "/save.json"; } }

        /// <summary>
        /// List of registered saveables
        /// </summary>
        private List<ISaveable> Saveables
        {

            get
            {
                if (saveables == null)
                {
                    saveables = new List<ISaveable>();
                }
                return saveables;
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Debug.Log(SaveFilePath);
          //DontDestroyOnLoad(this.gameObject);
        }
        private void OnDestroy()
        {
            Save();
        }
        private void OnApplicationQuit()
        {
            Save();
        }
        //private void OnApplicationPause(bool pause)
        //{
        //    Save();
        //}
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteSaveAndResetGame();
            }
        }

        //public void SaveNow()
        //{
        //    if (VRManagment.Instance != null)
        //    {
        //        VRManagment.Instance.Save();
        //    }
        //    if (FruiteManager.Instance != null)
        //    {
        //        FruiteManager.Instance.Save();
        //    }
        //    Save();
        //}


        public void DeleteSaveAndResetGame()
        {

            Statics.SaveText = "";
            System.IO.File.Delete(SaveFilePath);
            // StartCoroutine(SaveOnServer(Statics.SaveText));

        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Registers a saveable to be saved
        /// </summary>
        public void Register(ISaveable saveable)
        {
            Saveables.Add(saveable);
            //{


        }

        /// <summary>
        /// Loads the save data for the given saveable
        /// </summary>
        public JSONNode LoadSave(ISaveable saveable)
        {
            return LoadSave(saveable.SaveId);
        }

        /// <summary>
        /// Loads the save data for the given save id
        /// </summary>
        public JSONNode LoadSave(string saveId)
        {

            //Check if the save file has been loaded and if not try and load it
            if (loadedSave == null && !LoadSave(out loadedSave))
            {
                return null;
            }

            // Check if the loaded save file has the given save id
            if (!loadedSave.AsObject.HasKey(saveId))
            {
                return null;
            }

            // Return the JSONNode for the save id
            return loadedSave[saveId];
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves all registered saveables to the save file
        /// </summary>
        private void Save()
        {

            Dictionary<string, object> saveJson;
            //if (DeSerialize99() == "" || DeSerialize99() == null)
            //{
            //    saveJson = new Dictionary<string, object>();
            //}
            //else
            //{
            //    saveJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(DeSerialize99().ToString());
            //}
            Debug.Log("Statics Save text is: " + Statics.SaveText);

                if (!Statics.SaveText.Contains("{"))
                {

                    saveJson = new Dictionary<string, object>();
                }
                else
                {
                    saveJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(Statics.SaveText);
                    Debug.Log("Save Content is: " + saveJson);
                    if (saveables != null)
                    {
                        for (int i = 0; i < saveables.Count; i++)
                        {
                            if (!saveables.Contains(saveables[i]))
                            {
                                saveJson.Add(saveables[i].SaveId, saveables[i].Save());
                            }
                            else
                            {
                                saveJson[saveables[i].SaveId] = saveables[i].Save();
                            }

                        }
                    }

                }
                 Debug.Log(Statics.SaveText);
                Statics.SaveText = JsonConvert.SerializeObject(saveJson);
                System.IO.File.WriteAllText(SaveFilePath, JsonConvert.SerializeObject(saveJson));
                Debug.Log(SaveFilePath);
 





            //Serialize99(JsonConvert.SerializeObject(saveJson));

            //Statics.SaveText = JsonConvert.SerializeObject(saveJson);

            //StartCoroutine(SaveOnServer(Statics.SaveText));


        }

        private void LoadFromText()
        {
            if (!System.IO.File.Exists(SaveFilePath) || !System.IO.File.ReadAllText(SaveFilePath).Contains("{"))
            {
                Statics.SaveText = "";
            }
            else
            {
                Statics.SaveText = System.IO.File.ReadAllText(SaveFilePath);
            }
            Debug.Log(Statics.SaveText);
        }
        /// <summary>
        /// Tries to load the save file
        /// </summary>
        private bool LoadSave(out JSONNode json)
        {
            LoadFromText();
            json = null;

            if (!Statics.SaveText.Contains("{"))
            {

                return false;
            }
            else
            {
                json = JSONNode.Parse(Statics.SaveText);
            }

            //if (!System.IO.File.Exists(SaveFilePath) && !loadedJsonFromServer.Contains("{"))
            //{
            //    return false;
            //}
            //if (System.IO.File.Exists(SaveFilePath))
            //{
            //    json = JSON.Parse(System.IO.File.ReadAllText(SaveFilePath));
            //    System.IO.File.Delete(SaveFilePath);
            //}
            //else
            //{
            //    //json = DeSerialize99();
            //    json = JSONNode.Parse(loadedJsonFromServer);

            //}

            loadedSave = json;
            return json != null;


        }

        #endregion



        public void Serialize99(string saveJson)
        {

            ObscuredPrefs.SetString("saveFile", saveJson);
            //Debug.Log(saveJson);
            //Debug.Log("Saved json lenght: " + saveJson.Length);


        }
        public void SerializeOldSaveFile(JSONNode saveJson)
        {

            string save = JsonConvert.SerializeObject(saveJson);

            ObscuredPrefs.SetString("saveFile", save);

        }
        public JSONNode DeSerialize99()
        {

            string save;

            //save = ObscuredPrefs.GetString("saveFile");
            save = Statics.SaveText;
            JSONNode json = JSONNode.Parse(save);

            return json;
        }

        public class DownloadSaveFile_Result
        {
            public int ID;
            public string Saved_Json;
        }


    }
}
