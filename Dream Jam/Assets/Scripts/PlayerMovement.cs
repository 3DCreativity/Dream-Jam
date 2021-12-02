using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    Material glitch;
    bool jump = false;
    float horizontalMove = 0f;
    Rigidbody2D rb;

    public int HP = 100;
    public int HPLeft;
    [SerializeField]
    private bool healing;
    bool healed = true;
    public UnityEvent onDeath;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask enemyLayers;
    public float attackRange1 = 0.5f;
    public float attackRange2 = 0.5f;
    public int attackDamage1 = 40;
    public int attackDamage2 = 40;
    public float pullForceIntensity = 2f;
    bool Attack1 = false;
    bool Attack2 = false;
    bool isDead = false;
    [SerializeField]
    Image HPSlider;
    [SerializeField]
    Image Focus;
    public int focus = 100;
    [SerializeField]
    int focusTop;
    public int focusLeft;
    //int focusDefault = 100;
    //int HPDefault = 100;
    bool acounted1 = false;
    bool acounted2 = false;

    public float attackRate = 2f;
    float nextTime = 0f;

    public LevelManager levelMan;
    [SerializeField]
    private CameraZoom camEffects;

    private PlayerInputActions player;
    Color color;

    public bool focused = false;
    public bool movelock = false;
    public bool jumplock = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().material = new Material(GetComponent<SpriteRenderer>().material);
        glitch = GetComponent<SpriteRenderer>().material;
        glitch.SetFloat("_Instance", 0f);
        HPLeft = HP;
        focusLeft = focus;
        focusTop = focus;
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
        
        
        player = new PlayerInputActions();
        player.Player.Enable();
        player.Player.Jump.performed += Jump;
        player.Player.Attack1.performed += A1;
        player.Player.Attack2.performed += A2;
        player.Player.EnableAttack1.performed += EnblAttack1;
        player.Player.EnableAttack2.performed += EnblAttack2;
        player.Player.Heal.performed += Heal;
        player.Player.Heal.canceled += ctx => healing = false;
        player.Player.Movement.performed += ctx =>
        {
            if (movelock == false)
            {
                horizontalMove = ctx.ReadValue<Vector2>().x * runSpeed;
            }
        };
        player.Player.Movement.canceled += ctx => horizontalMove = 0f;
        player.Player.Focus.performed += Focusing;
        player.Player.Interact.performed += Interact;
    }
    private void OnEnable()
    {
        //GameObject.FindObjectOfType<SettingsManager>().LoadUserRebinds(Input.asset);
        player.Player.Enable();
    }
    private void OnDisable()
    {
        player.Player.Disable();
    }
    //private void OnTriggerEnter2D(Collider2D TriggerGlitch)
    //{
    //    //if (TriggerGlitch.collider.name == "Player")
    //        FindObjectOfType<LevelManager>().GetComponent<LevelManager>().ActivateGlitch();
    //}
    //Control Actrions
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumplock == false)
            jump = true;
    }
    private void Focusing(InputAction.CallbackContext context)
    {
        if (context.performed && focused == false)
            GameObject.FindObjectOfType<Tuthor>().Focus_Discover();
            focused = true;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && GameObject.FindObjectOfType<InteractionDisplay>().able)
            GameObject.FindObjectOfType<InteractionDisplay>().Interact();
    }
    private void EnblAttack1(InputAction.CallbackContext context)
    {
        if (context.performed && focused)
            StartCoroutine(EnableAttack1());
    }

    private void EnblAttack2(InputAction.CallbackContext context)
    {
        if (context.performed && focused)
            StartCoroutine(EnableAttack2());
    }

    private void A1(InputAction.CallbackContext context)
    {
        if (context.performed && Attack1 == true && Time.time >= nextTime && focused)
        {
            StopCoroutine(ActivateAttack1());
            StartCoroutine(ActivateAttack1());
            nextTime = Time.time + 1f / attackRate;
        }
    }

    private void A2(InputAction.CallbackContext context)
    {
        if (context.performed && Attack2 == true && Time.time >= nextTime && focused)
        {
            StopCoroutine(ActivateAttack2());
            StartCoroutine(ActivateAttack2());
            nextTime = Time.time + 1f / attackRate;
        }
    }

    //private void Move(InputAction.CallbackContext context)
    //{
    //    horizontalMove = context.ReadValue<Vector2>().x * runSpeed;
    //    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    //}

    private void Heal(InputAction.CallbackContext context)
    {
        if (context.performed && HPLeft < HP)
        {
            healing = true;
        }
    }

    public void DisableControls()
    {
        player.Player.Disable();
    }
    public void EnableControls()
    {
        player.Player.Enable();
    }
    public void EnableGlitch()
    {
        glitch.SetFloat("_Intensity", 0.02f);
    }
    public void DisableGlitch()
    {
        glitch.SetFloat("_Intensity", 0f);
    }
    void Update()
    {
        //Old Input System
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump = true;
        //}
        //if (Input.GetButtonDown("Attack1") && Attack1 == true && Time.time>=nextTime)
        //{
        //    StopCoroutine(ActivateAttack1());
        //    StartCoroutine(ActivateAttack1());
        //    nextTime = Time.time + 1f / attackRate;
        //}
        //if (Input.GetButtonDown("Attack2") && Attack2 == true && Time.time >= nextTime)
        //{
        //    StopCoroutine(ActivateAttack2());
        //    StartCoroutine(ActivateAttack2());
        //    nextTime = Time.time + 1f / attackRate;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton4))
        //{
        //    StartCoroutine(EnableAttack1());
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton5))
        //{
        //    StartCoroutine(EnableAttack2());
        //}
        if (Mathf.Abs(rb.velocity.y) > 2f)
        {
            animator.SetBool("isJumping", true);
        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    if (HPLeft < HP)
        //    {
        //        glitch.SetFloat("_Intensity", 0.01f);
        //        levelMan.Stamina -= 10;
        //        HPLeft += 10;
        //    }
        //    else
        //    {
        //        glitch.SetFloat("_Intensity", 0f);
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha3))
        //{
        //    glitch.SetFloat("_Intensity", 0f);
        //}
        //if (Input.GetKey(KeyCode.LeftShift))
        //{

        //}
        
        if (Attack1 && acounted1 == false)
        {
            focusTop -= 20;
            acounted1 = true;
        }

        if (Attack2 && acounted2 == false)
        {
            focusTop -= 10;
            acounted2 = true;
        }

        if (Attack1 == false && acounted1)
        {
            focusTop += 20;
            acounted1 = false;
        }

        if (Attack2 == false && acounted2)
        {
            focusTop += 10;
            acounted2 = false;
        }
        
        if (focusLeft < focusTop && healing == false)
        {
            focusLeft += Mathf.CeilToInt(Time.deltaTime);
        }
        else if (focusLeft > focusTop)
        {
            focusLeft = focusTop;
        }
        HPSlider.fillAmount = (HP+(HPLeft - HP))/100f;
        Focus.fillAmount = (focus+(focusLeft-focus))/100f;
        if (focus > 100)
        {
            Rect FocusWidth = Focus.GetComponent<RectTransform>().rect;
            FocusWidth.width += FocusWidth.width*((focus-100)/100);
        }
        if (HP > 100)
        {
            Rect HPWidth = HPSlider.GetComponent<RectTransform>().rect;
            HPWidth.width += HPWidth.width * ((HP - 100) / 100);
        }
        
        color = Random.ColorHSV();
        if ((DualShock4GamepadHID)Gamepad.current != null)
        {
            var controller = (DualShock4GamepadHID)Gamepad.current;
            if (glitch.GetFloat("_Intensity") > 0f)
            {
                controller.SetLightBarColor(color);
            }

            else
            {
                if (HPLeft > HP * 0.66)
                    controller.SetLightBarColor(Color.green);
                else if (HPLeft > HP * 0.33)
                    controller.SetLightBarColor(Color.yellow);
                else
                    controller.SetLightBarColor(Color.red);
            }
        }
        if (healing)
        {
            HPLeft += Mathf.CeilToInt(Time.deltaTime);
            focusLeft -= Mathf.CeilToInt(Time.deltaTime)*5;
            if (HPLeft > HP)
            {
                glitch.SetFloat("_Intensity", 0f);
                HPLeft = HP;
                healing = false;
            }
            else
            {
                glitch.SetFloat("_Intensity", 0.01f);
            }
            healed = false;
        }
        else if (healed == false)
        {
            glitch.SetFloat("_Intensity", 0f);
            healed = true;
        }


    }
    IEnumerator EnableAttack1()
    {
        glitch.SetFloat("_Intensity", 0.01f);
        yield return new WaitForSeconds(.5f);
        glitch.SetFloat("_Intensity", 0f);
        if (Attack1 == true)
            Attack1 = false;
        else
            Attack1 = true;
    }
    IEnumerator EnableAttack2()
    {
        glitch.SetFloat("_Intensity", 0.01f);
        yield return new WaitForSeconds(.5f);
        glitch.SetFloat("_Intensity", 0f);
        if (Attack2 == true)
            Attack2 = false;
        else
            Attack2 = true;
    }
    IEnumerator ActivateAttack2()
    {
        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(.5f);
        //GetComponent<AudioSource>().Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (glitch.GetFloat("_Intensity") == 0f)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage2);
        }
        focusLeft -= 5;
    }
    IEnumerator ActivateAttack1()
    {
        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(.3f);
        camEffects.ZoomActive = true;
        Collider2D[] drawnEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1*2, enemyLayers);
        foreach (Collider2D enemy in drawnEnemies)
        {
            enemy.GetComponent<Enemy>().drawn = true;
            float distance = Vector2.Distance(attackPoint1.transform.position, enemy.transform.position);
            Vector2 pullForce = ((attackPoint1.transform.position - enemy.transform.position).normalized / distance) * pullForceIntensity;
            enemy.GetComponent<Rigidbody2D>().AddForce(pullForce*2, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(.6f);
        camEffects.ZoomActive = false;
        camEffects.Shake();
        
        //GetComponent<AudioSource>().Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, enemyLayers);
        foreach(Collider2D enemy in drawnEnemies)
        {
            enemy.GetComponent<Enemy>().drawn = false;
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            
            float distance = Vector2.Distance(attackPoint1.transform.position, enemy.transform.position);
            if (glitch.GetFloat("_Intensity") == 0f)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage1);
        }
        focusLeft -= 20;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint1 == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint1.position, attackRange1);
    }

    public void Damage(int dam)
    {
        animator.SetTrigger("Hit");
        HPLeft -= dam;
        if (HPLeft <= 0 && isDead == false)
        {
            StartCoroutine(Death());
            isDead = true;
        }
    }

    IEnumerator Death()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2);
        onDeath.Invoke();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Input.Player.Heal.performed += Heal;
        //Input.Player.Shield.performed += Shield;
        
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }

    public void onLand()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("Jumped", false);
    }

}
