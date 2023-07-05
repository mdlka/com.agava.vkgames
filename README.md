# com.agava.vkgames
[Try the SDK demo here.](https://vk.com/app51632637)  

## Installation
  
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/mdlka/com.agava.vkgames.git#2.0.1`  
See minimum required Unity version in the `package.json` file.  
Find "Samples" in the package window and click the "Import" button. Use it as a guide.  
To update the package, simply add it again while using a different version tag.  
  
This is a publishing repo. If you need to create a pull request, use the [Development Repo](https://github.com/mdlka/VKGamesUnity).

## Features of Direct Games

In order for the game to start in Direct Games, you need to initialize vkBridge before starting Unity.  
Add [this](https://github.com/mdlka/TestSDK-VK/blob/main/index.html#L9) and [this](https://github.com/mdlka/TestSDK-VK/blob/main/index.html#L118) to index.html
