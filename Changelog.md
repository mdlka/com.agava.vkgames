# Changelog  
  
## [2.0.1] - 05.07.2023  
  
### Fixed  
- Now json string formatting in sample doesn't remove spaces.  
  
## [2.0.0] - 04.07.2023  
  
### Added  
- `Storage` class for cloud saves.  
- `groupId` param to `Community.InviteToGroup`.  
  
### Changed  
- `Community.InviteToIJuniorGroup` is renamed to `Community.InviteToGroup`.  
- Renamed some params.  
  
## [1.2.0] - 27.06.2023  
  
### Added  
- `Billing` class for In-App purchases.  
- `RemoteImage` class for downloading images.  
  
## [1.1.2] - 15.05.2023  
  
### Fixed  
- Now `vkBridge` is not loaded in `vkWebAppInit` if it was loaded in index.html  
  
## [1.1.1] - 23.10.2022  
  
### Fixed  
- Invite friends `onRewardedCallback` now working correctly  
  
## [1.1.0] - 23.08.2022  
  
### Added  
- `SocialInteraction.InviteFriends`  
- `Community.InviteToIJuniorGroup`  
  
## [1.0.1] - 12.08.2022  
  
### Fixed  
- Prevented potential double SDK initialization, which would have caused erratic behavior.  
- Improve readability of some errors.  