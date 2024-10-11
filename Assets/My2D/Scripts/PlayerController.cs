using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        //�÷��̾� �ȱ� �ӵ�
        [SerializeField] private float walkSpeed = 4f;

        //�÷��̾� �̵��� ���õ� �Է°�
        private Vector2 inputMove;

        //�ȱ�
        [SerializeField] private bool isMove = false;
        public bool IsMove
        {
            get
            {
                return isMove;
            }
            set
            {
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            }
        }

        //�ٱ�
        [SerializeField] private bool isRun = false;
        public bool IsRun
        {
            get
            {
                return isRun;
            }
            set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }
        //SpriteRenderer sr;
        #endregion

        private void Awake()
        {
            //����
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            //�÷��̾� �¿� �̵�
            rb2D.velocity = new Vector2(inputMove.x * walkSpeed, rb2D.velocity.y);
        }

        // �ٶ󺸴� �������� ��ȯ
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x > 0f && IsFacingRight == false)
            {
                //sr.flipX = false;
                IsFacingRight = true;
            }
            else if (moveInput.x < 0f && IsFacingRight == true)
            {
                //sr.flipX = true;
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            IsMove = inputMove != Vector2.zero;
            SetFacingDirection(inputMove);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            // ������ ����
            if (context.started)
            {
                IsRun = true;
            }
            // ���� ����
            else if (context.canceled)
            {
                IsRun = false;
            }
        }
    }
}