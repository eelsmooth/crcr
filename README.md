# Class Reunion Chatroom
### created by rocrac

* Our game uses a text file (*.csv) for the dialogues. This is loaded from the assets at the beginning of the game and NOT at runtime. 
* The class 'ReadData.cs' loads the file at the beginning of the game and saves the values line by line in a list of models (DatabaseModel).
* In order to integrate your own languages into the game, the .csv file must be changed/overwritten. 
  *  This is located under Assets -> Resources -> Text1.csv.
* You can also offer a Text2.csv file in another language, this only has to be stored in the ReadData:
  * var dataset = Resources.Load<TextAsset>("Text2");

