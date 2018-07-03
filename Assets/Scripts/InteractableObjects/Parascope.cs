using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parascope : ProgressiveInteractableImpl {
    public MapController map;
	public override void OnStart(GameObject interactor, Interaction interaction, Progression progression)
    {
		map.ShowIcons();
    }

	public override void OnStop(GameObject interactor, Interaction interaction, Progression progression)
    {
        map.HideIcons();
    }
}