using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {
	public ShipManager ship;
	public Sprite shipSprite;
	public Sprite markerSprite;
	public Sprite openImage;
	public Sprite unknownImage;
	public Image imageObject;
	public RectTransform panel;
	public float margin;
	public List<List<Image>> images;
	private Image shipImage;
	private Image markerImage;
	private int currentParascopeDistance;

	void Start() {
		images = new List<List<Image>>();
		
		UpdatePanelDimensions();
		HideIcons();

		for (int y = 0; y < ship.stage.rowConfigs.Count; y++) {
			images.Add(new List<Image>());
			for (int x = 0; x < ship.ship.maxParascopeDistance; x++) {
				Vector3 pos = CalcImagePosition(x, y);
				Image newImage = Instantiate(imageObject, pos, Quaternion.identity);
				newImage.transform.SetParent(panel.transform, false);
				images[y].Add(newImage);
			}
		}

		shipImage = Instantiate(imageObject, CalcImagePosition(1, ship.stage.GetRowIndex(ship.position.row)), Quaternion.identity);
		shipImage.transform.SetParent(panel.transform, false);
		shipImage.sprite = shipSprite;

		markerImage = Instantiate(imageObject, CalcImagePosition(2, ship.stage.GetRowIndex(ship.position.nextRow)), Quaternion.identity);
		markerImage.transform.SetParent(panel.transform, false);
		markerImage.sprite = markerSprite;
	}

	public void ShowIcons() {
		currentParascopeDistance = ship.ship.maxParascopeDistance;
	}

	public void HideIcons() {
		currentParascopeDistance = ship.ship.minParascopeDistance;
	}

	// Update is called once per frame
	void Update () {
		DisplayIcons();
	}

	void DisplayIcons() {
		shipImage.rectTransform.localPosition = CalcImagePosition(1, ship.stage.GetRowIndex(ship.position.row));
		markerImage.rectTransform.localPosition = CalcImagePosition(2, ship.stage.GetRowIndex(ship.position.nextRow));
		
		for (int y = 0; y < images.Count; y++) {
			for (int x = 0; x < ship.ship.maxParascopeDistance; x++) {
				if (x < currentParascopeDistance) {
					int relativeStep = x + ship.position.step - 1;
					Step step = ship.stage.GetStep(y, relativeStep);
					if (step.hazard != null) {
						images[y][x].sprite = step.hazard.icon;
					} else {
						images[y][x].sprite = openImage;
					}
				} else {
					images[y][x].sprite = unknownImage;
				}
			}
		}
	}

	void UpdatePanelDimensions() {
		panel.sizeDelta = new Vector2(CalcPanelWidth(), CalcPanelHeight());
	}

	float CalcPanelWidth() {
		return (openImage.rect.size.x + margin) * ship.ship.maxParascopeDistance + margin;
	}

	float CalcPanelHeight() {
		return (openImage.rect.size.y + margin) * ship.stage.rowConfigs.Count + margin;
	}

	Vector3 CalcImagePosition(int column, int row) {
		//return Vector2.zero;
		float x = ((openImage.rect.size.x + margin) * column - 1) + margin + (openImage.rect.size.x / 2);
		x -= panel.rect.size.x / 2;
		float y = ((openImage.rect.size.y + margin) * row) + margin + (openImage.rect.size.y / 2);
		y = -y;
		//Debug.Log(y + " = ((" + openImage.rect.size.y + " + " + margin + ") * " + row + " - 1) + " + margin + " + (" + openImage.rect.size.y + " / 2)");
		y += panel.rect.size.y / 2;
		return new Vector2(x, y);
	}
}
