// BoardManager
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Configuration;
using System.Collections;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance = null;
	
	public Grid grid;
	
	public Tilemap tilemap;
	
	public GameObject redRecon;
	public GameObject redInfantry;
	public GameObject redCarrier;
	public GameObject redBomber;
	public GameObject blueapc;
	public GameObject rotary;

	public SmartTile waterTile;
	public SmartTile waterTileBlue;
	public SmartTile grassTile;
	public SmartTile grassTileBlue;
	public SmartTile mountainTile;
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
		return accessTile (gridPos.x, gridPos.y);
	}

	public SmartTile accessTile(Vector3Int gridPos){
		return accessTile (gridPos.x, gridPos.y);
	}

	public Vector3Int gridToActual(int xPos, int yPos){
		int actualX = xPos + tilemap.cellBounds.position.x;
		int actualY = yPos + tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int gridToActual(Vector2Int gridPos){
		return gridToActual (gridPos.x, gridPos.y);
	}

	public Vector3Int gridToActual(Vector3Int gridPos){
		return gridToActual (gridPos.x, gridPos.y);
	}

	public Vector3Int actualToGrid(int xPos, int yPos){
		int actualX = xPos - tilemap.cellBounds.position.x;
		int actualY = yPos - tilemap.cellBounds.position.y;
		return new Vector3Int(actualX, actualY, 0);
	}

	public Vector3Int actualToGrid(Vector2Int gridPos){
		return actualToGrid (gridPos.x, gridPos.y);
	}

	public Vector3Int actualToGrid(Vector3Int gridPos){
		return actualToGrid (gridPos.x, gridPos.y);
	}

	public static Vector3 gridToWorld(int xPos, int yPos){
		Vector3Int actualPos = instance.gridToActual(xPos, yPos);
		float worldX = (float)((0.5 + actualPos.x)*instance.grid.cellSize.x);
		float worldY = (float)((0.5 + actualPos.y)*instance.grid.cellSize.y);
		return new Vector3(worldX, worldY, 0f);
	}

	public static Vector3 gridToWorld(Vector2Int gridPos){
		return gridToWorld (gridPos.x, gridPos.y);
	}

	public static Vector3 gridToWorld(Vector3Int gridPos){
		return gridToWorld (gridPos.x, gridPos.y);
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
			if (xPos<tilemap.cellBounds.size.x && yPos<tilemap.cellBounds.size.y){
				return true;
			}
		}
		return false;
	}

	public bool inBounds(Vector2Int gridPos){
		return inBounds (gridPos.x, gridPos.y);
	}

	public bool inBounds(Vector3Int gridPos){
		return inBounds (gridPos.x, gridPos.y);
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
		return spawnUnit (unit, gridPos.x, gridPos.y);
	}

	public GameObject spawnUnit(GameObject unit, Vector3Int gridPos){
		return spawnUnit (unit, gridPos.x, gridPos.y);
	}

	public void moveUnit(GameObject unit, int destX, int destY){
		UnitScript script = unitToScript (unit);
		Vector2Int unitPos = script.position;
		unitPosArray[unitPos.x][unitPos.y] = null;
		script.move (destX, destY);
		unitPosArray[destX][destY] = unit;
	}

	public void moveUnit(GameObject unit, Vector2Int destPos){
		moveUnit (unit, destPos.x, destPos.y);
	}

	public void moveUnit(GameObject unit, Vector3Int destPos){
		moveUnit (unit, destPos.x, destPos.y);
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

	public void checkNode(UnitScript unit, Queue<Node> openSet, HashSet<Node> closedSet, Node node, string direction){
		Node newNode = null;
		if (direction == "north" && inBounds (node.x, node.y+1)){
			newNode = nodeArr [node.x, node.y + 1];
		}
		else if (direction == "east" && inBounds (node.x+1, node.y)){
			newNode = nodeArr [node.x + 1, node.y];
		}
		else if (direction == "south" && inBounds (node.x, node.y-1)){
			newNode = nodeArr [node.x, node.y - 1];
		}
		else if (direction == "west" && inBounds (node.x-1, node.y)){
			newNode = nodeArr [node.x - 1, node.y];
		}
		if (newNode!=null && unitPosArray[newNode.x][newNode.y]==null){
			if (!openSet.Contains(newNode) && !closedSet.Contains (newNode)){
				if (unit.movementType == "walk" && newNode.walkable){
					newNode.moveLeft = node.moveLeft - newNode.walkCost;
					openSet.Enqueue (newNode);
				}
				else if (unit.movementType == "tread" && newNode.treadable){
					newNode.moveLeft = node.moveLeft - newNode.treadCost;
					openSet.Enqueue (newNode);
				}
				else if (unit.movementType == "sail" && newNode.sailable){
					newNode.moveLeft = node.moveLeft - newNode.sailCost;
					openSet.Enqueue (newNode);
				}
				else if (unit.movementType == "fly" && newNode.flyable){
					newNode.moveLeft = node.moveLeft - newNode.flyCost;
					openSet.Enqueue (newNode);
				}
			}
			else {
				if (unit.movementType == "walk" && newNode.walkable && newNode.moveLeft < node.moveLeft - newNode.walkCost){
					newNode.moveLeft = node.moveLeft - newNode.walkCost;
					if (closedSet.Contains (newNode)){
						openSet.Enqueue (newNode);
					}
				}
				else if (unit.movementType == "tread" && newNode.treadable && newNode.moveLeft < node.moveLeft - newNode.treadCost){
					newNode.moveLeft = node.moveLeft - newNode.treadCost;
					if (closedSet.Contains (newNode)){
						openSet.Enqueue (newNode);
					}
				}
				else if (unit.movementType == "sail" && newNode.sailable && newNode.moveLeft < node.moveLeft - newNode.sailCost){
					newNode.moveLeft = node.moveLeft - newNode.sailCost;
					if (closedSet.Contains (newNode)){
						openSet.Enqueue (newNode);
					}
				}
				else if (unit.movementType == "fly" && newNode.flyable && newNode.moveLeft < node.moveLeft - newNode.flyCost){
					newNode.moveLeft = node.moveLeft - newNode.flyCost;
					if (closedSet.Contains (newNode)){
						openSet.Enqueue (newNode);
					}
				}
			}
		}
	}

	public void showPossibleMovements(UnitScript unit){
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
			checkNode (unit, openSet, closedSet, node, "north");
			checkNode (unit, openSet, closedSet, node, "east");
			checkNode (unit, openSet, closedSet, node, "south");
			checkNode (unit, openSet, closedSet, node, "west");
		}
	}

	public void showPossibleMovements(GameObject unitObject){
		showPossibleMovements (unitToScript (unitObject));
	}

	public void removePossibleMovements(UnitScript unit){
		for (int i = unit.position.x - unit.movement; i < unit.position.x + unit.movement + 1; i++){
			for (int j = unit.position.y - unit.movement; j < unit.position.y + unit.movement + 1; j++){
				if (inBounds (i, j)){
					resetColor (i, j, "blue");
				}
			}
		}
	}

	public void removePossibleMovements(GameObject unitObject){
		removePossibleMovements (unitToScript (unitObject));
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
		nodeArr = new Node[tilemap.cellBounds.size.x, tilemap.cellBounds.size.y];
		for (int i = 0; i < tilemap.cellBounds.size.x; i++){
			unitPosArray.Add((List<GameObject>)new List<GameObject>());
			for (int j = 0; j < tilemap.cellBounds.size.y; j++){
				unitPosArray[i].Add(null);
				SmartTile tile = accessTile (i, j);
				nodeArr [i, j] = new Node (i, j, tile.walkable, tile.treadable, tile.sailable, tile.flyable, 
					tile.walkCost, tile.treadCost, tile.sailCost, tile.flyCost);
			}
		}
		selectedUnit = null;
		spawnUnit(redRecon, 0, 0);
		spawnUnit (redInfantry, 3, 3);
		spawnUnit (redCarrier, 10, 6);
		spawnUnit (redBomber, 5, 4);
		spawnUnit (blueapc, 4, 2);
		spawnUnit (rotary, 2, 2);
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)){
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPos = worldToGrid (worldPos);
			if (selectedUnit != null){
				if (inBounds (gridPos)){
					if (unitPosArray[gridPos.x][gridPos.y] == selectedUnit){
						removePossibleMovements (selectedUnit);
						selectedUnit = null; 
					}
					if (accessTile (gridPos).moveHighlighted){
						removePossibleMovements (selectedUnit);
						moveUnit (selectedUnit, gridPos);
						selectedUnit = null;
					}
				}
			}
			else if (selectedUnit == null){
				selectedUnit = unitPosArray [gridPos.x] [gridPos.y];
				if (selectedUnit != null){
					showPossibleMovements (selectedUnit);
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
