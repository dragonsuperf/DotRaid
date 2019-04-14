using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Stats
{
    public string id;
    public string name;
    public int level;
    public int max_hp;
    public int dp;
    public int atk;
    public float atk_speed;

    public Stats(string id, string name, int level, int max_hp, int dp, int atk, float atk_speed)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.max_hp = max_hp;
        this.dp = dp;
        this.atk = atk;
        this.atk_speed = atk_speed;
    }

    public override string ToString()
    {
        return string.Format("id: {0}, name: {1}, level: {2}, hp: {3}, dp: {4}, atk: {5}, speed: {6}",
            this.id, this.name, this.level, this.max_hp, this.dp, this.atk, this.atk_speed);
    }
}

public class DataLoader : MonoBehaviour
{
    private Dictionary<string, Stats> testTables = new Dictionary<string, Stats>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        TextAsset ta = Resources.Load<TextAsset>("Tables/test_stats");
        var lines = ta.text.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            var words = lines[i].Split(',');
            Stats stat = new Stats(words[0], words[1], int.Parse(words[2]), int.Parse(words[3]), 
                int.Parse(words[4]), int.Parse(words[5]), float.Parse(words[6]));
            testTables.Add(words[1], stat);
        }

        foreach(var dict in testTables)
        {
            Debug.Log(dict.Value.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
