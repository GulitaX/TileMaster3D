using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawningLevel : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;
    public int slotCount; // Slot menu transform to hold picked objects
    public GameObject slotPrefab;
    public GameObject inventoryPanel;
    public GameObject panelController;
    public List<GameObject> inventoryList;

    private GameObject[] tiles; // Array to store tile objects
    private GameObject pickedTile; // Currently picked tile
    private int tileToSpawn;
    private int remainingTiles;
    private bool allowTouch;

    private void Start()
    {
        allowTouch = true;
        if(GameManager.instance != null)
        {
            gameManager = GameManager.instance;
            tileToSpawn = gameManager.selectedLevel.sumOfTiles();
            remainingTiles = tileToSpawn;

            audioManager = gameManager.gameObject.transform.GetChild(0).GetComponent<AudioManager>();
            audioManager.ChangeBackGroundMusic("bg-2");
        }


        CreateTiles();
        CreateInventorySlots();

    }

    private void CreateTiles()
    {
        tiles = new GameObject[tileToSpawn];

        List<TileData> tileDatas = GameManager.instance.selectedLevel.levelTileDatas;

        foreach(TileData tileData in tileDatas)
        {
            int tileCounts = tileData.numberOfTriplets * 3;

            // Instantiate tile objects at random positions
            for (int i = 0; i < tileCounts; i++)
            {
                Vector3 position = new Vector3(Random.Range(-2f, 2f), 3, Random.Range(-3f, 3f));
                GameObject tile = Instantiate(tileData.prefab, position, Quaternion.identity);

                tiles[i] = tile;
            }
        }
    }

    private void CreateInventorySlots() {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.name = "Slot " + i;
            slot.transform.SetParent(inventoryPanel.transform);
            slot.transform.localScale = new Vector3(1, 1, 1);

            inventoryList.Add(slot);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowTouch && !gameManager.isPause)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast to detect the clicked object
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Check if the clicked object is a tile
                if (IsTile(clickedObject))
                {
                    AddToInventory(clickedObject);

                }
            }
        }
        

    }

    private bool IsInventoryFull ()
    {
        foreach (GameObject slot in inventoryList)
        {
            if (slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == null)
            {
                return false;
            }
        }

        return true;
    }

    private void AddToInventory(GameObject clickedObject)
    {
        foreach(GameObject slot in inventoryList)
        {
            GameObject slotImage = slot.transform.GetChild(0).gameObject;

            if (slotImage.GetComponent<Image>().sprite == null)
            {
                slotImage.GetComponent<Image>().sprite = clickedObject.GetComponent<SpriteToTexture>().inputSprite;
                Color slotColor = slotImage.GetComponent<Image>().color;
                slotColor.a = 1f;
                slotImage.GetComponent<Image>().color = slotColor;

                inventoryList.Sort(CompareTileByIndex);

                Destroy(clickedObject);
                remainingTiles -= 1;
                audioManager.PlaySoundFx("fx-swipe");

                break;
            }
        }
        StartCoroutine(MatchThree());
       

    }

    private int CompareTileByIndex(GameObject slot1, GameObject slot2)
    {
        GameObject slotImage1 = slot1.transform.GetChild(0).gameObject;
        GameObject slotImage2 = slot1.transform.GetChild(0).gameObject;
        if (slotImage1.GetComponent<Image>().sprite != null && slotImage2.GetComponent<Image>().sprite != null)
        {
            return slotImage1.GetComponent<Image>().sprite.name.CompareTo(slotImage2.GetComponent<Image>().sprite.name);
        }
        else return 0;
    }

    private IEnumerator MatchThree()
    {
        foreach (GameObject slot in inventoryList)
        {
            int index = inventoryList.IndexOf(slot);

            if(slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite != null)
            {
                List<GameObject> matchingList = new List<GameObject>();
                matchingList.Add(slot);

                for (int i = 0;  i < inventoryList.Count; i++)
                {
                    GameObject checkSlot = inventoryList[i];
                    if (index != i && checkSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite != null)
                    {
                      
                        if (slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name
                            == checkSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name)
                        {
                            matchingList.Add(checkSlot);
                            Debug.Log("Found a match item");
                        }

                        if (matchingList.Count >= 3)
                        {
                            Debug.Log("Found 3 match items");
                            allowTouch = false;
                            audioManager.PlaySoundFx("fx-crystal");

                            yield return new WaitForSecondsRealtime(0.3f);
                            RemoveFromInventory(matchingList);

                            continue;
                        }
                        if (IsInventoryFull() && matchingList.Count < 2 && index == inventoryList.Count -1)
                        {
                            Debug.LogError("Game Over! out of spaces");
                            panelController.GetComponent<GamePanelController>().OnGameOver();
                            break;
                        }

                    }
                }

                if (remainingTiles == 0)
                {
                    Debug.LogError("Level Completed!");
                    panelController.GetComponent<GamePanelController>().OnLevelCompleted();

                }

            }

        }
    }

    private void RemoveFromInventory(List<GameObject> matchList)
    {
        foreach(GameObject slot in matchList) 
        {
            slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
            Color slotColor = slot.transform.GetChild(0).gameObject.GetComponent<Image>().color;
            slotColor.a = 0.5f;
            slot.transform.GetChild(0).gameObject.GetComponent<Image>().color = slotColor;
        }
        allowTouch = true;
    }

    private bool IsTile(GameObject obj)
    {
        // Check if the object is a tile by checking its tag
        return obj.CompareTag("Tile");
    }

}
