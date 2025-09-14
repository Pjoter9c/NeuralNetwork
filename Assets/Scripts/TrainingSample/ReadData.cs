using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Read data from Excel file
public class ReadData : MonoBehaviour
{
    public List<List<Data>> datas = new List<List<Data>>();

    public List<Data> attack0 = new List<Data>();
    public List<Data> attack1 = new List<Data>();
    public List<Data> attack2 = new List<Data>();
    public List<Data> attack3 = new List<Data>();

    private void Awake()
    {
        datas.Add(attack0);
        datas.Add(attack1);
        datas.Add(attack2);
        datas.Add(attack3);

        TextAsset csvFile = Resources.Load<TextAsset>("NeuralNetwork");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] fields = line.Split(";");

            // Create new input output values data
            Data d = new Data();
            d.attackType = double.Parse(fields[0]);
            d.side = double.Parse(fields[1]);
            d.orientation = double.Parse(fields[2]);
            d.distance = double.Parse(fields[3]);
            d.inDmg = double.Parse(fields[4]);
            d.action = new double[7];
            for (int j = 0; j < 7; j++)
            {
                d.action[j] = double.Parse(fields[5 + j]);
            }
            d.learned = int.Parse(fields[12]);

            datas[int.Parse(fields[0])].Add(d);
        }
    }
}
