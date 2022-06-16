using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasComponent : MonoBehaviour
{
    [SerializeField] private Image mutationBarFill;
    [SerializeField] private TextMeshProUGUI playerStateText;
    [SerializeField] private Vector2[] MutationsPositions;
    [SerializeField] private float powerLerp = 5f;

    private float targetFill;

    private Transform currentCamera;

    private void Awake()
    {
        currentCamera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.forward = currentCamera.forward;

        if (targetFill != mutationBarFill.fillAmount)
        {
            mutationBarFill.fillAmount = Mathf.Lerp(mutationBarFill.fillAmount, targetFill, Time.deltaTime * powerLerp);
        }
    }
    public void SetMutationValue(float current, float total)
    {
        targetFill = current / total;
    }
    public void SetPlayerStateText(string stateText)
    {
        playerStateText.text = stateText;
    }
    public void SetMutation(int i)
    {
        if (MutationsPositions.Length > i)
        {
            transform.localPosition = MutationsPositions[i];
        }
    }
}