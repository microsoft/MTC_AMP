# AMP - Another Mvvm Project
A collection of helper utilities, classes and controls to augment UWP MVVM applications.   Built around the popular MVVMLight Framework, AMP was born from several past MTV projects and Alley demos.  It helps solve common problems like asynchronous view models, visual state management, and commanding with various controls.
Collected from various articles, projects, etc.

## NavAware ViewModels
You may need ViewModels to perform actions when users navigate to the viewmodels respective views.  The *NavAwarePage* and *NavAwareViewModel* can be used to accomplish this.

The *NavAwarePage* base class should be used in place of all XAML *Page* base classes.  The *NavAwarePage* will call the *Activate* method on a *NavAwareViewModel* when navigating to a page, and will call the  *Decactivate* method when navigating away from a *NavAwarePage*.

**To use:**
Implement your own ViewModel inheriting from the NavAwareViewModel base class:
```csharp
public class MyViewModel : NavAwareViewModel
```
Override the *Activate* and/or *Deactivate* methods in  your new class:
```csharp
public override void Activate(object parameter)
{ \\ Do work here}

public override void Deactivate(object parameter)
{ \\ Do work here}
```

The passed in *parameter* value will be the same as whatever state object passed in the Page navigation.

Replace your View's *Page* base class with the *NavAwarePage* class. Your *NavAwareViewModel* must be the *NavAwarePage*'s DataContext in order for the *Activate/Deactive* methods to be called correctly.

## Asychronous ViewModels
Often times, you need to perform actions asynchronously when your viewmodels are loaded.The *AsyncViewModel* is a specific implementation of a *NavAwareViewModel* that allows async operations to be peformed when the ViewModel is initially loaded.  



## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
