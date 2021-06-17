using UnityEngine;
using UnityEngine.EventSystems;
public class InputControllerTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputController.PointerCommands Command;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Command != InputController.PointerCommands.None)
        {
            InputController.ProcessInput(Command, true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Command != InputController.PointerCommands.None)
        {
            InputController.ProcessInput(Command, false);
        }
    }
}
