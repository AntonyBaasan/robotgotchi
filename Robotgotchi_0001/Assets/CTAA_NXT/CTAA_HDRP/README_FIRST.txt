HOW TO SETUP CTAA V3 HDRP IN A NEW HDRP PROJECT

1) Select the Unity Temporal Anti-Aliasing (TAA) Option in your cameras Anti-Aliasing section dropdown menu

2) Go to the HDRP Default Settings from the Edit->Project Settings

TURN OFF USE RENDER GRAPH!!! This is only for HDRP 10.2 and above. For any unity version below HDRP 10.2 this option is not there and not required

3) Under Rendering Options TURN OFF Anti-Aliasing , just uncheck (you will see your game view start to jitter after doing this)

4) Scroll down all the way to the bottom of Default HDRP Settings window (Custom Post Process Orders section) and Add CTAA_HDRP to the ' Before Post Process ' by clicking the + (plus ) icon, That's it! CTAA is active and working

5) You can now add CTAA to your Post Process Volume to tweak it's parameters. Just go to your Post process Volume in the Hierarchy (or create a global Volume if you do not have one) and click ' Add Override' -> Post-Processing -> Custom -> CTAA

////////////////////////////////////////////////////////////////////////////////////////////

TWEAK CTAA

Temporal Stability - This adjusts the number of previous frames to Blend with the current Frame, the higher the number, there will be less jitter/shimmer and better quality however more Ghosting. Values between 7-12 are good for almost all scenes, the default is 10

HDR Response  - This value sets the CTAA response to High Dynamic Range information in the scene the default value of 2 is great for most scenes. If there is not much Dynamic variation in the scene you can lower this value for sharper detail

Edge Response  - This rounds the corners of sharp details and adds extra aliasing on geometric edges, higher values will blur the entire scene, ONLY increase if required

Enable Anti Flick  - WARNING!! This should ONLY be used for scenes where there is only camera motion and NO dynamic/moving objects in the scene as it will have increased Ghosting. This Option will Completely Eliminate any micro subpixel shimmer, Great for Architectural, design and product visualisation scenes.