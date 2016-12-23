using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    #region member variables

    public Texture2D m_level;
    public Color32 m_floorColour;
    public Color32 m_playerColour;
    public GameObject m_playerPrefab;
    public GameObject m_floorPrefab;
    public GameObject m_wallPrefab;


    private byte[,] m_levelData;

    #endregion

    void Start ()
    {
        m_levelData = new byte[m_level.width, m_level.height];
        BuildLevelData();
        GenerateLevel();
    }

    void BuildLevelData()
    {
        //build floors
        for (int x = 0; x < m_level.width; x++)
        {
            for (int y = 0; y < m_level.height; y++)
            {
                Color32 realCol = m_level.GetPixel(x, y);
                if (realCol.Equals(m_floorColour))
                {
                    m_levelData[x, y] = 1;
                }
                else if (realCol.Equals(m_playerColour))
                {
                    m_levelData[x, y] = 100;
                }
                else
                {
                    m_levelData[x, y] = 0;
                }
            }
        }
        //build walls
        for (int x = 0; x < m_level.width; x++)
        {
            for (int y = 0; y < m_level.height; y++)
            {
                if (m_levelData[x, y] == 0 && IsNearWall(x, y))
                    m_levelData[x, y] = 2;
            }
        }
    }

    bool IsNearWall(int x, int y)
    {
        if (x > 0 && m_levelData[x - 1, y] != 0)
            return true;
        if (x < m_level.width - 1 && m_levelData[x + 1, y] != 0)
            return true;
        if (y > 0 && m_levelData[x, y - 1] != 0)
            return true;
        if (y < m_level.height - 1 && m_levelData[x, y + 1] != 0)
            return true;
        return false;

    }

    void GenerateLevel()
    {
        for (int x = 0; x < m_level.width; x++)
        {
            for (int y = 0; y < m_level.height; y++)
            {
                switch (m_levelData[x,y])
                {
                    case 1:
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, 4.5f, y * 3), Quaternion.identity);
                    break;

                    case 2:
                        Instantiate(m_wallPrefab, new Vector3(x * 3, 1.5f, y * 3), Quaternion.identity);
                    break;

                    case 100:
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_playerPrefab, new Vector3(x * 3, 0, y * 3), Quaternion.identity);
                    break;
                }
            }
        }
    }

	void Update ()
    {
	
	}
}
