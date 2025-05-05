using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;


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
    [SerializeField] private GameObject[] bulletPrefabs; // Mảng các bullet prefab
    [SerializeField] private float throwForce;
    [SerializeField] private bool isCanMove;

    public int scoreCamera;

    void Start()
    {
        isCanMove = false;
        EnableJoyStickInput();
        OnInit();

        switch (Random.Range(1,4))
        {
            case 1: ChangeColor(ColorType.red); break;
            case 2: ChangeColor(ColorType.yellow); break;
            case 3: ChangeColor(ColorType.green); break;
            case 4: ChangeColor(ColorType.icon); break;
        }
        scoreCamera = score;

        ChangeHair(PlayerPrefs.GetInt("Hair"));
        ChangePant(PlayerPrefs.GetInt("Pants"));
        ChangeWeapon(PlayerPrefs.GetInt("Weapon"));
    }
    public void EnableJoyStickInput()
    {
        isJoyStick = true;
        Invoke("isCanAttack", 1f);
        inputCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        int weaponIndex = PlayerPrefs.GetInt("Weapon");
        ChangeWeapon(weaponIndex);
        MoveJoyStick();
        Attack(weaponIndex);
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
    private void Attack(int weaponIndex)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isCanMove)
            {
                StartCoroutine(OnAttack(weaponIndex));
                Invoke("EndAttack", 1.02f);
            }
        }
    }
    private bool isCanAttack()
    {
        return isCanMove = true;
    }
    private void EndAttack()
    {
        animator.SetBool("Attack", false);
    }
    private IEnumerator OnAttack(int weaponIndex)
    {
        yield return new WaitForSeconds(0.1f);
        character target = GetTargetInRange();
        if (target != null)
        {
            animator.SetBool("Attack", true);
            GameObject bulletPrefab = bulletPrefabs[weaponIndex];
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            Vector3 direction = (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position).normalized;
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.OnInit(this, target.transform);
            RemoveTaget(target);
        }
    }
    public override void OnDead()
    {
        base.OnDead();
        GameController.Ins.PlayerDead();
        canvasDie.SetActive(true);
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void ChangeHair(int index)
    {
        base.ChangeHair(PlayerPrefs.GetInt("Hair"));
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(PlayerPrefs.GetInt("Pants"));
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(PlayerPrefs.GetInt("Weapon"));
    }
}
