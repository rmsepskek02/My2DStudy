using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// �˾� UI�� �����ϴ� �θ� Ŭ���� : show,hide ���(�ִϸ��̼� ����), close(��ư) - ����� ����
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class Popup : MonoBehaviour
    {
        public bool fade = false;               //�˾� fade ���
        private Animator animator;
        public CustomButton closeButton;        //â �ݱ� ��ư
        private CanvasGroup canvasGroup;

        //��� ����
        public Action OnShowAction;                 //�˾�â show�Ҷ� ��ϵ� �Լ� ȣ��
        public Action<PopupResult> OnCloseAction;   //�˾�â close�Ҷ� ��ϵ� �Լ� ȣ��
        protected PopupResult result;

        public delegate void PopupEvents(Popup popup);
        public static event PopupEvents OnOpenPopup;
        public static event PopupEvents OnClosePopup;
        public static event PopupEvents OnBeforeClosePopup;

        private void Awake()
        {
            //����
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();

            //close ��ư Ŭ���� ȣ��Ǵ� �Լ� ���
            if(closeButton != null)
            {
                closeButton.onClick.AddListener(Close);
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void Close()
        {
            if(closeButton != null)
            {
                closeButton.interactable = false;
            }

            canvasGroup.interactable = false;

            OnBeforeClosePopup?.Invoke(this);
            if(animator != null)
            {
                animator.Play("popup_hide");
            }
        }

        public void Show<T>(Action onShow, Action<PopupResult> onClose)
        {
            if(onShow != null)
            {
                OnShowAction = onShow;
            }

            if(onClose != null)
            {
                OnCloseAction = onClose;
            }

            OnOpenPopup?.Invoke(this);
            PlayShowAnimation();
        }

        private void PlayShowAnimation()
        {
            if (animator != null)
            {
                animator.Play("popup_show");
            }
        }

        // Show �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void ShowAnimationSound()
        {
            //TODO :: ���� ȿ���� ���
        }
        // Show �ִϸ��̼� �������� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void AfterShowAnimation()
        {
            OnShowAction?.Invoke();
        }
        // Hide �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void HideAnimationSound()
        {
            //TODO :: ���� ȿ���� ���
        }
        // Hide �ִϸ��̼� �������� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void AfterHideAnimation()
        {
            OnClosePopup?.Invoke(this);
            OnCloseAction?.Invoke(result);
            Destroy(gameObject,0.5f);
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            //���̵�ȿ��
            canvasGroup.alpha = 1.0f;
        }
        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            //���̵�ȿ��
            canvasGroup.alpha = 0.0f;
        }

        protected void StopInteraction()
        {
            canvasGroup.interactable = false;
        }
    }
}
