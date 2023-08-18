using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Routine
{
    public int startTime;
    public string destination;
    public Vector2 target;
    public int facingDirection;
    public string animation;
    public string[] dialogue;
    public static List<Routine> FromJson(string json, string key)
    {
        var data = JsonConvert.DeserializeObject<Root>(json);
        var routineData = data.Routine[0][key];
        var routines = routineData.Split(new[] { "//" }, System.StringSplitOptions.None);
        var result = new List<Routine>();

        foreach (var routine in routines)
        {
            var parts = routine.Split(' ');
            result.Add(new Routine
            {
                startTime = int.Parse(parts[0]),
                destination = parts[1],
                target = new Vector2(int.Parse(parts[2]), int.Parse(parts[3])),
                facingDirection = int.Parse(parts[4]),
                animation = parts[5]
            });
        }

        return result;
    }

    private class Root
    {
        public Dictionary<string, string>[] Routine { get; set; }
    }
}
