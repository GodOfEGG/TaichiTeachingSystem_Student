# TaichiTeachingSystem_Student

## Unity Editor Version
No specific request, but only test on 2022.3.4, 2022.3.22

## Installation

## Assets Hierarchy
```
Assets

#### Customize
├── Animation                       # Animation for Taichi and UI Button
├── Animator Controller             # Animator Controller For Taichi 16 Move and 24 Move
├── Avatar                          # Our 4 custom User Avatar and default Mixamo Bot 
├── Font                            # Font for chinese
├── Images                          # Images for UI and carpet under avatar
├── Material                        # Material For Carpet, UI, and indicators on the keypoints of user avatar (For Play Mode)
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
│   ├── Common                           # Some common structure and functions that used in both student system and teacher system(in other github repo)
│   └── StudentSystem                    # For Student System Only
|   │   |── HttpService                 # For Student System Only
|   |   │   |── HttpService                 # For Student System Only
|   |   │   └── HttpService                 # For Student System Only
|   │   |── Manager                 # For Student System Only
|   │   |── System                 # For Student System Only
|   │   |── Tools                 # For Student System Only
|   │   └── UI                 # For Student System Only
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
│   |   └── RaycastPlane                 # For XR Controller Interaction
├── Mocopi                               # The mocopi receiver that specify the port and avatar 
│   ├── OVRCameraRigInteraction
│   └── Passthrough
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
│   |   └── RaycastPlane                 # For XR Controller Interaction
├── Mocopi                               # The mocopi receiver that specify the port and avatar 
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── XR                                   # Standard XR Rig with Passthrough
│   ├── OVRCameraRigInteraction
│   └── Passthrough
├── SceneLoader                          # For switching to LoginSceneForDemo (logout)
└── EventSystem
```

## Build