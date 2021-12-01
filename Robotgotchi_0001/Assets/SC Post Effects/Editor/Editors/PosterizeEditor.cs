using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if PPS
using UnityEditor.Rendering.PostProcessing;
using UnityEngine.Rendering.PostProcessing;
#endif

namespace SCPE
{
#if !PPS
    public sealed class PosterizeEditor : Editor {} }
#else
    [PostProcessEditor(typeof(Posterize))]
    public sealed class PosterizeEditor : PostProcessEffectEditor<Posterize>
    {
        SerializedParameterOverride hsvMode;
        SerializedParameterOverride levels;
        SerializedParameterOverride hue;
        SerializedParameterOverride saturation;
        SerializedParameterOverride value;

        public override void OnEnable()
        {
            hsvMode = FindParameterOverride(x => x.hsvMode);
            levels = FindParameterOverride(x => x.levels);
            hue = FindParameterOverride(x => x.hue);
            saturation = FindParameterOverride(x => x.saturation);
            value = FindParameterOverride(x => x.value);
        }

        public override void OnInspectorGUI()
        {
            SCPE_GUI.DisplayDocumentationButton("posterize");

            SCPE_GUI.DisplaySetupWarning<PosterizeRenderer>();

            PropertyField(hsvMode);
            if (hsvMode.value.boolValue)
            {
                PropertyField(hue);
                PropertyField(saturation);
                PropertyField(value);
            }
            else
            {
                PropertyField(levels);
            }
        }
    }
}
#endif
