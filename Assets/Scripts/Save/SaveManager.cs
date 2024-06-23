using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SaveManager {

    private readonly string dbDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\WhileTrueProduction\\AshLegacy\\";
    private readonly string dbFile = "data.sqlite";
    private readonly string dbUri = "URI=file:" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\WhileTrueProduction\\AshLegacy\\" + "data.sqlite";
    
    private static SaveManager _INSTANCE;

    public PlayerDTO CurrentActivePlayer = null;

    public static SaveManager GetInstance() {
        if(_INSTANCE == null) _INSTANCE = new();
        return _INSTANCE;
    }

    private SaveManager() {
        InitializeDatabase();
    }

    private void InitializeDatabase() {
        // Debug.Log("Initializing database");

        if(!File.Exists(dbDir+dbFile)) {
            Directory.CreateDirectory(dbDir);
            File.Create(dbDir+dbFile);
            ExecuteSql("CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY, username TEXT UNIQUE, password TEXT, level TEXT, position_x FLOAT, position_y FLOAT, gold INTEGER);");
        }
    }

    public PlayerDTO GetCurrentPlayer() {
        return CurrentActivePlayer;
    }

    public void SaveProgress(string sceneName, Vector3 position, int gold) {
        if(CurrentActivePlayer == null) {
            Debug.Log("Current player null");
            return;
        };
        CurrentActivePlayer.Level = sceneName;
        CurrentActivePlayer.PositionX = position.x;
        CurrentActivePlayer.PositionY = position.y;
        CurrentActivePlayer.Gold = gold;


        ExecuteSql(String.Format("INSERT OR REPLACE INTO players (id, username, password, level, position_x, position_y, gold) VALUES (NULL, '{0}', '{1}', '{2}', {3}, {4}, {5});",
        CurrentActivePlayer.Username,
        CurrentActivePlayer.Password,
        CurrentActivePlayer.Level,
        CurrentActivePlayer.PositionX,
        CurrentActivePlayer.PositionY,
        CurrentActivePlayer.Gold));
    }

    public void GoToLatestProgress() {
        SceneManager.LoadScene(CurrentActivePlayer.Level);
    }
    
    public void Read(string username, string password) {
        GetPlayer(username, password);
    }

    public void Create(string username, string password) {
        ExecuteSql(string.Format("INSERT OR REPLACE INTO players (id, username, password, level, position_x, position_y, gold) VALUES (NULL, '{0}', '{1}', '{2}', {3}, {4}, {5});", username, password, "Level 1", 0f, 0f, 0));
        GetPlayer(username, password);
    }

    private void ExecuteSql(string sqlCommand) {
        using var connection = new SqliteConnection(dbUri);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = sqlCommand;
        Debug.Log("Executing ----> "+sqlCommand);
        command.ExecuteNonQuery();
        
        connection.Close();
    }

    private void GetPlayer(string username, string password) {
        PlayerDTO playerDTO = new();

        using var connection = new SqliteConnection(dbUri);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = string.Format("SELECT * FROM players WHERE username = '{0}' AND password = '{1}'", username, password);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            playerDTO.Id = reader.GetInt32(0);
            playerDTO.Username = reader.GetString(1);
            playerDTO.Password = reader.GetString(2);
            playerDTO.Level = reader.GetString(3);
            playerDTO.PositionX = reader.GetFloat(4);
            playerDTO.PositionY = reader.GetFloat(5);
            playerDTO.Gold = reader.GetInt32(6);
        }
        reader.Close();
        connection.Close();

        if(playerDTO.Id == 0) {
            CurrentActivePlayer = null;
        } else {
            CurrentActivePlayer = playerDTO;
        }
    }

}