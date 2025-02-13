using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// �ɼ�â
    /// </summary>
    public class Settings : Popup
    {
        [SerializeField] private CustomButton back;                 //�������� â ����
        [SerializeField] private CustomButton privacy;              //�������� ���� â

        private void OnEnable()
        {
            //�ɼ�â ��ư Ŭ���� ȣ��Ǵ� �Լ� ���
            back.onClick.AddListener(BackToMain);
            privacy.onClick.AddListener(PrivacyPolicy);
        }
        private void OnDisable()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void BackToMain()
        {
            StopInteraction();

            Close();
            //��������â ����
            MenuManager.Instance.ShowPopup<ExitGame>();
            Debug.Log("�������� â ����");
        }
        void PrivacyPolicy()
        {
            StopInteraction();

            Close();
            //�������� ����â ����
            MenuManager.Instance.ShowPopup<GDPR>();
            Debug.Log("�������� ����â ����");
        }
    }
}
