# UI

* Useful UI codes.
* Unity minimum version: **2018.3**
* Current version: **3.0.0**
* License: **MIT**

## How To Use

### Using a Delayed Button

For some games it's preferred to trigger a button *On Click* event only after a delay period of time.

You can do it using the [DelayedButton](/Runtime/Buttons/DelayedButton.cs) component and 
setting the *Delay* property.

### Creating a new Menu

Create and implement a class inheriting from [AbstractMenu](/Runtime/Menus/AbstractMenu.cs). 

All buttons *OnClick* actions should be binded (and unbinded) via code.
It's important that you use the *DelayedButton* instead the default one.

### Play an Audio when a button is selected

Add [SelectableAudioMenu](/Runtime/Menus/SelectableAudioMenu.cs) component in the GameObject parenting your buttons and 
choose a *selection* AudioClip to play every time a button is selected. 

This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [ISelectable](/Runtime/Interfaces/ISelectable.cs) interface, like *DelayedButton* does.

### Play an Audio when a button is submitted

Add [SubmitableAudioMenu](/Runtime/Menus/SubmitableAudioMenu.cs) component in the GameObject parenting your buttons and 
choose a *submition* AudioClip to play every time a button is submitted. 

This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [ISubmitable](/Runtime/Interfaces/ISubmitable.cs) interface, like *DelayedButton* does.

### Select a Button when the Mouse (or other Pointer) hovers it

Normally, a Selectable GameObject (like buttons) is just highlighted when a Pointer hovers it.
For some games it's preferred to select the button instead. You can do it using *SelectWhenHighlight* component.

Add [SelectWhenHighlight](/Runtime/Menus/SelectWhenHighlight.cs) component in the GameObject parenting your buttons.
This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [IHighlightable](/Runtime/Interfaces/IHighlightable.cs) interface, like *DelayedButton* does.

## Installation

### Using the Package Registry Server

Follow the instructions inside [here](https://cutt.ly/ukvj1c8) and the package **ActionCode-UI** 
will be available for you to install using the **Package Manager** windows.

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set. 

- Use the **Package Manager** "Add package from git URL..." feature and paste this URL: `https://github.com/HyagoOliveira/UI.git`

- You can also manually modify you `Packages/manifest.json` file and add this line inside `dependencies` attribute: 

```json
"com.actioncode.ui":"https://github.com/HyagoOliveira/UI.git"
```

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[BitBucket](https://bitbucket.org/HyagoGow/) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>