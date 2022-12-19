

public class Item : Interactable
{
    public override void CompleteInteraction()
    {
        _interactionCollider.enabled = false;
    }
}
