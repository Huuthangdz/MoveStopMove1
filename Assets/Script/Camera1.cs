using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    public Player player;
    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position,(playerTF.position + offset) * player.scoreCamera, Time.deltaTime * 5f);
    }
}
