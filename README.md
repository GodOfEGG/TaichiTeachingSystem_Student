# TaichiTeachingSystem_Student

## Unity Editor Version
No specific request, but only test on 2022.3.4, 2022.3.22

## Installation

1. Download github repo
    ```
    git clone https://github.com/GodOfEGG/TaichiTeachingSystem_Student.git
    ```

2. Open unity hub -> Projects -> Add -> Add project from disk -> select the github repo

3. After the unity editor is open, click File -> Build Settings -> Android -> Install with Unity Hub (if "No Android module loaded") -> Restart the editor and back to Build Settings -> Switch Platform

4. Now it's ready to run

## Assets Hierarchy
```
Assets

#### Customize
├── Animation                       # Animation for Taichi and UI Button
├── Animator Controller             # Animator Controller For Taichi 16 Move and 24 Move
├── Avatar                          # Our 4 custom User Avatar and default Mixamo Bot 
├── Font                            # Font for chinese (not for all the chinese character, but only the characters specified in the font asset )
├── Images                          # Images for UI and carpet under avatar
├── Material                        # Material For Carpet, UI, and indicators on the joint of user avatar (For Play Mode)
├── Scenes                          
├── Scripts

#### Default or comes with installed Packages
├── MocopiReceiver                  # Mocopi Default
├── MRTemplateAssets                # XR Default
├── Oculus                          # XR Default
├── Plugins                         # XR Default
├── ProBuilder Data                 # ProBuilder Default
├── Rendering                       # URP Renderer
├── Resources                       # Unity Default
├── Samples                         # XR Default
├── Settings                        # For URP Rendering
├── StreamingAssets                 # Unity Default
├── TextMesh Pro                    # TextMeshPro Default
├── UI Toolkit                      # UI Default
├── XR                              # XR Default
└── XRI                             # XR Default
```

```
Script
├── TaichiTeachingSystem
│   ├── Common                              # Some common structure and functions that used in both student system and teacher system(in other github repo)
│   └── StudentSystem                       # For Student System Only
|   │   |── HttpService                     # For Http Related Scripts
|   |   │   |── HttpService                 # Functions including upload, download and authentication
|   |   │   |── Request                     # Some request structure
|   |   │   └── Response                    # Some response structure
|   │   |── Manager                         # Managers for the resources in the scene like avatars, indicator, login info
|   |   │   |── AvatarManager               # For managing user avatar creation, position, actions, active (For regular version)
|   |   │   |── AvatarManagerForDemo        # For managing user avatar creation, position, actions, active (For demo version)
|   |   │   |── CoachManager                # For managing coach avatar creation, position, actions, active (For regular version)
|   |   │   |── CoachManagerForDemo         # For managing coach avatar creation, position, actions, active (For demo version)
|   |   │   |── IndicatorManager            # For managing indicator that shows avatar joints are modified by teacher or not
|   |   │   └── LoginManager                # Managing login, account info
|   │   |── System                          # Main scripts that control the main process
|   |   │   |── DemoMode                    # Main script for demo (For demo version)
|   |   │   |── PlayMode                    # Main script for play mode (For regular version)
|   |   │   |── RecordMode                  # Main script for record mode (For regular version)
|   |   │   └── StudentTaichiSystem         # Script that control the mode switch between play mode and record mode (For regular version)
|   │   |── Tools                           # Other tools
|   |   │   |── AvatarSelection             # For avatar selection in login scene (For regular version)
|   |   │   |── AvatarSelectionForDemo      # For avatar selection in login scene (For demo version)
|   |   │   |── FaceCamera                  # For name tag of each avatar to face the camera(XR rig)
|   |   │   |── FloorFollow                 # For floor(carpet) under each avatar that follows the avatar movement
|   |   │   |── Indicators                  # Structures of indicators
|   |   │   |── ResetAvatar                 # For animator controller to reset the coach move when switching the start move or end move
|   |   │   |── SceneLoader                 # Switch between LoginScene and MainScene
|   |   │   |── SceneLoaderForDemo          # Switch between LoginSceneForDemo and MainSceneForDemo
|   |   │   └── ShowPanelsButton            # Show or hide the UI panel
|   │   └── UI                              # For controlling UI appearence, interactive, and transfering the info of UI 
|   |   │   |── CoachPanelManager           # For Coach Panel in MainScene/MainSceneForDemo
|   |   │   |── CreateUserPanelManager      # For create user account panel in LoginScene
|   |   │   |── FramePanelManager           # For Frame Panel in MainScene
|   |   │   |── IPPanelManager              # For IP Panel in LoginScene
|   |   │   |── LoginPanelManager           # For Login Panel in LoginScene
|   |   │   |── ModePanelManager            # For mode panel in MainScene/MainSceneForDemo
|   |   │   |── PlayPanelManager            # For play mode panel in MainScene
|   |   │   |── RecordPanelManager          # For record mode panel in MainScene
|   |   │   └── StudentPanelManager         # For student avatar panel in MainScene/MainSceneForDemo
```

