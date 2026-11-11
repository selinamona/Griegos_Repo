
using UnityEngine;

public class ServedDishVFX : MonoBehaviour
{
    public GameObject successSparkles;
    public GameObject failSmoke;

    public void PlayEffect(string cookingResult)
    {
        if (cookingResult == "Success" && successSparkles != null)
        {
            Instantiate(successSparkles, transform.position, Quaternion.identity);
        }
        else if (cookingResult == "Fail" && failSmoke != null)
        {
            Instantiate(failSmoke, transform.position, Quaternion.identity);
        }
    }
}
