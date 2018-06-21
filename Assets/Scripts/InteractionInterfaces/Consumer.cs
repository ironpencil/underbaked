using UnityEngine;

public interface Consumer
{
    void OnConsume(Carrier carrier, Consumable consumable, Interaction interaction);
}