using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogSystemManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private GameObject dialogHolder;
    [SerializeField]private GameObject buttonPrefab;
    [SerializeField]private Transform buttonParent;
    [SerializeField]private Image characterImageObject;
    [SerializeField]private TextMeshProUGUI characterName;
    [SerializeField]private TextMeshProUGUI dialogText;

    private List<GameObject> buttons = new List<GameObject>();

    [Header("Dialogs")]
    public List<DialogStructForList> dialogStructList;
    [Serializable]
    public struct DialogStructForList{
        [Tooltip("Name of the dialog. Use this name when setting this dialog as next option after other dialog.")]
        public string dialogName;
        public DialogStruct dialogStruct;
    }

    public Dictionary<string, DialogStruct> dialogDic = new Dictionary<string, DialogStruct>(); 

    [Serializable]
    public struct DialogStruct{   
        public string characterName;
        public string dialogText;
        public Sprite characterImage;

        public List<dialogOptionStruct> dialogOptions;
    }

    [Serializable]
        public struct dialogOptionStruct{
            [Tooltip("Text that will be displayed inside the button.")]
            public string optionText;
            [Tooltip("This should be equal to the Dialog Name of the next Dialog.")]
            public string nextDialogName;
            [Tooltip("If true the conversation will after pressing the current button.")]
            public bool endDialog;
            [SerializeField]private UnityEvent unityEvent;
        }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ListToStruct();
        ShowDialog(dialogStructList[0].dialogName);
    }

    private void ListToStruct(){
        dialogDic.Clear();

        for (int i = 0; i < dialogStructList.Count; i++){
            dialogDic.Add(dialogStructList[i].dialogName, dialogStructList[i].dialogStruct);
        }
    }

    private void ShowDialog(string currentDialogName){
        dialogHolder.SetActive(true);

        DialogStruct currentDialogStruct = dialogDic[currentDialogName];  
        characterName.text = currentDialogStruct.characterName;
        characterImageObject.sprite = currentDialogStruct.characterImage;
        dialogText.text = currentDialogStruct.dialogText;

        foreach (GameObject button in buttons){
            Destroy(button);
        }
        buttons.Clear();

        if (currentDialogStruct.dialogOptions.Count != 0){
            for (int i = 0; i < currentDialogStruct.dialogOptions.Count; i++){
                GameObject spawnedButton = Instantiate(buttonPrefab, Vector2.zero, Quaternion.identity, buttonParent);
                spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogStruct.dialogOptions[i].optionText;

                buttons.Add(spawnedButton);

                if (!currentDialogStruct.dialogOptions[i].endDialog){
                    int dialogOptionInt = i;
                    spawnedButton.GetComponent<Button>().onClick.AddListener(() => ShowDialog(currentDialogStruct.dialogOptions[dialogOptionInt].nextDialogName));
                }
                else {
                    spawnedButton.GetComponent<Button>().onClick.AddListener(() => EndDialog());
                }
            }
        }
    }

    private void EndDialog(){
        foreach (GameObject button in buttons){
            Destroy(button);
        }
        buttons.Clear();

        dialogHolder.SetActive(false);
    }
}
