using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// ���� ���� â : ��(��������), �ƴϿ�
    /// </summary>
    public class ExitGame : Popup
    {
        [SerializeField] private CustomButton yes;

        private void OnEnable()
        {
            yes.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            Application.Quit();
            Debug.Log("���� ����");
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
