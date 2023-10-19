	# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- Rect Sprite
- DefaultUI UI Material

## [4.1.1] - 2023-03-22
### Fixed
- Add missing #if UNITY_EDITOR

## [4.1.0] - 2023-03-21
### Added
- GradientSlider component

## [4.0.0] - 2023-03-16
### Added
- ISelectable interface
- ISubmitable interface
- IHighlightable interface
- SelectableAudioMenu component
- SubmitableAudioMenu component
- HighlightableMenu component
- MenuData Scriptable Object
- AbstractMenu component
- AudibleSlider component
- HighlightableTrigger component
- SelectableTrigger component
- FirstGameObjectSelector component
- OnShown and OnHidden events into CanvasViewer
- Close Button property into Popup component
- ConfirmationPopup component

### Changed
- DelayedButton implements ISelectable, ISubmitable and IHighlightable interfaces
- DelayedButton waits for seconds in realtime
- Remove AudiSource and SelectableGroups from Popup component
- Unsealed Popup component
- CanvasViewer enables/disables GameObject when Visible changes

### Removed
- MenuEventTrigger component
- BaseMenu component

## [3.0.0] - 2021-12-26
### Changed
- BaseMenu: Replace Buttons by MenuEventTriggers

## [2.0.0] - 2021-08-11
### Added
- BaseMenu component
- MenuEventTrigger component

### Removed
- ButtonToggle component. Not necessary once UnityEngine.Button (with Sprite Swap Transition enabled) do just it
- AudibleButton component. Improved by BaseMenu

## [1.1.0] - 2020-12-28
### Added
- DelayedButton component
- AudibleButton component

## [1.0.1] - 2020-12-24
### Changed
- Fix ButtonToggle Reset function

## [1.0.0] - 2020-12-23
### Added
- Selectable Group component
- Popup component
- ButtonToggle component
- Add UI components
- Add CHANGELOG
- Add README
- Add initial files
- Initial commit


[Unreleased]: https://github.com/HyagoOliveira/UI/compare/4.1.1...main
[4.1.1]: https://github.com/HyagoOliveira/UI/tree/4.1.1/
[4.1.0]: https://github.com/HyagoOliveira/UI/tree/4.1.0/
[4.0.0]: https://github.com/HyagoOliveira/UI/tree/4.0.0/
[3.0.0]: https://github.com/HyagoOliveira/UI/tree/3.0.0/
[2.0.0]: https://github.com/HyagoOliveira/UI/tree/2.0.0/
[1.0.1]: https://github.com/HyagoOliveira/UI/tree/1.0.1/
[1.0.0]: https://github.com/HyagoOliveira/UI/tree/1.0.0/