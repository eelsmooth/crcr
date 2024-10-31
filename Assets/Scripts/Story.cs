using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Steamworks;

public class Story : MonoBehaviour {
	
    private List<DatabaseModel> _readDataList;
    private Fade _fade;
    private int _progressId;
	private bool _next;
	private bool _enableMusic;
	private bool _enableChatapp;
	private bool _mustType;
	private bool _isOccult;
	[SerializeField] private GameObject[] _intro;
	[SerializeField] private GameObject _chatBubble;
	[SerializeField] private GameObject _amyBubble;
	[SerializeField] private GameObject _locBubble;
	[SerializeField] private GameObject _ChatContentBox;
	[SerializeField] private GameObject _InputText;
	[SerializeField] private GameObject _TypedText;
	[SerializeField] private GameObject _inputObject;
	[SerializeField] private GameObject _chatapp;
	[SerializeField] private GameObject[] _mail;
	[SerializeField] private GameObject _desktop;
	[SerializeField] private GameObject _occult;
	[SerializeField] private GameObject _sfx_fullchat;
	[SerializeField] private GameObject _volumeWindow;
	[SerializeField] private GameObject _endImage;
	[SerializeField] private GameObject _thoughtBox;
	[SerializeField] private GameObject _narratorBox;
	[SerializeField] private GameObject _amb_rain;
	[SerializeField] private GameObject _mus_not_chill;
	[SerializeField] private GameObject _sfx_sneeze;
	[SerializeField] private GameObject _shadow_amy;
	[SerializeField] private GameObject[] _occult_box;
	[SerializeField] private GameObject _locationText;
	[SerializeField] private GameObject _locationContent;
	[SerializeField] private GameObject _tutorial;
	[SerializeField] private GameObject credits;
	[SerializeField] private GameObject[] _monsters;
	[SerializeField] private GameObject _amb_shower;
	private GameObject tempBox;
	private bool fullChatOpen;
	private bool _tutorialRead;
	private bool canActivateTutorial;
	[HideInInspector] public float volume_mus_amb;
	[HideInInspector] public float volume_sfx;
	[SerializeField] private Image _validation;
	[SerializeField] private GameObject[] chatTitle;
	[SerializeField] private GameObject chatlistContent;
	[SerializeField] private GameObject _sfx_pc_on;
	[SerializeField] private GameObject mus_chills;
	// Steam AV Entities
	private bool av_01_microwave;
	private bool av_02_shower;
	private bool av_03_amy;
	private bool av_04_end;
	
	public void HandleVolumeWindow(bool trigger){
		_volumeWindow.SetActive(trigger);
	}
	
	void Awake(){
		Screen.SetResolution(2560, 1440, true);
		volume_mus_amb = .2f;
		volume_sfx = .3f;
	}
	
	void Start() {
		if(SteamManager.Initialized) {
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
		}
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
	    _readDataList = GetComponent<ReadData>().databaseModelList;
	    tempBox = _ChatContentBox;
    }
    
	public void VolumeSoundTest(){
		Instantiate(_sfx_sneeze);
	}
    
	public void ConfirmVolume(){
		volume_mus_amb = GameObject.FindWithTag("sliderMusic").
			GetComponent<Slider>().value;
		volume_sfx = GameObject.FindWithTag("sliderSFX").
			GetComponent<Slider>().value;
		Destroy(GameObject.Find("AWAKE").gameObject);
		//Cursor.visible = false;
		StartCoroutine(StartGame());
	}
    
	public void CheckText(){
		if(_InputText.GetComponent<TextMeshProUGUI>().text 
			== _TypedText.GetComponent<TMP_InputField>().text && _mustType){
			_TypedText.GetComponent<TMP_InputField>().interactable = false;
			Instantiate(_sfx_fullchat);
			GameObject bubble;
			bubble = Instantiate(_amyBubble);
			bubble.transform.SetParent(_ChatContentBox.transform);
			bubble.transform.localScale = new Vector3(1,1,1);
			string nameWithColor = "<color=#9E9724>Amy</color>"; 
			bubble.transform.GetChild(0).transform.GetChild(0).
				GetComponent<TextMeshProUGUI>().text 
				= nameWithColor + "\n<color=#000000>"
				+ _InputText.GetComponent<TextMeshProUGUI>().text
				+ "</color>";
			_InputText.GetComponent<TextMeshProUGUI>().text = "";
			_TypedText.GetComponent<TMP_InputField>().text = "";
			_TypedText.GetComponent<TMP_InputField>().OnDeselect(
				new BaseEventData(EventSystem.current));
			_next = true;
			_mustType = false;
		}
	}
	
