using UnityEditor;
class SceneSelectorCommandLine
{
    static void PerformBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] {
            "Scenes/ui/main_menu_v2.unity",
            "Scenes/game_master/game_master.unity",
        };
        buildPlayerOptions.locationPathName = "/dist/webgl";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None; // set whatever you want here
        BuildPipeline.BuildPlayer(buildPlayerOptions);  // apply the setting changes
    }
}