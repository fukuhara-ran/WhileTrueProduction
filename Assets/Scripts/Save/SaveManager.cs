using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour {

    private readonly string dbDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\WhileTrueProduction\\AshLegacy\\";
    private readonly string dbFile = "data.sqlite";
    private readonly string dbUri = "URI=file:" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\WhileTrueProduction\\AshLegacy\\" + "data.sqlite";
    
    private static SaveManager _INSTANCE;

    public PlayerDTO CurrentActivePlayer = null;

    public static SaveManager INSTANCE {
        get { 
            if(_INSTANCE == null) _INSTANCE = new GameObject("SaveManager").AddComponent<SaveManager>();
            return _INSTANCE;
        }
    }

    void Start() {
        InitializeDatabase();
    }

    private void InitializeDatabase() {
        Directory.CreateDirectory(dbDir);
        if(!File.Exists(dbDir+dbFile)) File.Create(dbDir+dbFile);
    }

    public void SaveProgress() {
        ExecuteSql("CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY, username TEXT UNIQUE, password TEXT, level INTEGER, positionx FLOAT, positiony FLOAT);");

        ExecuteSql(String.Format("INSERT OR REPLACE INTO players (id, username, password, level, positionx, positiony) VALUES (NULL, '{0}', {1}, {2}, {3}, {4});",
        CurrentActivePlayer.Username,
        CurrentActivePlayer.Password,
        CurrentActivePlayer.Level,
        CurrentActivePlayer.PositionX,
        CurrentActivePlayer.PositionY));
    }
    
    public void Read(String username, String password) {
        ExecuteSql("CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY, username TEXT UNIQUE, password TEXT, level INTEGER, positionx FLOAT, positiony FLOAT);");

        GetPlayer(username, password);
    }

    public void Create(String username, String password) {
        ExecuteSql("CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY, username TEXT UNIQUE, password TEXT, level INTEGER, positionx FLOAT, positiony FLOAT);");
        
        ExecuteSql(String.Format("INSERT OR REPLACE INTO players (id, username, password, level, positionx, positiony) VALUES (NULL, '{0}', '{1}', {2}, {3}, {4});", username, password, 1, 0f, 0f));
        GetPlayer(username, password);
    }

    private void ExecuteSql(String sqlCommand) {
        // PlayerDTO playerDTO = new() {
        //     Id = 0
        // };

        // DataTable dataTable = new();
        // SqliteConnection sqliteConnection = new(dbUri);

        // sqliteConnection.Open();
        // SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
        // sqliteCommand.CommandText = 
        //     @"CREATE TABLE IF NOT EXISTS players (id INTEGER PRIMARY KEY, username TEXT UNIQUE, password TEXT, level INTEGER, positionx FLOAT, positiony FLOAT);" + sql;
        // sqliteCommand.ExecuteReader();
        // sqliteConnection.Close();

        // SqliteDataAdapter sqliteDataAdapter = new(sqliteCommand);
        // sqliteDataAdapter.Fill(dataTable);


        // foreach(DataRow row in dataTable.Rows) {
            // playerDTO.Id = row.Field<int>("id");
            // playerDTO.Username = row["username"].ToString();
            // playerDTO.Password = row["password"].ToString();
            // playerDTO.Level = row.Field<int>("level");
            // playerDTO.PositionX = row.Field<float>("positionx");
            // playerDTO.PositionY = row.Field<float>("positiony");
        // }

        using var connection = new SqliteConnection(dbUri);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = sqlCommand;
        Debug.Log("Executing ----> "+sqlCommand);
        command.ExecuteNonQuery();
        
        connection.Close();
    }

    private void GetPlayer(String username, String password) {
        PlayerDTO playerDTO = new();

        using var connection = new SqliteConnection(dbUri);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = String.Format("SELECT * FROM players WHERE username = '{0}' AND password = '{1}'", username, password);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            playerDTO.Id = reader.GetInt32(0);
            playerDTO.Username = reader.GetString(1);
            playerDTO.Password = reader.GetString(2);
            playerDTO.Level = reader.GetInt32(3);
            playerDTO.PositionX = reader.GetFloat(4);
            playerDTO.PositionY = reader.GetFloat(5);
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