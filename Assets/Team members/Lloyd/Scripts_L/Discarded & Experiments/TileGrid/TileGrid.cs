using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public static TileGrid Instance { get; private set; }

    public GameObject squarePrefab;
    public Material whiteMaterial;
    public Material blackMaterial;
    public float squareSize = 1f;
    public int boardSize = 8;
    
    private GameObject[,] squares;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CreateSquares();
        
    }

    public GameObject[,] GetSquares()
    {
        return squares;
    }


        private void CreateSquares()
    {
        squares = new GameObject[boardSize, boardSize];
        
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject square = Instantiate(squarePrefab, transform);
                square.transform.position = new Vector3(i * squareSize, 0f, j * squareSize);
                square.transform.localScale = new Vector3(squareSize, 1f, squareSize);
                
                Renderer renderer = square.GetComponent<Renderer>();
                renderer.material = (i + j) % 2 == 0 ? whiteMaterial : blackMaterial;
                
                squares[i, j] = square;
            }
        }
    }
}