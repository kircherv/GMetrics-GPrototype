using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    public Transform player;
    public Text scoreText;
    public Text positionXText;
    public Text positionYText;

    // Update is called once per frame
    void Update () {
        //Debug.Log(player.position.x);
        scoreText.text = player.position.x.ToString("0");
        positionXText.text = player.position.x.ToString();
        positionYText.text = player.position.y.ToString();
    }
}
