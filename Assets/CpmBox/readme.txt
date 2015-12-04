1. Import CPMbox.unitypackage in to your project (Assets->Import Package->Custom Package)

2. Place CpmbManager prefab from CpmbBox folder on your scene.

3. CpmbManager will be aligned and placed in front of your Main Camera.
If ad window too small or too big on your scene, you can select CpmbManager in Hierarchy window and tune it with Camera Distance and/or Ad Scale property in Inspector.
If you have more than one camera in your scene and you want to use some other camera to show ad, you can select CpmbManager in your Hierarchy window and drag needed camera in to the Camera field in Inspector.
When you will finish you can use Hide CPMbox checkbox to hide or show ad window.

4. Add new game in your CPMbox dashboard on cpmbox.com.

5. To show ad you need to add one line of code somewhere in your script:
CpmbManager.showAd("gameID", CallbackFunction, hideAd);
"gameID" - ID of your game from your dashboard
CallbackFunction - function that will be called after ad progress bar will be full.
hideAd - bool value used to hide ad window after progress bar will be full or you can leave it and hide it later with:
CpmbManager.removeAd();