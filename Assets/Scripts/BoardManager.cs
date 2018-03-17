// BoardManager
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Configuration;
using System.Collections;

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

	public GameObject selectedUnit;
	public List<List<GameObject>> unitPosArray;
	public Node[,] nodeArr;

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
		if (xPos >= 0 && yPos >= 0){
			if (xPos<tilemap.cellBounds.size.x-1 && yPos<tilemap.cellBounds.size.y-1){
				return true;
			}
		}
		return false;
	}

	public bool inBounds(Vector2Int gridPos){
		if (gridPos.x >= 0 && gridPos.y >= 0){
			if (gridPos.x<tilemap.cellBounds.size.x-1 && gridPos.y<tilemap.cellBounds.size.y-1){
				return true;
			}
		}
		return false;
	}

	public bool inBounds(Vector3Int gridPos){
		if (gridPos.x >= 0 && gridPos.y >= 0){
			if (gridPos.x<tilemap.cellBounds.size.x-1 && gridPos.y<tilemap.cellBounds.size.y-1){
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

	public GameObject spawnUnit(GameObject unit, Vector2Int gridPos){
		GameObject instantiatedUnit = Instantiate(unit, gridToWorld(gridPos.x, gridPos.y), Quaternion.identity).gameObject;
		unitToScript (instantiatedUnit).position = new Vector2Int(gridPos.x, gridPos.y);
		unitPosArray[gridPos.x][gridPos.y] = instantiatedUnit;
		return instantiatedUnit;
	}

	public GameObject spawnUnit(GameObject unit, Vector3Int gridPos){
		GameObject instantiatedUnit = Instantiate(unit, gridToWorld(gridPos.x, gridPos.y), Quaternion.identity).gameObject;
		unitToScript (instantiatedUnit).position = new Vector2Int(gridPos.x, gridPos.y);
		unitPosArray[gridPos.x][gridPos.y] = instantiatedUnit;
		return instantiatedUnit;
	}

	public void moveUnit(GameObject unit, int destX, int destY){
		unit.transform.position = gridToWorld (destX, destY);
		Vector2Int unitPos = unitToScript(unit).position;
		unitPosArray[unitPos.x][unitPos.y] = null;
		(unit.GetComponent(typeof(UnitScript)) as UnitScript).position = new Vector2Int(destX, destY);
		unitPosArray[destX][destY] = unit;
	}

	public void moveUnit(GameObject unit, Vector2Int destPos){
		unit.transform.position = gridToWorld (destPos);
		Vector2Int unitPos = unitToScript(unit).position;
		unitPosArray[unitPos.x][unitPos.y] = null;
		(unit.GetComponent(typeof(UnitScript)) as UnitScript).position = new Vector2Int(destPos.x, destPos.y);
		unitPosArray[destPos.x][destPos.y] = unit;
	}

	public void moveUnit(GameObject unit, Vector3Int destPos){
		unit.transform.position = gridToWorld (destPos);
		Vector2Int unitPos = unitToScript(unit).position;
		unitPosArray[unitPos.x][unitPos.y] = null;
		(unit.GetComponent(typeof(UnitScript)) as UnitScript).position = new Vector2Int(destPos.x, destPos.y);
		unitPosArray[destPos.x][destPos.y] = unit;
	}

	public class Node {
		public int x;
		public int y;
		public int moveLeft;
		public bool walkable;
		public bool treadable;
		public bool sailable;
		public bool flyable;
		public int walkCost;
		public int treadCost;
		public int sailCost;
		public int flyCost;
		public Node (int inX, int inY, bool w, bool t, bool s, bool f, int wc, int tc, int sc, int fc){
			x = inX;
			y = inY;
			moveLeft = 0;
			walkable = w;
			treadable = t;
			sailable = s;
			flyable = f;
			walkCost = wc;
			treadCost = tc;
			sailCost = sc;
			flyCost = fc;
		}
	}

	public void showPossibleMovementsBFS(UnitScript unit){
		Queue<Node> openSet = new Queue<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		Node origNode = nodeArr [unit.position.x, unit.position.y];
		origNode.moveLeft = unit.movement;
		openSet.Enqueue (origNode);
		while (!(openSet.Count==0)){
			
			Node node = openSet.Dequeue ();
			closedSet.Add (node);
			if (!(node == origNode)){
				changeColor (node.x, node.y, "blue");
			}
			if (node.moveLeft <= 0){
				continue;
			}
			Node northNode = null;
			Node eastNode = null;
			Node southNode = null;
			Node westNode = null;
			if (inBounds (node.x, node.y+1)){
				northNode = nodeArr [node.x, node.y + 1];
			}
			if (inBounds (node.x+1, node.y)){
				eastNode = nodeArr [node.x + 1, node.y];
			}
			if (inBounds (node.x, node.y-1)){
				southNode = nodeArr [node.x, node.y - 1];
			}
			if (inBounds (node.x-1, node.y)){
				westNode = nodeArr [node.x - 1, node.y];
			}
			if (unit.movementType == "tread"){
				if (northNode!=null && northNode.treadable && unitPosArray[northNode.x][northNode.y]==null && !openSet.Contains(northNode) &&
					((!closedSet.Contains (northNode)) | northNode.moveLeft < node.moveLeft - northNode.treadCost)){
					northNode.moveLeft = node.moveLeft - northNode.treadCost;
					openSet.Enqueue (northNode);
				}
				if (eastNode!=null && eastNode.treadable && unitPosArray[eastNode.x][eastNode.y]==null && !openSet.Contains(eastNode) &&
					((!closedSet.Contains (eastNode)) | eastNode.moveLeft < node.moveLeft - eastNode.treadCost)){
					eastNode.moveLeft = node.moveLeft - eastNode.treadCost;
					openSet.Enqueue (eastNode);
				}
				if (southNode!=null && southNode.treadable && unitPosArray[southNode.x][southNode.y]==null && !openSet.Contains(southNode) &&
					((!closedSet.Contains (southNode)) | southNode.moveLeft < node.moveLeft - southNode.treadCost)){
					southNode.moveLeft = node.moveLeft - southNode.treadCost;
					openSet.Enqueue (southNode);
				}
				if (westNode!=null && westNode.treadable && unitPosArray[westNode.x][westNode.y]==null && !openSet.Contains(westNode) &&
					((!closedSet.Contains (westNode)) | westNode.moveLeft < node.moveLeft - westNode.treadCost)){
					westNode.moveLeft = node.moveLeft - westNode.treadCost;
					openSet.Enqueue (westNode);
				}
			}
		}
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
	}

	public static void removePossibleMovements(GameObject unitObject){
		UnitScript unit = instance.unitToScript (unitObject);
		for (int i = unit.position.x - unit.movement; i < unit.position.x + unit.movement + 1; i++){
			for (int j = unit.position.y - unit.movement; j < unit.position.y + unit.movement + 1; j++){
				if (instance.inBounds (i, j)){
					if (instance.accessTile(i, j).treadable){
						instance.resetColor(i, j, "blue");
					}
				}
			}
		}
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
		nodeArr = new Node[tilemap.cellBounds.size.x - 1, tilemap.cellBounds.size.y - 1];
		for (int i = 0; i < tilemap.cellBounds.size.x-1; i++){
			unitPosArray.Add((List<GameObject>)new List<GameObject>());
			for (int j = 0; j < tilemap.cellBounds.size.y-1; j++){
				unitPosArray[i].Add(null);
				SmartTile tile = accessTile (i, j);
				nodeArr [i, j] = new Node (i, j, tile.walkable, tile.treadable, tile.sailable, tile.flyable, 
					tile.walkCost, tile.treadCost, tile.sailCost, tile.flyCost);
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
			Vector3Int gridPos = worldToGrid (worldPos);
			if (selectedUnit != null){
				if (inBounds (gridPos)){
					print("tomato");
					if (unitPosArray[gridPos.x][gridPos.y] == selectedUnit){
						removePossibleMovements (selectedUnit);
						selectedUnit = null; 
					}
					if (accessTile (gridPos).moveHighlighted){
						print ("onion");
						removePossibleMovements (selectedUnit);
						moveUnit (selectedUnit, gridPos);
						selectedUnit = null;
					}
				}
			}
			else if (selectedUnit == null){
				selectedUnit = unitPosArray [gridPos.x] [gridPos.y];
				if (selectedUnit != null){
					showPossibleMovementsBFS (unitToScript (selectedUnit));
				}
			}
		}
		else if (Input.GetMouseButtonDown (1)){
			if (selectedUnit != null){
				removePossibleMovements (selectedUnit);
				selectedUnit = null;
			}
		}
	}
}
