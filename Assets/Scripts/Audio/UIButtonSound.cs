using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private SoundEffect hoverSound = new SoundEffect();
    [SerializeField] private SoundEffect clickSound = new SoundEffect();

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
            hoverSound.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable)
            clickSound.Play();
    }
}