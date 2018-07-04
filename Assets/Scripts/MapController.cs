using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {
	public SubManager subManager;
	public Sprite shipSprite;
	public Sprite markerSprite;
	public Sprite openImage;
	public Sprite shoreImage;
	public Sprite grassImage;
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

		for (int y = 0; y < subManager.stage.rowConfigs.Count; y++) {
			images.Add(new List<Image>());
			for (int x = 0; x < subManager.stats.maxParascopeDistance; x++) {
				Vector3 pos = CalcImagePosition(x, y);
				Image newImage = Instantiate(imageObject, pos, Quaternion.identity);
				newImage.transform.SetParent(panel.transform, false);
				images[y].Add(newImage);
			}
		}

		shipImage = Instantiate(imageObject, Vector3.zero, Quaternion.identity);
		shipImage.rectTransform.SetParent(images[subManager.GetRowIndex()][1].transform, false);
		shipImage.sprite = shipSprite;

		markerImage = Instantiate(imageObject, Vector3.zero, Quaternion.identity);
		markerImage.rectTransform.SetParent(images[subManager.GetRowIndex()][2].transform, false);
		markerImage.sprite = markerSprite;
	}

	public void ShowIcons() {
		currentParascopeDistance = subManager.stats.maxParascopeDistance;
	}

	public void HideIcons() {
		currentParascopeDistance = subManager.stats.minParascopeDistance;
	}

	// Update is called once per frame
	void Update () {
		DisplayIcons();
	}

	void DisplayIcons() {
		shipImage.rectTransform.SetParent(images[subManager.GetRowIndex()][1].rectTransform, false);
		markerImage.rectTransform.SetParent(images[subManager.GetNextRowIndex()][2].rectTransform, false);
		int shoreColumn = -1;

		for (int y = 0; y < images.Count; y++) {
			for (int x = 0; x < subManager.stats.maxParascopeDistance; x++) {
				if (x < currentParascopeDistance) {
					int relativeStep = x + subManager.position.step - 1;
					Step step = subManager.stage.GetStep(y, relativeStep);
					if (step == null) {
						if (shoreColumn == -1) {
							shoreColumn = x;
						}
						if (shoreColumn == x) {
							images[y][x].sprite = shoreImage;
						} else {
							images[y][x].sprite = grassImage;
						}
					} else if (step.hazard != null) {
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
		return (openImage.rect.size.x + margin) * subManager.stats.maxParascopeDistance + margin;
	}

	float CalcPanelHeight() {
		return (openImage.rect.size.y + margin) * subManager.stage.rowConfigs.Count + margin;
	}

	Vector3 CalcImagePosition(int column, int row) {
		float x = ((openImage.rect.size.x + margin) * column - 1) + margin + (openImage.rect.size.x / 2);
		x -= panel.rect.size.x / 2;
		float y = ((openImage.rect.size.y + margin) * row) + margin + (openImage.rect.size.y / 2);
		y = -y;
		y += panel.rect.size.y / 2;
		return new Vector2(x, y);
	}
}
