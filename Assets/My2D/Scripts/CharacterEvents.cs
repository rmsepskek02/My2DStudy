using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //ĳ���Ϳ� ���õ� �̺�Ʈ �Լ����� �����ϴ� Ŭ����
    public class CharacterEvents
    {
        //ĳ���Ͱ� �������� ������ ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> characterDamaged;
        //ĳ���Ͱ� ���Ҷ� ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> characterHealed;

        //...
    }
}