	public void CheckText2(){
		_TypedText.GetComponent<TMP_InputField>().interactable = false;
		Instantiate(_sfx_fullchat);
		GameObject bubble;
		bubble = Instantiate(_amyBubble);
		bubble.transform.SetParent(_ChatContentBox.transform);
		bubble.transform.localScale = new Vector3(1,1,1);
		string nameWithColor = "<color=#9E9724>Amy</color>"; 
		bubble.transform.GetChild(0).transform.GetChild(0).
			GetComponent<TextMeshProUGUI>().text 
			= nameWithColor + "\n<color=#000000>"
			+ _InputText.GetComponent<TextMeshProUGUI>().text
			+ "</color>";
		_InputText.GetComponent<TextMeshProUGUI>().text = "";
		_TypedText.GetComponent<TMP_InputField>().text = "";
		_TypedText.GetComponent<TMP_InputField>().OnDeselect(
			new BaseEventData(EventSystem.current));
		_next = true;
		_mustType = false;
	}

	void Update(){
		if(canActivateTutorial && !_tutorialRead &&
		Input.GetKeyDown("space")||Input.GetKeyDown(KeyCode.Return)){
			_tutorialRead = true;
		}
		if(_mustType){
			if(_InputText.GetComponent<TextMeshProUGUI>().text 
				== _TypedText.GetComponent<TMP_InputField>().text){
				_validation.color = 
					new Color(7f/255f, 191f/255f, 0f/255f, 255f/255f);
			}else{
				_validation.color = 
					new Color(191f/255f, 8f/255f, 0f/255f, 255f/255f);
			}
		}else{
			_validation.color = 
				new Color(255f/255f, 255f/255f, 255f/255f, 0f/255f);
		}
		if(GameObject.FindWithTag("sliderMusic") != null){
			volume_mus_amb = GameObject.FindWithTag("sliderMusic").
				GetComponent<Slider>().value;
		}
		if(GameObject.FindWithTag("sliderSFX") != null){
			volume_sfx = GameObject.FindWithTag("sliderSFX").
				GetComponent<Slider>().value;
		}
		
		if(_InputText.GetComponent<TextMeshProUGUI>().text 
			== _TypedText.GetComponent<TMP_InputField>().text && _mustType &&
			Input.GetKeyDown(KeyCode.Return)){
			// message sent
			CheckText();
		}
		if((Input.GetKeyDown("space")||Input.GetKeyDown(KeyCode.Return)) 
			&& _next && !_mustType) {
            _next = false;
            _progressId++;
            _thoughtBox.SetActive(false);
			StartCoroutine(LoadContent(_progressId)); 
			}
		
		if(_progressId == 20 &&  // 20
		(Input.GetKeyDown("space")||Input.GetKeyDown(KeyCode.Return))){
			_mail[0].SetActive(false);
			_mail[1].SetActive(false);
			_mail[2].SetActive(false);
			
		}
		if(_progressId == 28 && !_enableChatapp){ // 28
			_enableChatapp = !_enableChatapp;
			_chatapp.SetActive(true);
		}
		if(_progressId == 29 && !fullChatOpen){
			fullChatOpen = true;
			Destroy(GameObject.Find("Fullchat").GetComponent<Animator>());
		}
	}
	
	public void ProgressText(){
		if(_next && !_mustType) {
			_next = false;
			_progressId++;
			_thoughtBox.SetActive(false);
			StartCoroutine(LoadContent(_progressId)); 
		}
	}
    
	public void SkipText(){
		if(_mustType){
			av_03_amy = true;
			CheckText2();
		}
	}
	
	IEnumerator GameOver(){
		yield return new WaitForSeconds(2);
		_monsters[0].SetActive(true);
		yield return new WaitForSeconds(2);
		_monsters[1].SetActive(true);
		yield return new WaitForSeconds(2);
		_monsters[2].SetActive(true);
		yield return new WaitForSeconds(2);
		credits.SetActive(true);
	}
    
