using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    /// <summary>
    /// Ŀ���� ��ư : ���� ��ư ��� �޾� ��� Ȯ��
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CustomButton : Button
    {
        //public AudioClip overrideClickSound;
        private bool isClicked;                                 //��ư�� ��������?
        private readonly float cooldownTime = 0.5f;             //��ٿ� �ð����� ��ư�� �������� ������ �� ����

        public new ButtonClickedEvent onClick;                  //��ư Ŭ���� ��ϵ� �Լ� ȣ��
        private new Animator animator;
        private static bool blockInput;                         //��� Ŀ���� ��ư ��� ����
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
            // TODO :: ������ ȿ���� �÷���
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

        //��� Ŀ���� ��ư ��� ����/����
        public static void SetBlockInput(bool block)
        {
            blockInput = block;
        }
    }
}
