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
    public sealed class DitheringEditor : Editor {} }
#else
    [PostProcessEditor(typeof(Dithering))]
    public sealed class DitheringEditor : PostProcessEffectEditor<Dithering>
    {
        SerializedParameterOverride intensity;
        SerializedParameterOverride tiling;
        SerializedParameterOverride luminanceThreshold;
        SerializedParameterOverride lut;
#if DITHERING_WORLD_PROJECTION
        SerializedParameterOverride worldProjected;
#endif

        public override void OnEnable()
        {
            lut = FindParameterOverride(x => x.lut);
            intensity = FindParameterOverride(x => x.intensity);
            tiling = FindParameterOverride(x => x.tiling);
            luminanceThreshold = FindParameterOverride(x => x.luminanceThreshold);
#if DITHERING_WORLD_PROJECTION
            worldProjected = FindParameterOverride(x => x.worldProjected);
#endif
        }

        public override void OnInspectorGUI()
        {
            SCPE_GUI.DisplayDocumentationButton("dithering");

            SCPE_GUI.DisplaySetupWarning<DitheringRenderer>();

            PropertyField(lut);

            if (lut.overrideState.boolValue && lut.value.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Assign a pattern texture", MessageType.Info);
            }

            EditorGUILayout.Space();

            PropertyField(luminanceThreshold);
            PropertyField(intensity);
#if DITHERING_WORLD_PROJECTION
            PropertyField(worldProjected);
#endif
            PropertyField(tiling);
        }
    }
}
#endif