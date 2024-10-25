using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //������ �浹ü ����
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        //������ �ݶ��̴� ����Ʈ 
        public List<Collider2D> detectedColliders = new List<Collider2D>();

        //�浹ü ����Ʈ�� �浹ü�� ���̻� ������ ȣ��Ǵ� �Լ�
        public UnityAction noColliderRamain;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�浹ü�� �����Ǹ� ����Ʈ�� �߰��Ѵ�
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //�浹ü�� ������ ����Ʈ���� �����Ѵ�
            detectedColliders.Remove(collision);

            //�浹ü�� �ϳ��� ���� ���� ������
            if(detectedColliders.Count <= 0)
            {
                noColliderRamain?.Invoke();
            }
        }
    }
}
