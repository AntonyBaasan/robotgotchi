using System;
using UnityEngine;
#if PPS
using UnityEngine.Rendering.PostProcessing;
using FloatParameter = UnityEngine.Rendering.PostProcessing.FloatParameter;
#endif

namespace SCPE
{
#if PPS
    [PostProcess(typeof(PosterizeRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Retro/Posterize", true)]
#endif
    [Serializable]
    public sealed class Posterize : PostProcessEffectSettings
    {
#if PPS
        public BoolParameter hsvMode = new BoolParameter { value = false };

        [Range(0, 256)]
        public IntParameter levels = new IntParameter { value = 256 };

        [Header("Levels")]
        [Range(0, 256)]
        public IntParameter hue = new IntParameter { value = 256 };
        [Range(0, 256)]
        public IntParameter saturation = new IntParameter { value = 256 };
        [Range(0, 256)]
        public IntParameter value = new IntParameter { value = 256 };

        public override bool IsEnabledAndSupported(PostProcessRenderContext context)
        {
            if (enabled.value)
            {
                if (!hsvMode && levels == 256) { return false; }
                return true;
            }

            return false;
        }
#endif
    }
}