using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// ���� ���� ���� â: ����, ���, �������� ������ ��ũ
    /// </summary>
    public class GDPR : Popup
    {
        // ���� ���� ����
        public void OnUserClickAccept()
        {
            //���� ���� ���� ����
            Close();
        }

        // ���� ���� ���� â ���
        public void OnUserClickCancel()
        {
            //���� ���� �ź� ����
            Close();
        }
        // ���� ���� ���� ���ͳ� ������ ����
        public void OnUserClickPrivacyPolicy()
        {
            Application.OpenURL("www.naver.com");
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