	private IEnumerator StartGame(){
		Destroy(GameObject.FindWithTag("mus_intro").gameObject);
		StartCoroutine(_fade.FadeIn());
		yield return new WaitForSeconds(3f);
		StartCoroutine(_fade.FadeOut());
		yield return new WaitForSeconds(2.5f);
		_tutorial.SetActive(true);
		StartCoroutine(_fade.FadeIn());
		yield return new WaitForSeconds(3f);
		canActivateTutorial = true;
		while(!_tutorialRead){
			yield return new WaitForSeconds(.1f);
		}
		StartCoroutine(_fade.FadeOut());
		yield return new WaitForSeconds(2.5f);
		_tutorial.SetActive(false);
		_intro[0].SetActive(false);
		_intro[1].SetActive(true);
		StartCoroutine(_fade.FadeIn());
		yield return new WaitForSeconds(5f);
		StartCoroutine(_fade.FadeOut());
		yield return new WaitForSeconds(2.5f);
		Destroy(GameObject.FindWithTag("intro"));
		_narratorBox.SetActive(true);
		_narratorBox.transform.GetChild(1).gameObject.
			GetComponent<TextMeshProUGUI>().text = _readDataList[0].message;
		StartCoroutine(_fade.FadeIn());
		Instantiate(Resources.Load("SFX/" + _readDataList[0].call));
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(_fade.FadeOut());
		yield return new WaitForSeconds(2.5f);
		_narratorBox.transform.GetChild(1).gameObject.
			GetComponent<TextMeshProUGUI>().text = _readDataList[1].message;
		Instantiate(Resources.Load("SFX/" + _readDataList[1].call));
		StartCoroutine(_fade.FadeIn());
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(_fade.FadeOut());
		yield return new WaitForSeconds(2.5f);
		Instantiate(_amb_rain);
		_narratorBox.SetActive(false);
		StartCoroutine(_fade.FadeIn());
		yield return new WaitForSeconds(2f);
		_progressId = 2; // Ersten beiden schon durchgeführt
		StartCoroutine(LoadContent(_progressId));
	}

