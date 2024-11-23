using UnityEngine;

public abstract class QuestBase
{
    public string Name => name;
    protected string name;

    public string Description => description;
    protected string description;

    public float LimitTime => limitTime;
    protected float limitTime;

    public QuestBase(string name, string description, float limitTime)
    {
        this.name = name;
        this.description = description;
        this.limitTime = limitTime;
    }
    public abstract bool Contidion();
    public abstract void Reward();
}
