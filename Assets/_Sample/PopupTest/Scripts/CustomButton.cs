using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    /// <summary>
    /// 커스텀 버튼 : 기존 버튼 상속 받아 기능 확장
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CustomButton : Button
    {
        //public AudioClip overrideClickSound;
        private bool isClicked;                                 //버튼이 눌러졌냐?
        private readonly float cooldownTime = 0.5f;             //쿨다운 시간동안 버튼이 연속으로 눌리는 것 방지

        public new ButtonClickedEvent onClick;                  //버튼 클릭시 등록된 함수 호출
        private new Animator animator;
        private static bool blockInput;                         //모든 커스텀 버튼 기능 정지
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked || blockInput)
            {
                return;
            }
            // TODO :: 누르는 효과음 플레이
            Press();

            isClicked = true;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Cooldown());
            }

            base.OnPointerClick(eventData);
        }
        private void Press()
        {
            onClick?.Invoke();
        }

        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldownTime);
            isClicked = false;
        }

        private bool IsAnimationPlay()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.loop || stateInfo.normalizedTime < 1;
        }

        //모든 커스텀 버튼 기능 정지/해제
        public static void SetBlockInput(bool block)
        {
            blockInput = block;
        }
    }
}