    private IEnumerator LoadContent(int id) {
	    var data = _readDataList[id];
	    
	    if(data.call.Contains("pau_3sec")){
	    	yield return new WaitForSeconds(3f);
	    }
	    
	    if(data.call.Contains("sfx_message_received")){
	    	StartCoroutine(GameObject.FindWithTag("rain").
		    	GetComponent<AudioManager>().StopAudio());
		    StartCoroutine(GameObject.FindWithTag("mus_not_chill").
		    	GetComponent<AudioManager>().StopAudio());
	    }
	    
	    if(data.call.Contains("scn_chat_creepy")){
	    	// scn_chat_creepy 
	    	StartCoroutine(GameObject.FindWithTag("mus_chill").
		    	GetComponent<AudioManager>().StopAudio());
	    	if(GameObject.FindWithTag("rain") != null){
	    		StartCoroutine(GameObject.FindWithTag("rain").
		    		GetComponent<AudioManager>().StartAudio());
	    	}else{
	    		Instantiate(_amb_rain);
	    	}
	    	//Instantiate(_mus_not_chill);
	    }
	    
	    if(data.call.Contains("microwave_off")){
	    	StartCoroutine(GameObject.FindWithTag("amb_microwave").
		    	GetComponent<AudioManager>().StopAudio());
		    SteamUserStats.SetAchievement("av_01_microwave");
		    SteamUserStats.StoreStats();
	    }
	    if(data.call.Contains("blaise_horror_off")){
	    	StartCoroutine(GameObject.FindWithTag("amb_blaise").
		    	GetComponent<AudioManager>().StopAudio());
	    }
	    if(data.call.Contains("shower_off")){
	    	StartCoroutine(GameObject.FindWithTag("amb_shower").
		    	GetComponent<AudioManager>().StopAudio());
		    Instantiate(_amb_rain);
		    Instantiate(_mus_not_chill);
		    SteamUserStats.SetAchievement("av_02_shower");
		    SteamUserStats.StoreStats();
	    }
	    if(data.call.Contains("amy_horror_off")){
	    	StartCoroutine(GameObject.FindWithTag("amb_amy").
		    	GetComponent<AudioManager>().StopAudio());
	    }
	    if(data.call.Contains("mike_horror_off")){
	    	StartCoroutine(GameObject.FindWithTag("amb_mike").
		    	GetComponent<AudioManager>().StopAudio());
	    }
	    
	    if(data.call.Contains("amb_shower")){
	    	StartCoroutine(GameObject.FindWithTag("mus_chill").
		    	GetComponent<AudioManager>().StopAudio());
	    	StartCoroutine(GameObject.FindWithTag("rain").
		    	GetComponent<AudioManager>().StopAudio());    	
		    //Instantiate(_amb_shower);
	    }
	    
	    if(data.call.Contains("mus_less_chill")){
	    	StartCoroutine(GameObject.FindWithTag("rain").
		    	GetComponent<AudioManager>().StartAudio());
	    }
	    
	    if(data.call.Contains("stop_less")){
	    	if(GameObject.FindWithTag("muss_less_chill") != null){
	    		GameObject.FindWithTag("muss_less_chill").
		    		GetComponent<AudioManager>().loadedMusic = false;
	    		StartCoroutine(GameObject.FindWithTag("muss_less_chill").
		    		GetComponent<AudioManager>().StopAudio());
		    	if(GameObject.FindWithTag("rain") != null){
			    	StartCoroutine(GameObject.FindWithTag("rain").
				    	GetComponent<AudioManager>().StopAudio());
		    	}
	    	}
	    }else if(data.call.Contains("stop_not")){
	    	if(GameObject.FindWithTag("mus_not_chill") != null){
	    		GameObject.FindWithTag("mus_not_chill").
		    		GetComponent<AudioManager>().loadedMusic = false;
	    		StartCoroutine(GameObject.FindWithTag("mus_not_chill").
		    		GetComponent<AudioManager>().StopAudio());
		    	if(GameObject.FindWithTag("rain") != null){
			    	StartCoroutine(GameObject.FindWithTag("rain").
				    	GetComponent<AudioManager>().StopAudio());
		    	}
	    	}
	    }
	    else if(data.call.Contains("stop_it")){
	    	if(GameObject.FindWithTag("mus_chill") != null){
	    		GameObject.FindWithTag("mus_chill").
		    		GetComponent<AudioManager>().loadedMusic = false;
	    		StartCoroutine(GameObject.FindWithTag("mus_chill").
		    		GetComponent<AudioManager>().StopAudio());
	    	}
	    	if(GameObject.FindWithTag("rain") != null){
	    		StartCoroutine(GameObject.FindWithTag("rain").
		    		GetComponent<AudioManager>().StopAudio());
	    	}
	    }
	    
	    if(data.call.Contains("scn_the_end")){
	    	// END
	    	_endImage.SetActive(true);
	    	StartCoroutine(GameOver());
	    	Instantiate(mus_chills);
		    SteamUserStats.SetAchievement("av_04_end");
		    SteamUserStats.StoreStats();
		    if(!av_03_amy){
		    	// Du hast jeden Text selbst geschrieben.
		    	Debug.Log("av_03_amy");
		    	SteamUserStats.SetAchievement("av_03_amy");
			    SteamUserStats.StoreStats();
		    }
	    }
	    
	    if(data.call.Contains("location")){
	    	GameObject obj = Instantiate(_locBubble);
		    obj.transform.SetParent(_ChatContentBox.transform);
		    obj.transform.localScale = new Vector3(1,1,1);
	    }

	    if(data.call.Contains("sfx") 
		    || data.call.Contains("amb") 
		    || data.call.Contains("mus")) {
		    if(Resources.Load("SFX/" + data.call) != null){
		    	Instantiate(Resources.Load("SFX/" + data.call));
		    }
	    }
	   	
    	if(data.call.Contains("scn_laptop")){
    		Instantiate(_sfx_pc_on);
    		_desktop.SetActive(true);
    		yield return new WaitForSeconds(.5f);
    		_desktop.transform.GetChild(0).gameObject.SetActive(true);
    	}
    	
    	if(data.name.Contains("occult")){
    		if(GetComponent<ReadData>().langIndex == 0 || GetComponent<ReadData>().langIndex == 1){
    			_occult_box[0].SetActive(true);
    		}else{
    			_occult_box[0].SetActive(true);
    		}
    	}else {
    		_occult_box[0].SetActive(false);
    		_occult_box[1].SetActive(false);
    	}
    	if(data.name.Contains("black")){	
    		_shadow_amy.SetActive(true);
    		_shadow_amy.transform.GetChild(0).gameObject.
	    		GetComponent<TextMeshProUGUI>().text = 
	    		data.message;
	    	StartCoroutine(_fade.FadeIn());
	    	yield return new WaitForSeconds(2.5f);
    	}
    	else{
    		_shadow_amy.SetActive(false);
    	}
    	if(data.name.Contains("unbreak")){
    		// Amy Text ÜBER Fade hierarchie
    		StartCoroutine(_fade.FadeIn());
	    	yield return new WaitForSeconds(1.5f);
    	}
    	else if(data.name.Contains("break")){
    		yield return new WaitForSeconds(1.5f);
    		_desktop.SetActive(true);
    		yield return new WaitForSeconds(.5f);
    		_desktop.transform.GetChild(0).gameObject.SetActive(true);
    		StartCoroutine(_fade.FadeOut());
    		yield return new WaitForSeconds(.5f);
    	}
	    if(data.call.Contains("scn_chat_liv")){
		    chatlistContent.transform.GetChild(1).SetAsFirstSibling();
		    chatTitle[0].SetActive(false);
		    chatTitle[1].SetActive(true);
	    	GameObject.FindWithTag("Chat").GetComponent<SortChat>().DestroyContent();
	    	GameObject bb = Instantiate(_chatBubble);
		    bb.transform.SetParent(_ChatContentBox.transform);
		    bb.transform.localScale = new Vector3(1,1,1);
		    Instantiate(_sfx_fullchat);
		    string nameWithColor = "<color=#275B9C>Liv</color>";
		    bb.transform.GetChild(0).transform.GetChild(0).gameObject.
		    	GetComponent<TextMeshProUGUI>().text 
		    	= nameWithColor + "\n<color=#000000>Hey Amy.</color>";
	    }else if(data.call.Contains("kbmehr")){
	    	GameObject.FindWithTag("Chat").GetComponent<SortChat>().DestroyContent();
	    	chatlistContent.transform.GetChild(1).SetAsFirstSibling();
	    	chatTitle[0].SetActive(true);
		    chatTitle[1].SetActive(false);
	    }
	    else if(data.name.Contains("mail")){
	    	_mail[GetComponent<ReadData>().langIndex].SetActive(true);
	    }
	    else if(data.name.Contains("-")) {
        
	    }
	    else if(data.name.Contains("AmyInput")){
	    	_mustType = true;
        	_InputText.GetComponent<TextMeshProUGUI>().text
        		= data.message;
        	_TypedText.GetComponent<TMP_InputField>().Select();
		    _TypedText.GetComponent<TMP_InputField>().ActivateInputField();
		    _TypedText.GetComponent<TMP_InputField>().interactable = true;
	    }
	    else if(data.name.Contains("Amy")) {
            _thoughtBox.SetActive(true);
	        _thoughtBox.transform.GetChild(2).gameObject.
		        GetComponent<TextMeshProUGUI>().text = data.message;
		    yield return new WaitForSeconds(.25f);
        }
	    else if(!data.name.Contains("break") && 
		    !data.name.Contains("black") &&
	    	!data.name.Contains("occult")){
	    	Instantiate(_sfx_fullchat);
		    GameObject bubble;
		    bubble = Instantiate(_chatBubble); 
	        bubble.transform.SetParent(_ChatContentBox.transform);
		    bubble.transform.localScale = new Vector3(1,1,1);
		    string nameWithColor;
		    
		    if(data.name.Contains("Angie"))
		    	nameWithColor = "<color=#9C2778>Gigi <3</color>";
		    else if(data.name.Contains("Mike"))
		    	nameWithColor = "<color=#279C2B>Maik</color>";
		    else
		    	nameWithColor = "<color=#275B9C>"+data.name+"</color>"; 
		    	
		    bubble.transform.GetChild(0).transform.GetChild(0).gameObject.
			    GetComponent<TextMeshProUGUI>().text 
			    = nameWithColor + "\n<color=#000000>" + data.message + "</color>";
        }
        _next = true;
        yield return null;
    }
}
