
using UnityEngine;
using UnityEngine.UI;



public class SkinShop : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Button Home;
    [SerializeField] Button Pant;
    [SerializeField] Button Hair;
    [SerializeField] Button SelectPants;
    [SerializeField] Button CancelPants;
    [SerializeField] Button SelectHair;
    [SerializeField] Button CancelHair;
    [SerializeField] GameObject PanelPant;
    [SerializeField] GameObject PanelHair;
    [SerializeField] Button[] listHair;
    [SerializeField] Button[] listPants;
    private Renderer PantRenderer;
    private Renderer HairRenderer;
    private int pantsIndexs = 0;
    private int hairIndexs = 0;

    private int hairSelect = 0;
    private int pantsSelect = 0;

    // Update is called once per frame
    void Start()
    {
        hairIndexs = PlayerPrefs.GetInt("Hair");
        pantsIndexs = PlayerPrefs.GetInt("Pants");

        //PantRenderer = Pants.GetComponent<Renderer>();
        //HairRenderer = Hairs.GetComponent<Renderer>();

        Home.onClick.AddListener(() => GoHome());
        Pant.onClick.AddListener(() => PantsRoom());
        Hair.onClick.AddListener(() => HairRoom());
        SelectPants.onClick.AddListener(() => ChangePants());
        SelectHair.onClick.AddListener(() => ChangeHair());

        listPants[0].onClick.AddListener(() => selectIndexPants(0));
        listPants[1].onClick.AddListener(() => selectIndexPants(1));
        listPants[2].onClick.AddListener(() => selectIndexPants(2));
        listPants[3].onClick.AddListener(() => selectIndexPants(3));
        listPants[4].onClick.AddListener(() => selectIndexPants(4));
        listPants[5].onClick.AddListener(() => selectIndexPants(5));
        listPants[6].onClick.AddListener(() => selectIndexPants(6));
        listPants[7].onClick.AddListener(() => selectIndexPants(7));
        listPants[8].onClick.AddListener(() => selectIndexPants(8));

        listHair[0].onClick.AddListener(() => selectIndexHair(0));
        listHair[1].onClick.AddListener(() => selectIndexHair(1));
        listHair[2].onClick.AddListener(() => selectIndexHair(2));
        listHair[3].onClick.AddListener(() => selectIndexHair(3));
        listHair[4].onClick.AddListener(() => selectIndexHair(4));
        listHair[5].onClick.AddListener(() => selectIndexHair(5));
        listHair[6].onClick.AddListener(() => selectIndexHair(6));
        listHair[7].onClick.AddListener(() => selectIndexHair(7));
        listHair[8].onClick.AddListener(() => selectIndexHair(8));
        listHair[9].onClick.AddListener(() => selectIndexHair(9));

    }
    private void InitPants(int index)
    {
        pantsSelect = index;
        if (index == pantsIndexs)
        {
            SelectPants.gameObject.SetActive(false);
            CancelPants.gameObject.SetActive(true);
        }
        else
        {
            SelectPants.gameObject.SetActive(true);
            CancelPants.gameObject.SetActive(false);
        }

    }
    private void InitHair(int index)
    {
        hairSelect = index;
        if (index == hairIndexs)
        {
            SelectHair.gameObject.SetActive(false);
            CancelHair.gameObject.SetActive(true);
        } else
        {
            SelectHair.gameObject.SetActive(true);
            CancelHair.gameObject.SetActive(false);
        }
    }

    public void selectIndexPants(int index)
    {
        InitPants(index);
    }
    public void selectIndexHair(int index)
    {
       InitHair(index);
    }
    public void ChangeHair()
    {
        hairIndexs = hairSelect;
        PlayerPrefs.SetInt("Hair", hairIndexs);
        InitHair(hairIndexs);
        player.GetComponent<character>().ChangeHair(hairIndexs);
    }
    public void ChangePants()
    {
        pantsIndexs = pantsSelect;
        PlayerPrefs.SetInt("Pants",pantsIndexs);
        InitPants(pantsIndexs);
        player.GetComponent<character>().ChangePant(pantsIndexs);
    }
    public void GoHome()
    {
        GameController.Ins.comeHome2();
    }
    public void PantsRoom()
    {
        PanelPant.SetActive(true);
        PanelHair.SetActive(false);
    }
    public void HairRoom()
    {
        PanelPant.SetActive(false);
        PanelHair.SetActive(true);
    }
}
