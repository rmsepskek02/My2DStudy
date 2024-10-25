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
        private Damageable damageable;

        //�÷��̾� �̵� �ӵ�
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float airSpeed = 2f;

        public float CurrentMoveSpeed
        {
            get
            {
                if(CanMove)
                {
                    if (IsMove && touchingDirections.IsWall == false)
                    {
                        if (touchingDirections.IsGround)
                        {
                            if (isRun)
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
                        return 0f;  //idle state
                    }
                }
                else
                {
                    return 0f;  //�������� ���Ҷ�
                }
            }
        }

        //�̵�����
        public bool CanMove
        {
            get
            {
                return animator.GetBool(AnimationString.CanMove);
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

        //�¿� ����
        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                //����
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }

        //����
        [SerializeField] private float jumpForce = 5f;

        //���� üũ
        public bool IsDeath
        {
            get { return animator.GetBool(AnimationString.IsDeath); }
        }
        #endregion

        private void Awake()
        {
            //����
            rb2D = this.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();

            damageable = GetComponent<Damageable>();
            damageable.hitAction += OnHit;              //UnityAction ��������Ʈ �Լ��� ���
        }

        private void FixedUpdate()
        {
            if(!damageable.LockVelocity)
            {
                //�÷��̾� �¿� �̵�
                rb2D.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.velocity.y);
            }

            //�ִϸ��̼� ��
            animator.SetFloat(AnimationString.YVelocity, rb2D.velocity.y);
        }

        //�ٶ󺸴� ������ ��ȯ
        void SetFacingDirection(Vector2 moveInput)
        {
            if(moveInput.x > 0f && IsFacingRight == false)
            {
                //�������� �ٶ󺻴�
                IsFacingRight = true;
            }
            else if (moveInput.x < 0f && IsFacingRight == true)
            {
                //������ �ٶ󺻴�
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();

            if(IsDeath)
            {
                IsMove = false;
            }
            else //�������
            {
                IsMove = (inputMove != Vector2.zero);

                //������ȯ
                SetFacingDirection(inputMove);
            }
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            //������ �����ϴ� ����
            if(context.started)
            {
                IsRun = true;
            }
            else if(context.canceled)   //������ �ϴ� ����
            {
                IsRun = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //������ �����ϴ� ����, ���� ���� x
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.JumpTrigger);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            //���콺 Ŭ������ �����ϴ� ����
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
        }

        public void OnBowAttack(InputAction.CallbackContext context)
        {
            //FŰ �����¼��� �����ϴ� ����
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.BowTrigger);
            }
        }

        public void OnHit(float damage, Vector2 knockback)
        {
            rb2D.velocity = new Vector2(knockback.x, rb2D.velocity.y + knockback.y);
        }
    }
}