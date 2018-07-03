using UnityEngine;

public interface Producer
{
    void OnProduce(GameObject interactor, GameObject product, Interaction interaction);
}