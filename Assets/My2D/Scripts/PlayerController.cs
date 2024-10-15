using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;
        //�÷��̾� �ȱ� �ӵ�
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float airSpeed = 2f;

        public float CurrentMoveSpeed
        {
            get
            {
                if (CanMove)
                {
                    if (isMove == true && touchingDirections.IsWall == false)
                    {
                        if (touchingDirections.IsGround == true)
                        {
                            if (IsRun == true)
                            {
                                return runSpeed;
                            }
                            else
                            {
                                return walkSpeed;
                            }
                        }
                        else
                        {
                            return airSpeed;
                        }
                    }
                    else
                    {
                        return 0; // Idle State;
                    }
                }
                else
                {
                    return 0; // Idle State;
                }
            }
        }

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

        // ����
        [SerializeField] private float jumpForce = 10f;

        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                if (isFacingRight != value)
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
            touchingDirections = GetComponent<TouchingDirections>();
            //sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            //�÷��̾� �¿� �̵�
            rb2D.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.velocity.y);

            //�ִϸ��̼� ��
            animator.SetFloat(AnimationString.YVelocity, rb2D.velocity.y);
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
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
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
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.JumpTrigger);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }
        public void OnAttack01(InputAction.CallbackContext context)
        {
            // ���콺�� Ŭ���ϴ� ����
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
        }
    }
}