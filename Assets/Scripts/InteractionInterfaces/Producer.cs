using UnityEngine;

public interface Producer
{
    void OnProduce(GameObject interactor, Carryable carryable, Interaction interaction);
}