using TMPro;
using UnityEngine;

public enum GameMode
{
    Idle, Playing, LevelEnd
}

public class QuestWrecker : MonoBehaviour
{
    public static QuestWrecker S;
    [SerializeField] TMP_Text uiLevel;
    [SerializeField] TMP_Text uiShots;
    [SerializeField] TMP_Text uiButton;
    [SerializeField] TMP_Text menuButton;
    [SerializeField] Vector2 castlePos;
    [SerializeField] GameObject[] castles;
    [SerializeField] GameEvent OnBeatCastle;
    [SerializeField] float cameraDistance;
    float originalCameraDistance;
    string showing = "Show Slingshot";
    int level;
    int levelMax;
    int shotsTaken;
    GameObject castle;
    GameMode mode = GameMode.Idle;

    void Start()
    {
        originalCameraDistance = Camera.main.transform.position.z;
        S = this;
        level = 0;
        levelMax = castles.Length;
        menuButton.gameObject.SetActive(false);
        StartLevel();
    }

    void Update()
    {
        UpdateGUI();
        if (mode == GameMode.Playing && Goal.goalMet)
        {
            mode = GameMode.LevelEnd;
            SwitchView("Show Both");
            Invoke(nameof(NextLevel), 2f);
        }
        
        if (Input.GetKey(KeyCode.Tab))
            menuButton.gameObject.SetActive(true);
        else
            menuButton.gameObject.SetActive(false);
    }

    void StartLevel()
    {
        if (castle != null)
            Destroy(castle);

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
            Destroy(pTemp);

        GameObject[] trash = GameObject.FindGameObjectsWithTag("Castle Piece");
        foreach (GameObject pTemp in trash)
            Destroy(pTemp);


        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Slingshot");
        ProjectileLine.S.Clear();
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.Playing;
    }

    void UpdateGUI()
    {
        uiLevel.text = $"Level: {level + 1} of {levelMax}";
        uiShots.text = $"Shots Taken: {shotsTaken}";
    }

    void NextLevel()
    {
        OnBeatCastle?.Invoke();
        level++;
        if (level == levelMax)
            level = 0;
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
            eView = uiButton.text;

        showing = eView;
        Vector2 camPos = Camera.main.transform.position;
        Vector3 camOrig = new Vector3(camPos.x, camPos.y, originalCameraDistance);

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = Slingshot.S.gameObject;
                Camera.main.transform.position = camOrig;
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                Camera.main.transform.position = camOrig;
                break;

            case "Show Both":
                Vector3 newPos = GameObject.Find("ViewBoth").transform.position;
                Camera.main.transform.position = new Vector3(newPos.x, newPos.y, cameraDistance);
                FollowCam.POI = null;
                break;
        }
    }

    public string GetCurrentView() => showing;
    public static void ShotFired() => S.shotsTaken++;
}