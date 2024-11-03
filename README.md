# Strangeman.Cysharp
[![GitHub package.json version]](#installation) [![ChangelogBadge]](CHANGELOG.md) [![InstallationBadge]](#installation) [![WikiBadge]][WikiLink]


This package provides an infrastructure layer and default implementation for Cysharp's MessagePipe event system, and implementations for common problems that are solvable with UniTask.

Documentation to follow at a later date.

General Use:
1. Create MessagePipeProvider (context menu/Strangeman/Messaging/PipeProvider) Scriptable Object in a 'Resources' folder. Do not rename.
2. Create classes or structs inheriting from: IMessage interface.
3. Update MessagePipeProvider with created Messages by clicking Add or Add All on Asset.
4. Access Publishing from: GlobalMessagePublisher (publisher.Publish(message))
5. Access Subscribing from: GlobalMessageSubscriber (I do this on awake or initialize equivalent)
6. Enjoy!

## Installation
Install Cysharp Libraries from the following URLS:

Step 1:
```
https://github.com/Cysharp/UniTask
```
Step 2: Make sure you install the 'Core' version!
```
https://github.com/Cysharp/MessagePipe
```

Add to Unity from Package Manager:

Step 1:
```
https://github.com/miclede/Strangeman.Core.git
```
Step 2:
```
https://github.com/miclede/Strangeman.Cysharp.git
```

<!------>
[ChangelogBadge]: https://img.shields.io/badge/Changelog-light
[GitHub package.json version]: https://img.shields.io/github/package-json/v/miclede/Strangeman.Cysharp

[InstallationBadge]: https://img.shields.io/badge/Installation-red
[WikiBadge]: https://img.shields.io/badge/Documentation-purple

[WikiLink]: https://github.com/miclede/Strangeman.Cysharp