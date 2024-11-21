using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    red,
    yellow,
    green,
    icon,
    defaul
}
public class character : abstract1
{

    public ColorType colorType;
    public Transform player;
    public Transform body;
    public LayerMask Ground;
    public Animator animator;
    public List<character> targets = new List<character>();
    public character target;
    public bool isdead = true;

    [SerializeField] Rigidbody rb;
    [SerializeField] private new Renderer renderer;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject indicatorPoint;
    public Skin skin;


    protected TargetIndicator targetIndicator;

    private GameObject currentBullet;
    private string currentAnim;
    private int score = 1;

    public virtual void AddTarget(character target)
    {
        targets.Add(target);
    }

    public void ChangeWeapon(int index)
    {
        skin.ChangeWeapon(index);
    }

    public void ChangePant(int index)
    {
        skin.ChangePant(index);
    }
    public void ChangeHair(int index)
    {
        skin.ChangeHair(index);
    }
    public void Throw()
    {
        //Weapon.SetActive(false);
        Bullet bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.OnInit(this, target.transform);
        bullet.GetComponent<Weapon>().Throw();
    }
    public void UpdatePoints()
    {
        score++;
        targetIndicator.SetScore(score);
        body.localScale = new Vector3(1f + (score - 1) * 0.2f, 1f + (score - 1) * 0.2f, 1f + (score - 1) * 0.2f);
    }
    public virtual void RemoveTaget(character target)
    {
        targets.Remove(target);
        //this.target = null;
    }

    public character GetTargetInRange()
    {
        if (targets.Count > 0)
        {
            target = targets[Random.Range(0, targets.Count)];
            return target;
        }
        return null;
    }

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorController.Ins.getColorMaterial(colorType);
    }


    public void sameColor(ColorType colorType)
    {

    }

    public Vector3 checkGround(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, 5f, Ground))
        {
            return hit.point + Vector3.up * 0f;
        }
        return point;
    }
    public bool CanMove(Vector3 point)
    {
        bool canMove = true;
        return canMove;
    }
    public virtual void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                animator.ResetTrigger(currentAnim);
            }
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    public override void OnInit()
    {
        targetIndicator = levelManager.Ins.CreateIndicatorPanel(indicatorPoint.transform);
    }
    public override void OnAttack( )
    {

    }

    public override void OnDead()
    {
        targetIndicator.gameObject.SetActive(false);
        changeAnim("Dead");
        levelManager.Ins.InitCharacterAlive();
        isdead = false;
    }

    public override void OnDespawm()
    {

    }
}
