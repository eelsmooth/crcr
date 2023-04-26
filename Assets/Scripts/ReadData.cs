using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour {

    public List<DatabaseModel> databaseModelList = new List<DatabaseModel>();

    void Awake() {
        var dataset = Resources.Load<TextAsset>("Text1");
        var dataLines = dataset.text.Split('\n');

        for(int i = 0; i < dataLines.Length; i++) {
            var data = dataLines[i].Split('$');
            var model = new DatabaseModel();

            for(int d = 0; d < data.Length; d++) {
                if(d == 0)
                    model.name = data[0];
                else if(d == 1)
                    model.message = data[1];
                else 
                    model.call = data[2];
            }
            databaseModelList.Add(model);
        }
    }
}
