using UnityEngine;
using UnityEngine.InputSystem;


public class Player : character
{
    [SerializeField] private GameObject canvasDie;
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private float speed;
    private CounterTime counter = new CounterTime();

    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private bool isJoyStick;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float rotationSpeed;
    //[SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        EnableJoyStickInput();
        OnInit();
        switch (Random.Range(1, 5))
        {
            case 1: ChangeColor(ColorType.red); break;
            case 2: ChangeColor(ColorType.yellow); break;
            case 3: ChangeColor(ColorType.green); break;
            case 4: ChangeColor(ColorType.icon); break;

            default: Debug.Log("f"); break;
        }
        // check xem animator của attack có được gọi không 

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Debug.Log("attack");
        }
        else
        {
            Debug.Log("not attack");
        }
    }
    public void EnableJoyStickInput()
    {
        isJoyStick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    void Update()
    {  
        ChangeWeapon(PlayerPrefs.GetInt("Weapon"));
        PlayerPrefs.GetInt("Weapon"); 
        Attack();
        MoveJoyStick();
    }
    private void MoveJoyStick()
    {
        if (isJoyStick)
        {
            var movermentDirection = new Vector3(variableJoystick.Direction.x, 0, variableJoystick.Direction.y);
            characterController.SimpleMove(movermentDirection * speed);

            if (movermentDirection.sqrMagnitude <= 0)
            {
                animator.SetBool("Run", false);
                return;
            }
            animator.SetBool("Run", true);
            var targetRotation = Vector3.RotateTowards(characterController.transform.forward, movermentDirection, rotationSpeed * Time.deltaTime, 0.0f);
            characterController.transform.rotation = Quaternion.LookRotation(targetRotation);
        }
    }
    private void Attack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            character target = GetTargetInRange();
            if (target != null)
            {
                animator.SetBool("Attack",true);
                OnAttack();
                RemoveTaget(target);
                Invoke("EndAttack", 1.02f);
            }
        }
    }
    private void EndAttack()
    {
        animator.SetBool("Attack", false);
    }
    public override void OnAttack()
    {
        base.OnAttack();
        counter.Start(Throw, 0.2f);

    }
    public override void OnDead()
    {
        base.OnDead();
        GameController.Ins.PlayerDead();
        canvasDie.SetActive(true);
    }
    public override void OnInit()
    {
        skin.ChangeWeapon(PlayerPrefs.GetInt("weapon"));
        skin.ChangePant(PlayerPrefs.GetInt("Pants"));
        base.OnInit();
    }
}
