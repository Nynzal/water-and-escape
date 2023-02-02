using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SunEffectPostProcessing : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private Vector2 _vignetteIntensity;
    [SerializeField] private Vector2 _bloomIntensity;

    private Bloom _bloom;
    private Vignette _vignette;

    private bool _isEffectOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _postProcessVolume.profile.TryGetSettings(out _bloom);
        _postProcessVolume.profile.TryGetSettings(out _vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (Sun.sunBurnActive > 0)
        {
            _vignette.intensity.Override(_vignetteIntensity.x + 
                                         Sun.sunBurnActive * (_vignetteIntensity.y - _vignetteIntensity.x));
            _bloom.intensity.Override(_bloomIntensity.x + 
                                      Sun.sunBurnActive * (_bloomIntensity.y -_bloomIntensity.x));
        }
        else
        {
            _vignette.intensity.Override(_vignetteIntensity.x);
            _bloom.intensity.Override(_bloomIntensity.x);
        }
    }
}
