$unityPath = "C:\Program Files\Unity\Hub\Editor\2020.3.23f1\Editor\Unity.exe"
$unityParameters = "-batchmode -executeMethod SceneSelectorCommandLine.PerformBuild"
Start-Process -FilePath $unityPath -ArgumentList $unityParameters -Wait -PassThru
