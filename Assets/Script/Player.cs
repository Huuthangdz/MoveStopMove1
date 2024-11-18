﻿using UnityEngine;
using UnityEngine.InputSystem;


public class Player : character
{
    [SerializeField] private GameObject canvasDie;
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private float speed;
    Vector2 moveVector;
    private CounterTime counter = new CounterTime();
    
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
        changeAnim("Idile");
        switch (Random.Range(1, 5))
        {
            case 1: ChangeColor(ColorType.red); break;
            case 2: ChangeColor(ColorType.yellow); break;
            case 3: ChangeColor(ColorType.green); break;
            case 4: ChangeColor(ColorType.icon); break;

            default: Debug.Log("f"); break;
        }
    }

    // Update is called once per frame
    void Update()
    {  
        ChangeWeapon(PlayerPrefs.GetInt("Weapon"));
        PlayerPrefs.GetInt("Weapon"); 
        Attack();
    }
    private void Attack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            character target = GetTargetInRange();
            if (target != null)
            {
                changeAnim("attack");
                OnAttack();
                RemoveTaget(target);
            }
            else
            {
                changeAnim("Idle");
            }
        }
    }
    public void InputPlayer(InputAction.CallbackContext context)
    {
         moveVector = context.ReadValue<Vector2>();
         
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
