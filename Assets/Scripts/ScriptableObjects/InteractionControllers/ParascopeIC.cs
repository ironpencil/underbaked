using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParascopeIC", menuName = "InteractionControllers/Parascope")]
public class ParascopeIC : InteractionController {
    public override void HandleInteraction(GameObject target, GameObject interactor, Interaction interaction) {
		target.GetComponent<MonoBehaviour>().StartCoroutine(Watch(target, interactor));
	}

	IEnumerator Watch(GameObject target, GameObject interactor) {
		Character character = interactor.GetComponent<Character>();
		MapController map = target.GetComponent<MapController>();

		if (character != null && map != null) {
			SetCharacterBusy(character);
			map.ShowIcons();

			while (character.movementState != Character.MovementState.MOVING) {
				yield return 0;
			}

			map.HideIcons();
		}
	}
}
