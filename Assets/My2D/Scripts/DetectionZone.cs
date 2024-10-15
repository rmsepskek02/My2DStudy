using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class DetectionZone : MonoBehaviour
    {
        // 감지된 콜라이더 리스트
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
            // 충돌체가 감지되면 리스트에 추가
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // 충돌체가 나가면 리스트에 삭제
            detectedColliders.Remove(collision);
        }
    }
}