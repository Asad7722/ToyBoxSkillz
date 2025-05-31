using UnityEngine;


public class SceneTransition : MonoBehaviour
{
    public string scene = "<Insert scene name>";
    public float duration = 0.10f;
    public Color color = Color.black;
    public GAME_STATE state;
    

    public void PerformTransition()
	{

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {

            bool giftboxbool = Configuration.instance.GiftBox;
            bool randomplaybool = Configuration.instance.RandomPlay;
            bool arenaplay = Configuration.instance.ArenaMode;

            if (CoreData.instance.GetOpendedLevel() == Configuration.instance.FirstGoMap && !giftboxbool && !randomplaybool && !arenaplay)
            {
                Transition.LoadLevel("Map", 0.1f, Color.black);
            }
            else
            {
                Transition.LoadLevel(scene, 0.1f, Color.black);
            }
        }

        else
        {
            Transition.LoadLevel(scene, 0.1f, Color.black);
        }
       
    }
}
