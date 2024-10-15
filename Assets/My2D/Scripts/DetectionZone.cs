using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class DetectionZone : MonoBehaviour
    {
        // ������ �ݶ��̴� ����Ʈ
        public List<Collider2D> detectedColliders = new List<Collider2D>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �浹ü�� �����Ǹ� ����Ʈ�� �߰�
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �浹ü�� ������ ����Ʈ�� ����
            detectedColliders.Remove(collision);
        }
    }
}