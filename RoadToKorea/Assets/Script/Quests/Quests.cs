using UnityEngine;

namespace Quests
{
    public class SampleQuest : QuestBase
    {
        public SampleQuest() : base("SampleQuest", "This is SampleQuest", 60f) { }
        public override bool Contidion()
        {
            //����Ʈ Ȱ��ȭ�� �޼� ���� üũ
            return true;
        }

        public override void Reward()
        {
            //���� ����
        }
    }
}

