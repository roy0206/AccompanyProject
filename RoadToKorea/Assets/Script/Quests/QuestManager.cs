using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class QuestManager : Singleton<QuestManager>
{
    public List<QuestBase> ActiveQuests => activeQuests;
    [SerializeField] private List<QuestBase> activeQuests;


    private void Update()
    {
        CheckCondition();
    }

    public void ActiveQuest(QuestBase quest)
    {
        activeQuests.Add(quest);
    }

    void CheckCondition()
    {
        QuestBase quest;
        for (int i = 0; i < activeQuests.Count; i++)
        {
            quest = activeQuests[i];
            if (quest.Contidion())
            {
                quest.Reward();
                activeQuests.RemoveAt(i);
                i--;
            }
        }
    }
}
