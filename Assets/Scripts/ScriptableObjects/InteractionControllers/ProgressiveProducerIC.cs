using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction Controller/Progressive Producer")]
public class ProgressiveProducerIC : ProgressiveIC {
    public GameObject productPrefab;

    public override void OnFinish(GameObject interactor, Interaction interaction) {
        GameObject product = Instantiate(productPrefab, interactor.transform);

        foreach (Interactable i in subscribers) {
            if (i is Producer) {
                ((Producer)i).OnProduce(interactor, product, interaction);
            }
        }
        foreach (Interactable i in subscribers) {
            if (i is Progressive) {
                ((Progressive)i).OnFinish(interactor, interaction);
            }
        }
    }
}
