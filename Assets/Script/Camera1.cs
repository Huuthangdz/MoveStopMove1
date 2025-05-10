using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    public Player player;
    private void LateUpdate()
    {
        float adjustedY = offset.y + player.score * 0.5f;
        float adjustedZ = offset.z - player.score * 0.5f;

        Vector3 adjustedOffset = new Vector3(offset.x, adjustedY, adjustedZ);
        TF.position = Vector3.Lerp(TF.position,(playerTF.position + adjustedOffset), Time.deltaTime * 10f);
        Debug.Log(adjustedOffset);
    }

}
