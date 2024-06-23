using System;
using UnityEngine;

public class PlayerDTO {
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Level { get; set; }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public int Gold { get; set; }

    public Vector3 Position {
        get {
            return new Vector3(PositionX, PositionY, 0f);
        }
    }
}