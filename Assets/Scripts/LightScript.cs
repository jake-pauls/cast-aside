using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject lighthouseBeams;
    public Light directionalLight;
    float beamAngle = 0.0f, beamRotateSpeed = 25.0f;

    public Material greybox;
    [Range(5.0f, 20.0f)] public float greyboxRotationSpeed = 5.0f;
    const float GREYBOX_LIGHTNING_RANGE_MIN = 0.75f, GREYBOX_LIGHTNING_RANGE_MAX = 1.25f;
    [Range(0.1f, 10.0f)] public float greyboxLightningSpeed = 0.1f;
    float[] greyboxLightningLevels = new float[] { -1, -1, -1 };
    int greyboxLightningWaveCount = -1, greyboxLightningBoltAmount;
    bool greyboxLightningWaveIncline = true;
    float greyboxLightningWait = -1, greyboxLightningCount, greyboxLightningWave;//wait = 1.5-5 seconds
    int greyboxExposureID, greyboxRotationID;
    float greyboxRotation, greyboxExposure;
    float greyboxBaseExposure;//0.35

    void Start()
    {
        directionalLight.intensity = 0.0f;
        greyboxExposureID = Shader.PropertyToID("_Exposure");
        greyboxRotationID = Shader.PropertyToID("_Rotation");
        greyboxRotation = greybox.GetFloat(greyboxRotationID);
        greyboxExposure = greybox.GetFloat(greyboxExposureID);
        greyboxBaseExposure = greyboxExposure;
    }

    void Update()
    {
        beamAngle += Time.deltaTime * beamRotateSpeed;
        if (beamAngle >= 360f)
            beamAngle = 0.0f;
        lighthouseBeams.transform.eulerAngles = new Vector3(0.0f, beamAngle, 0.0f);
        greybox.SetFloat(greyboxRotationID, greybox.GetFloat(greyboxRotationID) + Time.deltaTime * greyboxRotationSpeed);

        if(greyboxLightningWait == -1)
        {
            greyboxLightningWait = Random.Range(1.5f, 5.0f);
            greyboxLightningCount = 0;
        }
        else
        {
            if(greyboxLightningCount >= greyboxLightningWait)
            {
                if(greyboxLightningLevels[0] == -1)
                {
                    greyboxLightningWave = greyboxBaseExposure;
                    greyboxLightningWaveCount = 0;
                    greyboxLightningBoltAmount = Random.Range(1, 4);
                    for (int i = 0; i < greyboxLightningBoltAmount; i++)
                        greyboxLightningLevels[i] = Random.Range(GREYBOX_LIGHTNING_RANGE_MIN, GREYBOX_LIGHTNING_RANGE_MAX);
                }
                else
                {
                    greybox.SetFloat(greyboxExposureID, greyboxLightningWave);
                    directionalLight.intensity = greyboxLightningWave / greyboxLightningLevels[greyboxLightningWaveCount];
                    if (greyboxLightningWaveIncline)
                    {
                        greyboxLightningWave += Time.deltaTime * greyboxLightningSpeed;
                        if (greyboxLightningWave >= greyboxLightningLevels[greyboxLightningWaveCount])
                        {
                            greyboxLightningWaveIncline = false;
                            greyboxLightningWave = greyboxLightningLevels[greyboxLightningWaveCount];
                        }
                    }
                    else
                    {
                        greyboxLightningWave -= Time.deltaTime * greyboxLightningSpeed;
                        if(greyboxLightningWave <= greyboxBaseExposure)
                        {
                            greyboxLightningWaveIncline = true;
                            greyboxLightningWave = greyboxBaseExposure;
                            if(++greyboxLightningWaveCount > greyboxLightningBoltAmount - 1)
                            {
                                directionalLight.intensity = 0.0f;
                                greybox.SetFloat(greyboxExposureID, greyboxBaseExposure);
                                greyboxLightningWait = -1;
                                for (int i = 0; i < greyboxLightningBoltAmount; i++)
                                    greyboxLightningLevels[i] = -1;
                            }
                        }
                    }
                }
            }
            else
                greyboxLightningCount += Time.deltaTime;
        }
    }
}
