------------------------------------------------------------------------------------------
                                      Introduction
------------------------------------------------------------------------------------------

VSTOContrib.SimpleInjector allows you to use SimpleInjector to resolve your view models



------------------------------------------------------------------------------------------
                                    Usage Instructions
------------------------------------------------------------------------------------------

1. Open your ThisAddIn.cs file
2. Go to the CreateRibbonExtensibilityObject() method
3. Replace `new <OFFICEAPP>RibbonFactory(new DefaultViewModelFactory(), ...)` with
           `new <OFFICEAPP>RibbonFactory(new SimpleInjectorViewModelFactory(container), ...)` or
           `new <OFFICEAPP>RibbonFactory(new SimpleInjectorViewModelFactory(new AddinSimpleInjectorModule()), ...)`
4. In your container registration register your ribbon view models with
           `builder.RegisterRibbonViewModels(typeof(AddinModule).Assembly);` (exists in the VSTOContrib.SimpleInjector namespace)
   

VSTOContrib will now create a lifetime scope for each viewmodel it resolves, and dispose the lifetime scope when the viewmodel is cleaned up