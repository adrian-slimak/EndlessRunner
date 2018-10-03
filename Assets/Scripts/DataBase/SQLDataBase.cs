using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.IO;

public class SQLDataBase : MonoBehaviour
{
    public static SQLDataBase Instance;

    string conn;
    IDbConnection dbconn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            OpenDB("LocalDataBase.s3db");
        }
        else Destroy(this);
    }

    public void OpenDB(string p) //p is the database name

    {
        // check if file exists in Application.persistentDataPath

        string filepath = Application.persistentDataPath + "/" + p;

        if (!File.Exists(filepath))

        {

            // if it doesn't ->

            // open StreamingAssets directory and load the db -> 

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);  // this is the path to your StreamingAssets in android

            while (!loadDB.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            // then save to Application.persistentDataPath

            File.WriteAllBytes(filepath, loadDB.bytes);

        }

        //open db connection

        conn = "URI=file:" + filepath;

        dbconn = new SqliteConnection(conn);

        dbconn.Open();

    }

    internal static void SaveLevel(LevelInfo levelInfo)
    {
        IDbCommand dbcmd = Instance.dbconn.CreateCommand();

        string sqlQuery = "UPDATE Levels " +
                          " SET totalAttempts = " + levelInfo.totalAttempts + 
                          ", totalJumps = " + levelInfo.totalJumps + 
                          ", levelFinished = " + (levelInfo.levelFinished?1:0) + 
                          ", levelFinishedPractice = " + (levelInfo.levelFinishedPractice?1:0) +
                          ", normalModeProgress = " + levelInfo.normalModeProgress +
                          ", practiceModeProgress = " + levelInfo.practiceModeProgress +
                          ", pointsCollected = " + levelInfo.pointsCollected +
                          ", hiddenCoinsCollected = \"" + levelInfo.hiddenCoinsCollected.x + "-" + levelInfo.hiddenCoinsCollected.y + "-" + levelInfo.hiddenCoinsCollected.z + "\"" + 
                          " WHERE LevelID = \"" + levelInfo.LevelID + "\"";

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    internal static void SaveAchievement(int acheievID)
    {
        IDbCommand dbcmd = Instance.dbconn.CreateCommand();

        string sqlQuery = "UPDATE Achievements " +
                          " SET collected = 1" +
                          ", date = " + "\"" + DateTime.Now.ToString("dd-MM-yyyy") + "\""+
                          " WHERE AchievementID = \"" + acheievID + "\"";

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    internal static void SaveSkinInfo(SkinInfo skin)
    {
        IDbCommand dbcmd = Instance.dbconn.CreateCommand();

        string sqlQuery = "UPDATE Skins " +
                          " SET skinSelected = " + skin.skinSelected +
                          ", firstColor = " + "\"" + ColorToHex(skin.firstColor) + "\"" +
                          ", firstColorIndex = " + skin.firstColorIndex +
                          ", secondColor = " + "\"" + ColorToHex(skin.secondColor) + "\"" +
                          ", secondColorIndex = " + skin.secondColorIndex +
                          " WHERE MechanicID = \"" + skin.MechanicID + "\"";

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    internal static Dictionary<int, LevelInfo> LoadLevels()
    {
        Dictionary<int, LevelInfo> levelInfoList = new Dictionary<int, LevelInfo>();

        IDbCommand dbcmd = Instance.dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM Levels";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int levelID = reader.GetInt32(0);
            string levelName = reader.GetString(1);
            int maxStars = reader.GetInt32(2);
            int maxPoints = reader.GetInt32(3);

            LevelInfo level = new LevelInfo(levelID, levelName, maxStars, maxPoints);

            level.totalAttempts = reader.GetInt32(4);
            level.totalJumps = reader.GetInt32(5);
            level.levelFinished = reader.GetInt32(6) == 0? false:true;
            level.levelFinishedPractice = reader.GetInt32(7) == 0 ? false : true;
            level.normalModeProgress = reader.GetInt32(8);
            level.practiceModeProgress = reader.GetInt32(9);
            level.pointsCollected = reader.GetInt32(10);
            string[] hiddenCoinsCollected = reader.GetString(11).Split('-');

            level.hiddenCoinsCollected.x = int.Parse(hiddenCoinsCollected[0]);
            level.hiddenCoinsCollected.y = int.Parse(hiddenCoinsCollected[1]);
            level.hiddenCoinsCollected.z = int.Parse(hiddenCoinsCollected[2]);
            levelInfoList.Add(levelID, level);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return levelInfoList;
    }

    internal static List<Achievement> LoadAchievements()
    {
        List<Achievement> achievements = new List<Achievement>();

        IDbCommand dbcmd = Instance.dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM Achievements";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int achievementID = reader.GetInt32(0);
            string name = reader.GetString(1);

            Achievement achievement = new Achievement(achievementID, name);

            achievement.description = reader.GetString(2);
            achievement.collected = reader.GetInt32(3)==0?false:true;
            achievement.category = reader.GetInt32(4);
            achievement.date = reader.GetString(5);
            achievement.image = reader.GetString(6);

            achievements.Add(achievement);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return achievements;
    }

    internal static List<SkinInfo> LoadSkins()
    {
        List<SkinInfo> skins = new List<SkinInfo>();

        IDbCommand dbcmd = Instance.dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM Skins";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            SkinInfo skin = new SkinInfo(reader.GetInt32(0));
            skin.skinSelected = reader.GetInt32(1);
            skin.firstColor = HexToColor(reader.GetString(2));
            skin.firstColorIndex = reader.GetInt32(3);
            skin.secondColor = HexToColor(reader.GetString(4));
            skin.secondColorIndex = reader.GetInt32(5);
            skin.spriteName = reader.GetString(6);

            skins.Add(skin);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        return skins;

    }

    static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    public static void CloseDataBase()
    {
        Instance.dbconn.Close();
        Instance.dbconn = null;
    }

    private void OnApplicationQuit()
    {
        CloseDataBase();
    }
}
