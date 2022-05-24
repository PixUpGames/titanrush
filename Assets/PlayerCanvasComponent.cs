using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasComponent : MonoBehaviour
{
    [SerializeField] private Image mutationBarFill;
    [SerializeField] private TextMeshProUGUI playerStateText;

    private Transform currentCamera;

    private void Awake()
    {
        currentCamera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.forward = currentCamera.forward;
    }
    public void SetMutationValue(float current, float total)
    {
        mutationBarFill.fillAmount = current / total;
    }
    public void SetPlayerStateText(string stateText)
    {
        playerStateText.text = stateText;
    }
}