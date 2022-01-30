using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class IA : MonoBehaviour
{
    [SerializeField] private EventReference stepSound;
    [SerializeField] private EventReference seeSound;

    private GameManager gm;

    private PlayerController player;

    public bool freeze = false;
    public bool inPlay = true;


    public float velocity;
    [Range(0, 50)]
    public float range;
    [Range(0, 10)]
    public float rangeAttack;
    private Transform objective;

    public Transform punto;

    private Transform randomPoint;


    public void step() { 
        if(inPlay)
            RuntimeManager.PlayOneShot(stepSound);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();

        randomPoint = Instantiate(punto, getRandomVector2InRange(this.transform, 20f), Quaternion.identity);
        randomPoint.tag = "randomPoint";
        StartCoroutine(hotFix());
        StartCoroutine(lerpObjective());
        //StartCoroutine(lerpObjective());
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlay)
        {
            if (objective != null)
            {
                if (freeze)
                {
                    velocity = 0.25f;
                }
                else if (objective.tag == "player")
                {
                    velocity = 0.5f;
                }
                else
                {
                    velocity = 0.3f;
                }
            }
            else
            {
                velocity = 0;
            }
            getObjetive();
        }
    }

    void getObjetive() {

        Transform preObjetive;

        GameObject[] candles = GameObject.FindGameObjectsWithTag("candle");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        preObjetive = player.transform;

        float distancePreObjective = Vector2.Distance(this.transform.position, preObjetive.position);

        if (candles.Length > 0)
        {
            foreach (GameObject candle in candles)
            {
                float distanceCandle = Vector2.Distance(candle.transform.position, this.transform.position);
                if (distanceCandle < distancePreObjective)
                {
                    preObjetive = candle.transform;
                    distancePreObjective = distanceCandle;
                }
            }
        }

        if (range > distancePreObjective)
        {
            if (preObjetive!=null && objective!=null && preObjetive.tag == "Player" && objective.tag != "Player")
            {
                Debug.Log("goes brrr");
                RuntimeManager.PlayOneShot(seeSound);
            }
            objective = preObjetive;
        }
        else {
            if (objective == null || objective.gameObject.tag != "randomPoint") {
                objective = randomPoint.transform;
            }
            
        }
    }

    IEnumerator lerpObjective() {
        yield return new WaitUntil(() => objective != null);
        while (inPlay)
        {
            yield return new WaitForEndOfFrame();
            if (Vector2.Distance(this.transform.position, objective.position) > rangeAttack)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, objective.position, Time.deltaTime * velocity);
            }
            else
            {
                Debug.Log("He llegado a " + objective.tag);
                switch (objective.tag.ToLower().Trim())
                {
                    case "player":
                        gm.lose();
                        inPlay = false;
                        break;
                    case "candle":
                        yield return new WaitForSeconds(3f);
                        if (objective != null && objective.tag == "candle") {
                            candleController vela = objective.GetComponent<candleController>();
                            vela.apagarVela();
                        }
                        randomPoint.position = getRandomVector2InRange(this.transform, 30f);
                        objective = randomPoint;
                        yield return new WaitForEndOfFrame();
                        break;
                    case "randompoint":
                        this.transform.position = getRandomVector2InRange(player.transform, 30f);
                        randomPoint.position = getRandomVector2InRange(this.transform, 20f);
                        break;
                    default:
                        break;
                }
                objective = null;
            }
        }
        yield return new WaitForEndOfFrame();


    }

    IEnumerator hotFix() {
        while (inPlay) {
            yield return new WaitForEndOfFrame();
            if (objective == null)
                getObjetive();
        }
        yield return new WaitForEndOfFrame();
    }

    Vector2 getRandomVector2InRange(Transform center, float range) {
        Vector2 newVector = center.transform.position;
        newVector = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized * range;
        return newVector;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a blue sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);

        // Draw a red sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}
