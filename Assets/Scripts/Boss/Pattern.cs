using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern
{
    bool isRunning = false;
    //Animator ani;
    float duration = 0.0f;
    float elapsedTime = 0.0f;
    float cooldown = 0.0f;
    float speed = 1.0f;
    string name;
    //Queue<string> patternQueue = new Queue<string>();
    
    public Pattern(string name, float duration, float cd)
    {
        //  this.ani = ani;
        this.name = name;
        this.duration = duration;
        cooldown = cd;
        isRunning = true;
    }

    public Pattern(string name, float duration, float cd, float speed)
    {
        //  this.ani = ani;
        this.name = name;
        this.duration = duration;
        this.speed = speed;
        cooldown = cd;
        isRunning = true;
    }

    public void Reset()
    {
        elapsedTime = 0.0f;
        isRunning = true;
    }

    public void Update(float deltaTime)
    {
        elapsedTime += deltaTime;

        if (elapsedTime > duration)
            isRunning = false;
    }

    public string Name { get => name; set => name = value; }
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
}
