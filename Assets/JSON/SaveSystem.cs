using UnityEngine;
using System.IO;

public class SaveSystem
{
    private static string path = Application.persistentDataPath + "/player_save.json";

    public static void Save(PlayerData player)
    {
        PlayerData data = new PlayerData();

        data.health = player.health;
        data.maxHealth = player.maxHealth;
        data.speed = player.speed;
        data.jumpForce = player.jumpForce;

        Vector3 pos = player.transform.position;
        data.x = pos.x;
        data.y = pos.y;
        data.z = pos.z;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Saved to: " + path);
    }

    public static void Load(PlayerData player)
    {
        if (!File.Exists(path))
        {
            Debug.Log("No save file found!");
            return;
        }

        string json = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        player.health = data.health;
        player.maxHealth = data.maxHealth;
        player.speed = data.speed;
        player.jumpForce = data.jumpForce;

        player.transform.position = new Vector3(data.x, data.y, data.z);

        Debug.Log("Loaded!");
    }
}