## Scene Hierarchy
### LoginSceneForDemo
```
├── Directional Light
├── XR                                   # Standard XR Rig with Passthrough
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── AvatarSelection                      # For Selecting the User Avatar
│   ├── Plateform                        # The platform under Avatar
│   ├── XRAvatar_f_01                    # The four user avatar options
│   ├── XRAvatar_f_02
│   ├── XRAvatar_m_01
│   ├── XRAvatar_m_02
│   └── Canvas                           # UI for buttons and panels
├── SceneLoader                          # For switching to MainSceneForDemo
└── EventSystem
```

### MainSceneForDemo
```
├── Directional Light
├── DemoMode                             # The main object that control the process
├── AvatarManager                        # Manage Avatars
│   ├── Avatars                          # The user Avatars and coach Avatars
│   ├── AvatarPos                        # Position of each avatars 
│   ├── AvatarOptions                    # The 4 custom avatars(for runtime avatar creation)
│   └── AvatarNameTag                    # The name tag of each avatar(for runtime creation)
├── UI
│   ├── Canvas                           
│   |   ├── Panels                       
│   |   |   ├── CoachPanel
│   |   |   ├── StudentPanel 
│   |   |   ├── Play                     # The Play Button               
│   |   ├── LogoutButton
│   |   ├── ShowPanelButton              # The button that show and hide Panels
│   |   └── RaycastPlane                 # Plane for XR Controller Interaction
├── Mocopi                               
│   └── MocopiSimpleReceiver             # The mocopi receiver that specify the port and avatar 
├── XR                                   # Standard XR Rig with Passthrough
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── SceneLoader                          # For switching to LoginSceneForDemo (logout)
└── EventSystem
```

### LoginScene
```
├── Directional Light
├── XR                                   # Standard XR Rig with Passthrough
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── Login                                
│   ├── Canvas                           
│   |   ├── Panels                       
│   |   |   ├── LoginPanel               
│   |   |   ├── CreateUserPanel
│   |   |   └── IPPanel
│   |   └── RaycastPlane                 # Plane for XR Controller Interaction
├── AvatarSelection                      # For Selecting the User Avatar
│   ├── Plateform                        # The platform under Avatar
│   ├── XRAvatar_f_01                    # The four user avatar options
│   ├── XRAvatar_f_02
│   ├── XRAvatar_m_01
│   ├── XRAvatar_m_02
│   └── Canvas                           # UI for buttons and panels
├── SceneLoader                          # For switching to MainSceneForDemo
└── EventSystem
```

### MainScene
```
├── Directional Light
├── StudentTaichiSystem                  # Main script that control the process => switch between play mode and record mode
│   ├── RecordMode                       # Main script that control record mode
│   └── PlayMode                         # Main script thta control play mode
├── AvatarManager                        # Manage Avatars
│   ├── Avatars                          # The user Avatars and coach Avatars
│   ├── AvatarPos                        # Position of each avatars 
│   ├── AvatarOptions                    # The 4 custom avatars(for runtime avatar creation)
│   └── AvatarNameTag                    # The name tag of each avatar(for runtime creation)
├── UI
│   ├── Canvas                           
│   |   ├── Panels
│   |   |   ├── FramePanel                   
│   |   |   ├── CoachPanel
│   |   |   ├── StudentPanel
│   |   |   ├── ModePanel
│   |   |   ├── RecordPanel
│   |   |   └── PlayPanel           
│   |   ├── LogoutButton
│   |   ├── ShowPanelButton              # The button that show and hide Panels
│   |   └── RaycastPlane                 # For XR Controller Interaction
├── IndicatorManager                     # Manager the indicators in play mode that shows the modified joints 
│   ├── OriginIndicatorList              # The indicators on the origin avatars(avatars with original move)
│   |   ├── Indicators_Front
│   |   ├── Indicators_Left
│   |   ├── ......
│   └── ModifyIndicatorList              # The indicators on the modify avatars(avatars with modified move)
│   |   ├── Indicators_Front
│   |   ├── Indicators_Left
│   |   ├── ......
├── Mocopi                               
│   └── MocopiSimpleReceiver             # The mocopi receiver that specify the port and avatar 
├── XR                                   # Standard XR Rig with Passthrough
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── SceneLoader                          # For switching to LoginSceneForDemo (logout)
└── EventSystem
```

## Build
1. Use type C cable to connect Quest 3 to PC (有傳輸速度要求，ipad充電線不行，可以用quest3附帶的線配合USB轉接頭，也可以用5Gbps type C to USB傳輸線)
2. Put on Quest 3 helmet, it should pop out a notification "Allow USB Debugging", click "allow". (If you didn't see the notification, try plug out and re-plug in the type C cable )
3. Click file -> Build Settings ->  Android -> Run Device -> "Oculus Quest 3" (click "Refresh" and the option should appear)
4. For regular version, choose "Scenes/LoginScene   0" and "Scenes/MainScene   1" in the "Scenes in Build" windows in "Build Settings". For demo version, choose "Scenes/LoginSceneForDemo    0" and "Scenes/MainSceneForDemo    1".
5. Click "Build and Run" to build and run the project in quest 3
