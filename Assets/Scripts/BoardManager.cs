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

	public SmartTile accessTile(int xPos, int yPos){
		Vector3Int actualPos = gridToActual(xPos, yPos);
		return tilemap.GetTile(actualPos) as SmartTile;
	}

	public SmartTile accessTile(Vector2Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		return tilemap.GetTile(actualPos) as SmartTile;
	}

	public SmartTile accessTile(Vector3Int gridPos){
		Vector3Int actualPos = gridToActual(gridPos);
		return tilemap.GetTile(actualPos) as SmartTile;
	}

	public Vector3Int gridToActual(int xPos, int yPos){
		int actualX = xPos + tilemap.cellBounds.position.x;
		int actualY = yPos + tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int gridToActual(Vector2Int gridPos){
		int actualX = gridPos.x + tilemap.cellBounds.position.x;
		int actualY = gridPos.y + tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int gridToActual(Vector3Int gridPos){
		int actualX = gridPos.x + tilemap.cellBounds.position.x;
		int actualY = gridPos.y + tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(int xPos, int yPos){
		int actualX = xPos - tilemap.cellBounds.position.x;
		int actualY = yPos - tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(Vector2Int gridPos){
		int actualX = gridPos.x - tilemap.cellBounds.position.x;
		int actualY = gridPos.y - tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(Vector3Int gridPos){
		int actualX = gridPos.x - tilemap.cellBounds.position.x;
		int actualY = gridPos.y - tilemap.cellBounds.position.y;
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

	public bool inBounds(Vector2Int gridPos){
		if (gridPos.x >= 0 & gridPos.y >= 0){
			if (gridPos.x<tilemap.cellBounds.size.x-1 & gridPos.y<tilemap.cellBounds.size.y-1){
				return true;
			}
		}
		return false;
	}

	public bool inBounds(Vector3Int gridPos){
		if (gridPos.x >= 0 & gridPos.y >= 0){
			if (gridPos.x<tilemap.cellBounds.size.x-1 & gridPos.y<tilemap.cellBounds.size.y-1){
				return true;
			}
		}
		return false;
	}

	public void changeColor(int xPos, int yPos, string newColor){
		SmartTile oldTile = accessTile (xPos, yPos);
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
		tilemap.SetTile(gridToActual (xPos, yPos), newTile);
	}

	public void resetColor(int xPos, int yPos, string oldColor){
		SmartTile oldTile = accessTile (xPos, yPos);
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
		tilemap.SetTile(gridToActual (xPos, yPos), newTile);
	}

	public GameObject spawnUnit(GameObject unit, int xPos, int yPos){
		GameObject instantiatedUnit = Instantiate(unit, gridToWorld(xPos, yPos), Quaternion.identity).gameObject;
		unitToScript (instantiatedUnit).position = new Vector2Int(xPos, yPos);
		unitPosArray[xPos][yPos] = instantiatedUnit;
		return instantiatedUnit;
	}

	public void moveUnit(GameObject unit, int destX, int destY){
		unit.transform.position = gridToWorld (destX, destY);
		Vector2Int unitPos = unitToScript(unit).position;
		unitPosArray[unitPos.x][unitPos.y] = null;
		(unit.GetComponent(typeof(UnitScript)) as UnitScript).position = new Vector2Int(destX, destY);
		unitPosArray[destX][destY] = unit;
	}

	public static bool showPossibleMovements(UnitScript unit){
		if (instance.selectedUnit == null){
			if (unit.movementType == "tread"){
				for (int i = unit.position.x - unit.movement; i < unit.position.x + unit.movement + 1; i++){
					for (int j = unit.position.y - unit.movement; j < unit.position.y + unit.movement + 1; j++){
						if (instance.inBounds(i, j) && instance.accessTile(i, j).treadable && instance.unitPosArray[i][j]==null){
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

	public static void removePossibleMovements(UnitScript unit){
		for (int i = unit.position.x - unit.movement; i < unit.position.x + unit.movement + 1; i++){
			for (int j = unit.position.y - unit.movement; j < unit.position.y + unit.movement + 1; j++){
				if (instance.inBounds (i, j)){
					if (instance.accessTile(i, j).treadable){
						instance.resetColor(i, j, "blue");
					}
				}
			}
		}
		instance.selectedUnit = null;
	}

	void Start(){
		if (instance == null){
			instance = this;
		}
		else if (instance != null){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		unitPosArray = new List<List<GameObject>>();
		for (int i = 0; i < tilemap.cellBounds.size.x-1; i++){
			unitPosArray.Add((List<GameObject>)new List<GameObject>());
			for (int j = 0; j < tilemap.cellBounds.size.y-1; j++){
				unitPosArray[i].Add(null);
			}
		}
		selectedUnit = null;
		GameObject recon1 = spawnUnit(recon, 0, 0);
		spawnUnit(recon, 1, 5);
		spawnUnit (recon, 2, 3);
		spawnUnit (recon, 6, 2);
		moveUnit(recon1, 7, 3);
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)){
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (selectedUnit != null){
				Vector3Int gridPos = worldToGrid (worldPos);
				if (inBounds (gridPos)){
					continue;
				}
			}
		}
	}
}
