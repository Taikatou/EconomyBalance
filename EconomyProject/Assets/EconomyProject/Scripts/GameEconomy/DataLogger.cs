﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
// CODE COURTESY OF https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html

    struct AuctionItem
    {
        public InventoryItem item;
        public float price;
        public int agentId;

        public string Name => item.itemName;

        public AuctionItem(InventoryItem item, float price, int agentId) : this()
        {
            this.item = item;
            this.price = price;
            this.agentId = agentId;
        }
    }

    public class DataLogger : MonoBehaviour
    {
        private List<AuctionItem> _auctionItems;

        public static int staticLoggerId = 0;

        private string learningEnvironmentId = "agent_id_";

        public int loggerId;

        public string GetFileName
        {
            get
            {
                string nowStr = DateTime.Now.ToString("_dd_MM_yyyy_HH_mm");
                return learningEnvironmentId + loggerId + nowStr + ".csv";
            }
        }

        private void Start()
        {
            staticLoggerId++;
            loggerId = staticLoggerId;
            _auctionItems = new List<AuctionItem>();
        }

        public void AddAuctionItem(InventoryItem item, float price, EconomyAgent agent)
        {
            AuctionItem newItem = new AuctionItem(item, price, agent.agentId);
            _auctionItems.Add(newItem);
        }

        public void OutputCsv()
        {
            string[] row = { "Item Name", "Item Price" };
            List<string[]> rowData = new List<string[]> { row };
            foreach (var item in _auctionItems)
            {
                row = new[] { item.Name, item.price.ToString(CultureInfo.InvariantCulture), item.agentId.ToString() };
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


            string filePath = GetPath();

            StreamWriter outStream = System.IO.File.CreateText(filePath);
            outStream.WriteLine(sb);
            outStream.Close();
        }

        // Following method is used to retrive the relative path as device platform
        private string GetPath()
        {
            #if UNITY_EDITOR
                return Application.dataPath + "/CSV/" + GetFileName;
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
            OutputCsv();
        }

        public void Reset()
        {
            OutputCsv();
            _auctionItems.Clear();
        }
    }
}