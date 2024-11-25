using System.Collections;
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

    public int scoreCamera;

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
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        //{
        //    Debug.Log("attack");
        //}
        //else
        //{
        //    Debug.Log("not attack");
        //}
        scoreCamera = score;
    }
    public void EnableJoyStickInput()
    {
        isJoyStick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        //Debug.Log(scoreCamera);
        int weaponIndex = PlayerPrefs.GetInt("Weapon");
        ChangeWeapon(weaponIndex);
        Attack(weaponIndex);
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
    private void Attack(int weaponIndex)
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(OnAttack(weaponIndex));
            Invoke("EndAttack", 1.02f);
        }
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
            //Debug.Log("ThrowBullet");
            // ThowBullet
            // Kiểm tra bulletPrefabs
            //if (bulletPrefabs == null || bulletPrefabs.Length == 0)
            //{
            //    Debug.LogError("BulletPrefabs array is not assigned or empty.");
            //    return;
            //}
            // Kiểm tra weaponIndex
            //if (weaponIndex < 0 || weaponIndex >= bulletPrefabs.Length)
            //{
            //    Debug.LogError("Invalid weapon index.");
            //    return;
            //}
            // Kiểm tra target
            //if (target == null)
            //{
            //    Debug.LogError("Target is not assigned.");
            //    return;
            //}
            // Instantiate the bullet
            GameObject bulletPrefab = bulletPrefabs[weaponIndex];
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Kiểm tra Rigidbody
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            //if (rb == null)
            //{
            //    Debug.LogError("Rigidbody component not found on the bullet.");
            //    return;
            //}
            // Calculate direction and apply force
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.OnInit(this, target.transform);
            RemoveTaget(target);
            // End Throw Bullet    
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
        skin.ChangeWeapon(PlayerPrefs.GetInt("weapon"));
        skin.ChangePant(PlayerPrefs.GetInt("Pants"));
        base.OnInit();
    }
}
