using UnityEngine;

namespace Quests
{
    public class SampleQuest : QuestBase
    {
        public SampleQuest() : base("SampleQuest", "This is SampleQuest", 60f) { }
        public override bool Contidion()
        {
            //퀘스트 활성화시 달성 조건 체크
            return true;
        }

        public override void Reward()
        {
            //보상 정의
        }
    }
}

