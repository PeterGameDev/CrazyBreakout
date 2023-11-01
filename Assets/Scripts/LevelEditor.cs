using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    public int numberOfRows;
    public int numberOfColumns;
    private int gridWidth;
    private int gridHeight;
    public List<GameObject> objects;
    private GameObject currentObject;
    private List<Vector3> savedObjects;
    private bool spawnMode = true;
    public TMP_InputField levelInput;



    void Start()
    {
        savedObjects = new List<Vector3>();
        gridWidth = Screen.width / numberOfColumns;
        gridHeight = Screen.height / numberOfRows;
        //Debug.Log("Width: " + Screen.width + " Height: " + Screen.height);
        currentObject = Instantiate(objects[0]);
        currentObject.SetActive(false);
        Color color = currentObject.GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        currentObject.GetComponent<Renderer>().material.color = color;
    }
    private void Update()
    {
        Vector3 gridPoint = findNearestGridPoint();
        gridPoint.z = 0;
        if (Input.mousePosition.y > 200 && spawnMode)
        {
            showHoloSpawnIndication(gridPoint);

        }
        // spawn object, still needs to handle object selection part
        if (Input.GetMouseButtonDown(0))
        {
            if(objects.Count > 0 && Input.mousePosition.y > 200 && spawnMode)
            {
                GameObject spawnedObject = Instantiate(objects[0], gridPoint, objects[0].transform.rotation);
                savedObjects.Add(gridPoint);
            }

        }
        // deselect current object
        if (Input.GetMouseButtonDown(1))
        {
            spawnMode = false;
        }
    }

    private void showHoloSpawnIndication(Vector3 spawnPoint)
    {
        if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), spawnPoint) > 100)
        {
            currentObject.SetActive(false);
        }
        else
        {
            currentObject.SetActive(true);
            currentObject.transform.position = spawnPoint;
        }
    }

    private Vector3 findNearestGridPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        //Debug.Log("Mouse position: " + mousePosition.x + "," + mousePosition.y);
        float extraWidth = mousePosition.x % gridWidth;
        int columnNumber = Mathf.RoundToInt(mousePosition.x / gridWidth);
        float extraHeight = mousePosition.y % gridHeight;
        int rowNumber = Mathf.RoundToInt(mousePosition.y / gridHeight);
        //Debug.Log("Row: " + rowNumber + " | Column: " + columnNumber);
        Vector2 gridPoint = new Vector2(columnNumber*gridWidth, rowNumber*gridHeight);
        Vector3 worldGridPoint = Camera.main.ScreenToWorldPoint(gridPoint);
        return worldGridPoint;
    }

    public void saveLevel()
    {
        //Debug.Log("Saving level");
        //Debug.Log("List is: " + savedObjects.Count);
        if(levelInput.text.CompareTo("")==0)
        {
            Debug.Log("Enter level number in the text field!");
            return;
        }

        MyDataType.SpawnList spawnList = new MyDataType.SpawnList();
        spawnList.spawnPoints = savedObjects;
        string path = Application.dataPath + "/Levels/Level"+levelInput.text+".json";
        string jsonString = JsonUtility.ToJson(spawnList);
        Debug.Log(jsonString);
        System.IO.File.WriteAllText(path, jsonString);
    }

    // not working, draw grid guide lines
    private void drawColumns()
    {        
        // draw vertical lines from middle of screen to sides
        for (int i=0; i<Screen.width; i += gridWidth)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            var points = new Vector3[2];
            points[0] = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(i, Screen.height, GetComponent<Camera>().nearClipPlane));
            points[1] = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(i, 0, GetComponent<Camera>().nearClipPlane));
            lineRenderer.SetPositions(points);
        }

    }

    private void drawRows()
    {
        // draw horizontal lines from mid to sides
    }
}
