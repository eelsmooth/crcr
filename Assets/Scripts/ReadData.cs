using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReadData : MonoBehaviour {
	public TMP_Text awake_ui_title;
	public TMP_Text awake_ui_music_title;
	public TMP_Text awake_ui_sfx_title;
	public TMP_Text awake_ui_language_title;
	public TMP_Dropdown dropDown;
	public TMP_Text awake_ui_tutorial_title;
	public TMP_Text awake_ui_tutorial_1;
	public TMP_Text awake_ui_tutorial_2;
	public TMP_Text awake_ui_tutorial_3;
	public TMP_Text awake_ui_ingame_opt_music;
	public TMP_Text awake_ui_ingame_opt_sfx;
	public TMP_Text awake_ui_warning_1;
	public TMP_Text awake_ui_warning_2;

	public List<DatabaseModel> databaseModelList = new List<DatabaseModel>();
	
	private TextAsset dataset = null;
	private DatabaseModel model = null;
	private string[] dataLines = null;
	private string[] data = null;
	public int langIndex;
	
	void Awake() {
		SetupLanguage();
		
		langIndex = PlayerPrefs.GetInt("language", 0);
	}
	
	private void SetupLanguage(){
		dataset = null;
		model = null;
		data = null;
		databaseModelList.Clear();
		ChangeLanguageUI(PlayerPrefs.GetInt("language", 0));
		dropDown.value = PlayerPrefs.GetInt("language", 0);
		dataset = Resources.Load<TextAsset>("Text" + PlayerPrefs.GetInt("language", 0));
		
		dataLines = dataset.text.Split('\n');
		for(int i = 0; i < dataLines.Length; i++) {
			data = dataLines[i].Split('$');
			model = new DatabaseModel();

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
    
	public void OnDropDownChanged(TMP_Dropdown dropDown) {
		
		// ENGLISH - 0
		if(dropDown.value == 0){
			langIndex = 0;
			PlayerPrefs.SetInt("language", 0);
		}
			
		// FRENCH - 1
		else if(dropDown.value == 1){
			langIndex = 1;
			PlayerPrefs.SetInt("language", 1);
		}
			
		// GERMAN - 2
		else if(dropDown.value == 2){
			langIndex = 2;
			PlayerPrefs.SetInt("language", 2);
		}
			
		PlayerPrefs.Save();
		SetupLanguage();
	}
	
	public void ChangeLanguageUI(int l) {
		// ENGLISH
		if(l == 0){
			awake_ui_title.text = "- GAME SETTINGS - ";
			awake_ui_music_title.text = "MUSIC / AMBIANCE";
			awake_ui_sfx_title.text = "SOUND EFFECTS";
			awake_ui_language_title.text = "LANGUAGE";
			awake_ui_ingame_opt_music.text = "Music / Ambiance";
			awake_ui_ingame_opt_sfx.text = "Sounds";
			awake_ui_warning_1.text = "This game features descriptions of violence, gore and animal cruelty.";
			awake_ui_warning_2.text = "Play with lights off and with headphones for a better experience.";
			
			awake_ui_tutorial_title.text = "Tutorial";
			awake_ui_tutorial_1.text = "1.\n\nPress Space to advance the story.";
			awake_ui_tutorial_2.text = "2.\n\nType the gray, pre-written text into the chat box if Amy is responding, and press the send button or Enter to send it.";
			awake_ui_tutorial_3.text = "3.\n\nAlternatively, you can press the double arrow button to autofill the text.";
		}
		// FRENCH
		else if(l == 1){
			awake_ui_title.text = "- PARAMÈTRES DU JEU - ";
			awake_ui_music_title.text = "MUSIQUE / AMBIANCE";
			awake_ui_sfx_title.text = "Effets sonores";
			awake_ui_language_title.text = "LANGUE";
			awake_ui_ingame_opt_music.text = "Musique / Ambiance";
			awake_ui_ingame_opt_sfx.text = "Sons";
			awake_ui_warning_1.text = "Ce jeu contient des descriptions de violence, de gore et de cruauté envers les animaux" ;
			awake_ui_warning_2.text = "Jouez avec les lumières éteintes et avec des écouteurs pour une meilleure expérience" ;
			
			awake_ui_tutorial_title.text = "Tutoriel";
			awake_ui_tutorial_1.text = "1.\n\nAppuyez sur Espace pour faire avancer l'histoire.";
			awake_ui_tutorial_2.text = "2.\n\nTapez le texte gris pré-écrit dans la boîte de dialogue si Amy répond, et appuyez sur le bouton d'envoi ou sur la touche Entrée pour l'envoyer.";
			awake_ui_tutorial_3.text = "3.\n\nVous pouvez également appuyer sur la touche à double flèche pour remplir automatiquement le texte.";
		}
		// GERMAN
		else if(l == 2){
			awake_ui_title.text = "- SPIELEINSTELLUNGEN  - ";
			awake_ui_music_title.text = "MUSIK / AMBIENTE";
			awake_ui_sfx_title.text = "SOUNDEFFEKTE";
			awake_ui_language_title.text = "SPRACHE";
			awake_ui_ingame_opt_music.text = "Musik / Ambiente";
			awake_ui_ingame_opt_sfx.text = "Sounds";
			awake_ui_warning_1.text = "Dieses Spiel enthält Beschreibungen von Gewalt, Grausamkeit und Tierquälerei.";
			awake_ui_warning_2.text = "Spielen Sie bei ausgeschaltetem Licht und mit Kopfhörern, um ein besseres Erlebnis zu haben.";
			
			awake_ui_tutorial_title.text = "Tutorial";
			awake_ui_tutorial_1.text = "1.\n\nDrücken Sie die Leertaste, um die Geschichte fortzusetzen.";
			awake_ui_tutorial_2.text = "2.\n\nGeben Sie den grauen, vorformulierten Text in das Chat-Feld ein, wenn Amy antwortet, und drücken Sie die Schaltfläche Senden oder die Eingabetaste, um ihn abzuschicken.";
			awake_ui_tutorial_3.text = "3.\n\nAlternativ können Sie auch die Doppelpfeiltaste drücken, um den Text automatisch auszufüllen.";
		}
	}
}
