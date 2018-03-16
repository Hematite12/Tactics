// BoardManager
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance = null;
	[SerializeField]
	public Grid grid;
	[SerializeField]
	public Tilemap tilemap;
	[SerializeField]
	public GameObject recon;
	[SerializeField]
	public SmartTile waterTile;
	[SerializeField]
	public SmartTile waterTileBlue;
	[SerializeField]
	public SmartTile grassTile;
	[SerializeField]
	public SmartTile grassTileBlue;
	[SerializeField]
	public SmartTile mountainTile;
	[SerializeField]
	public SmartTile mountainTileBlue;

	public UnitScript selectedUnit;
	public List<List<GameObject>> unitPosArray;

	public UnitScript unitToScript(GameObject unit){
		return unit.GetComponent(typeof(UnitScript)) as UnitScript;
	}

	public GameObject scriptToUnit(UnitScript script){
		return script.gameObject;
	}

	public SmartTile accessTile(Tilemap map, int xPos, int yPos){
		Vector3Int actualPos = gridToActual(xPos, yPos);
		return map.GetTile(actualPos) as SmartTile;
	}

	public SmartTile accessTile(Tilemap map, Vector2Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		return map.GetTile(actualPos) as SmartTile;
	}

	public SmartTile accessTile(Tilemap map, Vector3Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		return map.GetTile(actualPos) as SmartTile;
	}

	public Vector3Int gridToActual(int xPos, int yPos){
		int actualX = xPos + tilemap.cellBounds.position.x;
		int actualY = xPos + tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int gridToActual(Vector2Int gridPos){
		int actualX = gridPos.x + tilemap.cellBounds.position.x;
		int actualY = gridPos.y + tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int gridToActual(Vector3Int gridPos){
		int actualX = gridPos.x + tilemap.cellBounds.position.x;
		int actualY = gridPos.y + tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(int xPos, int yPos){
		int actualX = xPos - tilemap.cellBounds.position.x;
		int actualY = xPos - tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(Vector2Int gridPos){
		int actualX = gridPos.x - tilemap.cellBounds.position.x;
		int actualY = gridPos.y - tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(Vector3Int gridPos){
		int actualX = gridPos.x - tilemap.cellBounds.position.x;
		int actualY = gridPos.y - tilemap.cellBounds.position.x;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3 gridToWorld(int xPos, int yPos){
		Vector3Int actualPos = gridToActual(xPos, yPos);
		float worldX = (float)((0.5 + actualPos.x)*grid.cellSize.x);
		float worldY = (float)((0.5 + actualPos.y)*grid.cellSize.y);
		return new Vector3(worldX, worldY, 0f);
	}

	public Vector3 gridToWorld(Vector2Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		float worldX = (float)((0.5 + actualPos.x)*grid.cellSize.x);
		float worldY = (float)((0.5 + actualPos.y)*grid.cellSize.y);
		return new Vector3(worldX, worldY, 0f);
	}

	public Vector3 gridToWorld(Vector3Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		float worldX = (float)((0.5 + actualPos.x)*grid.cellSize.x);
		float worldY = (float)((0.5 + actualPos.y)*grid.cellSize.y);
		return new Vector3(worldX, worldY, 0f);
	}

	public Vector3 actualToWorld(int xPos, int yPos){
		return gridToWorld(actualToGrid(xPos, yPos));
	}

	public Vector3 actualToWorld(Vector2Int actualPos){
		return gridToWorld(actualToGrid(actualPos));
	}

	public Vector3 actualToWorld(Vector3Int actualPos){
		return gridToWorld(actualToGrid(actualPos));
	}

	public Vector3Int worldToActual(float xPos, float yPos){
		return new Vector3Int(Mathf.FloorToInt(xPos / grid.cellSize.x), Mathf.FloorToInt(yPos / grid.cellSize.y), 0);
	}

	public Vector3Int worldToActual(Vector2 worldPos){
		return new Vector3Int(Mathf.FloorToInt(worldPos.x / grid.cellSize.x), Mathf.FloorToInt(worldPos.y / grid.cellSize.y), 0);
	}

	public Vector3Int worldToActual(Vector3 worldPos){
		return new Vector3Int(Mathf.FloorToInt(worldPos.x / grid.cellSize.x), Mathf.FloorToInt(worldPos.y / grid.cellSize.y), 0);
	}

	public Vector3Int worldToGrid(float xPos, float yPos){
		return actualToGrid(worldToActual(xPos, yPos));
	}

	public Vector3Int worldToGrid(Vector2 worldPos){
		return actualToGrid(worldToActual(worldPos));
	}

	public Vector3Int worldToGrid(Vector3 worldPos){
		return actualToGrid(worldToActual(worldPos));
	}

	public bool inBounds(int xPos, int yPos){
		if (xPos >= 0 & yPos >= 0){
			if (xPos<tilemap.cellBounds.size.x-1 & yPos<tilemap.cellBounds.size.y-1){
				return true;
			}
		}
		return false;
	}

	public void changeColor(int xPos, int yPos, string newColor){
		Vector3Int actualPos = gridToActual(xPos, yPos);
		SmartTile oldTile = accessTile (actualPos);
		SmartTile newTile = oldTile;
		if (newColor == "blue"){
			if (oldTile == waterTile){
				newTile = waterTileBlue;
			}
			else if (oldTile == grassTile){
				newTile = grassTileBlue;
			}
			else if (oldTile == mountainTile){
				newTile = mountainTileBlue;
			}
		}
		tilemap.SetTile(actualPos, newTile);
	}

	public void resetColor(int xPos, int yPos, string oldColor)
	{
		Vector3Int actualPos = gridToActual(xPos, yPos);
		SmartTile oldTile = accessTile (tilemap, actualPos);
		SmartTile newTile = oldTile;
		if (oldColor == "blue"){
			if (oldTile == waterTileBlue){
				newTile = waterTile;
			}
			else if (oldTile == grassTileBlue){
				newTile = grassTile;
			}
			else if (oldTile == mountainTileBlue){
				newTile = mountainTile;
			}
		}
		tilemap.SetTile(actualPos, newTile);
	}

	public GameObject spawnUnit(GameObject unit, int xPos, int yPos){
		GameObject instantiatedUnit = Instantiate(unit, gridToWorld(xPos, yPos), Quaternion.identity).gameObject;
		unitToScript (instantiatedUnit).position = new Vector2Int(xPos, yPos);
		((List<GameObject>)unitPosArray[xPos])[yPos] = instantiatedUnit;
		return instantiatedUnit;
	}

	public void moveUnit(GameObject unit, int destX, int destY){
		unit.transform.position = gridToWorld (destX, destY);
		Vector2Int unitPos = unitToScript(unit).position;
		((List<GameObject>)unitPosArray[unitPos.x])[unitPos.y] = null;
		(unit.GetComponent(typeof(UnitScript)) as UnitScript).position = new Vector2Int(destX, destY);
		((List<GameObject>)unitPosArray[destX])[destY] = unit;
	}

	public static bool showPossibleMovements(UnitScript unit){
		if (instance.selectedUnit == null){
			if (unit.movementType == "tread"){
				for (int i = unit.position.x - unit.movement; i < unit.position.x + unit.movement + 1; i++){
					for (int j = unit.position.y - unit.movement; j < unit.position.y + unit.movement + 1; j++){
						if (instance.inBounds(i, j) && instance.accessTile(instance.tilemap, i, j).treadable){
							instance.changeColor(i, j, "blue");
						}
					}
				}
			}
			instance.selectedUnit = unit;
			return true;
		}
		return false;
	}

	public static void removePossibleMovements(UnitScript unit)
	{
		for (int i = unit.position.get_x() - unit.movement; i < unit.position.get_x() + unit.movement + 1; i++)
		{
			for (int j = unit.position.get_y() - unit.movement; j < unit.position.get_y() + unit.movement + 1; j++)
			{
				if (instance.inBounds(i, j) && instance.accessTile(instance.tilemap, i, j).treadable)
				{
					instance.resetColor(i, j, "blue");
				}
			}
		}
		instance.selectedUnit = null;
	}

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Object.Destroy(this.get_gameObject());
		}
		Object.DontDestroyOnLoad(this.get_gameObject());
		unitPosArray = new List<List<GameObject>>();
		int num = 0;
		while (true)
		{
			int num2 = num;
			BoundsInt cellBounds = tilemap.get_cellBounds();
			Vector3Int size = cellBounds.get_size();
			if (num2 < size.get_x() - 1)
			{
				unitPosArray.Add((List<GameObject>)new List<GameObject>());
				int num3 = 0;
				while (true)
				{
					int num4 = num3;
					BoundsInt cellBounds2 = tilemap.get_cellBounds();
					Vector3Int size2 = cellBounds2.get_size();
					if (num4 < size2.get_y() - 1)
					{
						((List<GameObject>)unitPosArray[num]).Add(null);
						num3++;
						continue;
					}
					break;
				}
				num++;
				continue;
			}
			break;
		}
		selectedUnit = null;
		GameObject val = spawnUnit(recon, 0, 0);
		GameObject val2 = spawnUnit(recon, 1, 5);
		moveUnit(val, 7, 3);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 worldPos = Camera.get_main().ScreenToWorldPoint(Input.get_mousePosition());
			SmartTile smartTile = accessTile(tilemap, this.worldToGrid(worldPos));
			MonoBehaviour.print((object)unitPosArray.Count);
			MonoBehaviour.print((object)((List<GameObject>)unitPosArray[0]).Count);
			for (int i = 0; i < unitPosArray.Count; i++)
			{
				unitPosArray.ForEach(delegate(List<GameObject> a)
					{
						MonoBehaviour.print((object)("{0}\t" + a.ToString()));
					});
				for (int j = 0; j < ((List<GameObject>)unitPosArray[0]).Count; j++)
				{
					if (!(((List<GameObject>)unitPosArray[i])[j] != null))
					{
						continue;
					}
				}
			}
		}
	}
}