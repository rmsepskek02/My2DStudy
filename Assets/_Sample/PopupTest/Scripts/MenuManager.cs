using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MySample
{
    /// <summary>
    /// �޴� �˾��� �����ϴ� �̱��� Ŭ����
    /// </summary>
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        public List<Popup> popupStack = new List<Popup>();
        [SerializeField] private Canvas canvas;                 //�˾� �޴��� �θ� ĵ���� ������Ʈ

        private void OnEnable()
        {
            Popup.OnClosePopup += ClosePopup;
            Popup.OnBeforeClosePopup += OnBeforeCloseAction;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void ClosePopup(Popup popupClose)
        {
            if (popupStack.Count > 0)
            {
                popupStack.Remove(popupClose);
                if (popupStack.Count > 0)
                {
                    var popup = popupStack.Last();
                    popup.Show();
                }
            }
        }

        void OnBeforeCloseAction(Popup popupClose)
        {
            //TODO :: ���̵� ȿ��
        }

        public T ShowPopup<T>(Action onShow = null, Action<PopupResult> onClose = null) where T : Popup
        {
            // �̹� â�� ���ȴ��� üũ
            if (popupStack.OfType<T>().Any())
            {
                return popupStack.OfType<T>().First();
            }

            return (T)ShowPopup("Popups/" + typeof(T).Name, onShow, onClose);
        }

        public Popup ShowPopup(string pathWithType, Action onShow = null, Action<PopupResult> onClose = null)
        {
            // �̹� â�� �����ִ��� üũ
            if (popupStack.Any(p => p.GetType().Name == pathWithType.Split("/").Last()))
            {
                return popupStack.First(p => p.GetType().Name == pathWithType.Split("/").Last());
            }

            // ������
            var popupPrefab = Resources.Load<Popup>(pathWithType);
            if (popupPrefab == null)
            {
                Debug.Log("ã�� �˾� �������� �����ϴ�.");
                return null;
            }

            return ShowPopup(popupPrefab, onShow, onClose);
        }

        public Popup ShowPopup(Popup popupPrefab, Action onShow = null, Action<PopupResult> onClose = null)
        {
            var popup = Instantiate(popupPrefab, canvas.transform);
            if (popupStack.Count > 0)
            {
                popupStack.Last().Hide();
            }

            popupStack.Add(popup);
            popup.Show<Popup>(onShow, onClose);
            var rectTransform = popup.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            return popup;
        }

        // Ư�� Ÿ��(T)�� �˾�ã��
        public T GetPopupOpened<T>() where T : Popup
        {
            foreach (var popup in popupStack)
            {
                if (popup.GetType() == typeof(T))
                {
                    return (T)popup;
                }
            }

            return null;
        }

        //���� �ִ� ��� â �ݱ�
        public void CloseAllPopups()
        {
            for (int i = 0; i < popupStack.Count; i++)
            {
                var popup = popupStack[i];
                popup.Close();
            }
            popupStack.Clear();
        }
        //â�� �ϳ��� �����ֳ�?
        public bool IsAnyPopupOpen()
        {
            return popupStack.Count > 0;
        }

        public Popup GetLastPopup()
        {
            return popupStack.Last();
        }
    }
}
