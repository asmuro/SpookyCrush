using Assets.Scripts;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetFirstTilePosition(int width, int height)
    {
        return new Vector2(((float)width) / 2 * -1 + Constants.TILE_SIZE / 2
            , ((float)height) / 2 * -1 + Constants.TILE_SIZE / 2);
    }

    public void Instantiate(Vector2 tilePosition, GameObject parent, int i, int j)
    {
        var tileBackground = Instantiate(this, tilePosition, Quaternion.identity);
        tileBackground.transform.parent = parent.transform;
        tileBackground.name = $"{nameof(BackgroundTile)} ( {i}, {j} )";
    }
}
