using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenuSystem : MonoBehaviour
{
    private List<Vector2> positions;
    private List<int> choiceNums;
    private bool gotChoices;
    private List<GameObject> gos;
    private bool nowLoading;

    [SerializeField]
    private List<bool> load;
    [SerializeField]
    private List<string> loadScenes;

    private int menuChoice;
    private int currentMenuChoice;
    private int totalChoices;

    public AudioClip choiceChange;
    public AudioClip choiceSelect;

    GameObject menuCursor;

    AudioSource as1;

    [SerializeField]
    private float xOffsetChoice, yOffsetChoice;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
        choiceNums = new List<int>();

        menuChoice = 0;
        
        as1 = GetComponent<AudioSource>();

        menuCursor = GameObject.FindWithTag("Menu Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Menu Choice") != null && !gotChoices)
        {
            GetMenuChoices();
            MoveSelection();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentMenuChoice <= 0)
            {
                currentMenuChoice = totalChoices - 1;
            }else
            {
                currentMenuChoice--;
            }
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMenuChoice >= totalChoices - 1)
            {
                currentMenuChoice = 0;
            }
            else
            {
                currentMenuChoice++;
            }
        }
        

        if (menuChoice != currentMenuChoice)
        {
            MoveSelection();
        }

        if((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && !nowLoading)
        {
            as1.PlayOneShot(choiceSelect);
            menuCursor.GetComponent<RectTransform>().localScale = 1.5f * menuCursor.GetComponent<RectTransform>().localScale;
            
            menuCursor.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
            for (int i = 0; i < gos.Count; i++)
            {
                if(currentMenuChoice == i)
                {
                    
                    if (load[i] == true)
                    {
                        nowLoading = true;
                        SceneManager.LoadSceneAsync(loadScenes[i]);
                    }else
                    {
                        Debug.Log("This should be closing the game");
                        Application.Quit();
                    }
                }
            }
        }
    }


    public void GetMenuChoices()
    {
        gos = GameObject.FindGameObjectsWithTag("Menu Choice").ToList();
        Debug.Log("Number of Menu Choices:" + gos.Count);
        /*
        for (int i = 0; i < gos.Count; i++)
        {
            Debug.Log("Position #" + i + " = x(" + gos[i].transform.position.x + "), y(" + gos[i].transform.position.y + ").");
        }
        */

        for (int i = 0; i < gos.Count; i++)
        {
            positions.Add(new Vector2(gos[i].GetComponent<RectTransform>().localPosition.x, gos[i].GetComponent<RectTransform>().localPosition.y));
            choiceNums.Add(gos[i].GetComponent<MenuChoice>().choiceNum);
        }
        totalChoices = gos.Count;
        gotChoices = true;
        
    }

    public void MoveSelection()
    {
        as1.PlayOneShot(choiceChange);
        menuChoice = currentMenuChoice;
        for (int i = 0; i < gos.Count; i++)
        {
            if (choiceNums[i] == currentMenuChoice)
            {
                menuCursor.GetComponent<RectTransform>().localPosition = positions[i] + new Vector2(xOffsetChoice, yOffsetChoice);
                break;
            }
        }
        menuCursor.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
    }
}
