using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PickupHealth : MonoBehaviour
    {
        #region Variables
        //�� - ȸ����
        [SerializeField] private float restoreHealth = 20f;

        [SerializeField] private Vector3 rotateSpeed = new Vector3(0f, 180f, 0f);
        #endregion

        private void Update()
        {
            //ȸ��
            transform.eulerAngles += rotateSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�浹�� ������Ʈ damageable�� �˻��Ͽ� ���Ѵ�
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                bool isHeal = damageable.Heal(restoreHealth);

                if (isHeal)
                {
                    //������ ų
                    Destroy(gameObject);
                }
            }
        }

    }
}