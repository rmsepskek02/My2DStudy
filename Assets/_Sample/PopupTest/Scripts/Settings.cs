using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 옵션창
    /// </summary>
    public class Settings : Popup
    {
        [SerializeField] private CustomButton back;                 //게임종료 창 오픈
        [SerializeField] private CustomButton privacy;              //개인정보 동의 창

        private void OnEnable()
        {
            //옵션창 버튼 클릭시 호출되는 함수 등록
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
            //게임종료창 오픈
            MenuManager.Instance.ShowPopup<ExitGame>();
            Debug.Log("게임종료 창 오픈");
        }
        void PrivacyPolicy()
        {
            StopInteraction();

            Close();
            //개인정보 동의창 오픈
            MenuManager.Instance.ShowPopup<GDPR>();
            Debug.Log("개인정보 동의창 오픈");
        }
    }
}
