# Mooshie Games Common

Mooshie Games Common is a collection of reusable code designed to reduce boilerplate and encourage cleaner, more maintainable Unity projects. It includes various utilities, patterns, and tools to streamline development.

## Installation

1. Copy this Repository's URL
2. Go to Unity
3. At the top, select **Window** -> **Package Manager**
4. In the top left, select the **(+)** Icon
5. Select `Install Package from git URL...`
6. Paste the Repository URL
7. Finally, click **Install**

## Features

- **TagSelector Attribute** – Creates a dropdown in the editor when used on string fields
- **Timers** – Manageable wrappers for tick-based timers, including `CountdownTimer` and `StopWatchTimer`
- **Observables** – Field wrappers allowing for `OnChange` event subscription
- **Singletons** – A `MonoBehaviour`-based Singleton implementation to reduce boilerplate
- **UI MVC** – A basic implementation using inheritance and generics to structure a pseudo-strict MVC pattern
- **Service Locator** – A service provider system that enables dynamic registration of services

## Compatibility

- Requires **Unity 2022.1** or later

## Author

Developed by **Seth H.**  
YouTube: [Code with Seth](https://www.youtube.com/@codewithseth)

## License

This package is free to use and modify. Attribution is appreciated but not required.

---

For a full list of changes, check the [CHANGELOG.md](./CHANGELOG.md).