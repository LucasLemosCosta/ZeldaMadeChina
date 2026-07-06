using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{


    public float maxStamina;
    public float currentStamina { get; private set; }
    public bool canSpandStamina { get; private set; }

    public Image usedStamina;
    public Image secondUsedStamina;
    private Animator anim;


    public bool change;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentStamina = maxStamina;
    }

    private void Update()
    {

        //transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation,
            //Time.deltaTime * 1000f);

        if(change)
        {
            anim.SetBool("Show", false);
            anim.SetBool("Out", true);
            change = false;
        }

        if(!canSpandStamina)
        {
            if (currentStamina >= maxStamina)
            {
                canSpandStamina = true;
            }
        }
    }
    public void IncreaseStamina()
    {
        if(currentStamina < maxStamina)
        {
            anim.SetBool("Out", false);
            anim.SetBool("Show", true);
            currentStamina = Mathf.Lerp(currentStamina, maxStamina + 1, 0.6f * Time.deltaTime);
            usedStamina.fillAmount = currentStamina / maxStamina;


        }
        else
        {
            anim.SetBool("Show", false);
            anim.SetBool("Out", true);
        }

        change = true;

    }


    public void ShowStamina()
    {
        anim.SetBool("Out", false);
        anim.SetBool("Show", true);
    }

    public void DecriseStamina()
    {
        currentStamina = Mathf.Lerp(currentStamina, -1, 0.5f * Time.deltaTime);

        usedStamina.fillAmount = currentStamina / maxStamina;
        secondUsedStamina.fillAmount = usedStamina.fillAmount + 0.1f;
        anim.SetBool("Out", false);
        anim.SetBool("Show", true);
        change = true;

        if(currentStamina <= 1f)
        {
            canSpandStamina = false;
        }
    }

}
