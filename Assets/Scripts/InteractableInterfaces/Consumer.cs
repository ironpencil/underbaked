using UnityEngine;

public interface Consumer
{
    void OnConsume(GameObject interactor, Consumable consumable, Interaction interaction);
}