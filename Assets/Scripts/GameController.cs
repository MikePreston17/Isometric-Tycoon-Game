public class GameController : Singleton<GameController>
{
    public Park park;

    public void Start()
    {
        //park = new Park();
        park = Park.Instance;
    }
    public void G()
    {

    }
}