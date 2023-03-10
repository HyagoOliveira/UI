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

### Create a Menu Data asset

This Scriptable Object is important to reuse the data present on it into other menus.

Create a new **Menu Data** asset by using the Create menu, **ActionCode > UI > Menu Data**.

### Play an Audio when a button is selected

Add [SelectableAudioMenu](/Runtime/Menus/SelectableAudioMenu.cs) component in the GameObject parenting your buttons and
assign a **Menu Data** asset to it. Inside this asset, don't forget to choose a *Selection* AudioClip to play every time a button is selected. 

This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [ISelectable](/Runtime/Interfaces/ISelectable.cs) interface, like *DelayedButton* does.

If you want to add this behavior to other UI component, like a Slider or Text, add the [SelectableTrigger](/Runtime/Triggers/SelectableTrigger.cs)
component to it, which is other implementation of *ISelectable* interface.

### Play an Audio when a button is submitted

Add [SubmitableAudioMenu](/Runtime/Menus/SubmitableAudioMenu.cs) component in the GameObject parenting your buttons and 
assign a **Menu Data** asset to it. Inside this asset, don't forget to choose a *Submition* AudioClip to play every time a button is submitted. 

This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [ISubmitable](/Runtime/Interfaces/ISubmitable.cs) interface, like *DelayedButton* does.

### Select a Button when the Mouse (or other Pointer) hovers it

Normally, a Selectable GameObject (like buttons) is just highlighted when a Pointer hovers it.
For some games it's preferred to select the button instead. You can do it using *HighlightableMenu* component.

Add [HighlightableMenu](/Runtime/Menus/HighlightableMenu.cs) component in the GameObject parenting your buttons.

This GameObject doesn't need to have an AbstractMenu implementation on it but it's necessary that your children buttons has
an implementation of [IHighlightable](/Runtime/Interfaces/IHighlightable.cs) interface, like *DelayedButton* does.

If you want to add this behavior to other UI component, like a Slider or Text, add the [HighlightableTrigger](/Runtime/Triggers/HighlightableTrigger.cs)
component to it, which is other implementation of *IHighlightable* interface.

>**Note**: for a more traditional menu experience, don't forget to disable the *Deselect On Background Click* field 
inside the **Input System UI Input Module** component handling your UI.

### Using all

To use all components above in a single traditional menu, right click on your menu component 
(the one implementing AbstractMenu) and select **Create a Traditional Menu**.

![Create a Traditional Menu](/Docs~/creating-traditional-menu.png "Create a Traditional Menu")

HighlightableMenu, SelectableAudioMenu and SubmitableAudioMenu components will attached to its GameObject.

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