using UnityEditor;

public class SwitchPlatform {

    [MenuItem("Platform/Standalone Windows 64")]
    public static void PerformToWindows64() {
        // Switch to Windows standalone build.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Platform/WebGL")]
    public static void PerformToWebGL() {
        // Switch to Windows standalone build.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
    }
}
