using TMPro;
using UnityEngine.EventSystems;

public class NonSubmitInputField : TMP_InputField
{
    public bool ignoreSubmit = true;

    public override void OnSubmit(BaseEventData eventData)
    {
        if (ignoreSubmit)
            return;

        base.OnSubmit(eventData);
    }
}