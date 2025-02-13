using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 게임 종료 창 : 예(게임종료), 아니오
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
            Debug.Log("게임 종료");
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
