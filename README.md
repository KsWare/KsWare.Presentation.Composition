# KsWare.Presentation.Composition

Use [CompositionBuilder](##CompositionBuilder) and nothing else. :-) 

- Supports .Net Framework 4.5+ and .NetCoreApp 3.x
- uses [System.ComponentModel.Composition 4.7+](https://www.nuget.org/packages/System.ComponentModel.Composition/)
- uses [CommonServiceLocator 2.0.5+](https://www.nuget.org/packages/CommonServiceLocator/)

## CompositionBuilder
The easy way:
```csharp
new CompositionBuilder().CreateContainer();
```
The complicated way ;-)
```csharp
new CompositionBuilder())
   .Add(new DirectoryCatalog("."))
   .Add(typeof(SampleService))
   .Add(Assembly.LoadFile("Plugins/Loader.dll"))
   .CreateContainer()
   .Add(new MyExtraPart());
```
and use ServiceLocator
```csharp
ServiceLocator.Current.GetInstance<MyExtraPart>();
```