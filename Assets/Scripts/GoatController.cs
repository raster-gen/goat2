using UnityEngine;

public class GoatController : MonoBehaviour
{
    public Joystick joystick;

    public float speedMove;
    public float powerJump;

    
    private Rigidbody2D rb;
    private bool faceRight = true;
    private float moveInput;

    private Animator animator;

    private bool isGrounded = false;
    public float checkRadius;


    // свойство связывает перечисление и аниматор
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
        if (isGrounded && joystick.Horizontal == 0) 
        {
            State = CharState.Idle;
        }
        
    }

    private void FixedUpdate()
    {

        if (isGrounded && joystick.Horizontal != 0)
        {
            Move();
        }
        

        if (isGrounded && joystick.Vertical > 0.5)
        {
            Jump();

        }
  
    }

    /// <summary>
    /// Проверка находится ли персонаж на земле
    /// </summary>
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        isGrounded = colliders.Length > 1;
        if(!isGrounded) State = CharState.Jump;
    }

    /// <summary>
    /// Функция движения влево/вправо
    /// </summary>
    private void Move()
    {
        moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput * speedMove, rb.velocity.y);
        State = CharState.Move;
        
        if (faceRight == true && moveInput > 0)
        {
            Flip();
        }
        else if (faceRight == false && moveInput < 0)
        {
            Flip();
        }
        
    }

    /// <summary>
    /// Функция прыжка
    /// </summary>
    private void Jump()
    {
        rb.AddForce(transform.up * powerJump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Функция разворота на 180 градусов
    /// </summary>
    private void Flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}

public enum CharState
{
    Idle,   //0
    Move,   //1
    Jump    //2
}