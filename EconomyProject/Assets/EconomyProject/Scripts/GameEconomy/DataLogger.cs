using Assets.EconomyProject.Scripts.Inventory;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


// CODE COURTESY OF https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html

struct AuctionItem
{
    public InventoryItem item;
    public float price;

    public string Name => item.Name;

    public AuctionItem(InventoryItem item, float price) : this()
    {
        this.item = item;
        this.price = price;
    }
}

public class DataLogger : MonoBehaviour
{
    private List<AuctionItem> _auctionItems;

    public string SaveFileName = "Saved_data.csv";

    private void Start()
    {
        _auctionItems = new List<AuctionItem>();
    }

    public void AddAuctionItem(InventoryItem item, float price)
    {
        AuctionItem newItem = new AuctionItem(item, price);
        _auctionItems.Add(newItem);
    }

    public void OutputCSV()
    {
        string[] row = new string[] { "Item Name", "Item Price" };
        List<string[]> rowData = new List<string[]>() { row };
        foreach (var item in _auctionItems)
        {
            row = new string[2] { item.Name, item.price.ToString() };
            rowData.Add(row);
        }
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/CSV/" + SaveFileName;
        #elif UNITY_ANDROID
            return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
            return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
            return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }

    void OnApplicationQuit()
    {
        OutputCSV();
    }
}
