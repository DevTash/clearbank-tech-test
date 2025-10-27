# ClearBank Developer Test - Refactoring

- [.NET 8+ (SDK 8.0.415 | Runtime 8.0.21)](https://dotnet.microsoft.com/en-us/download)
## Get Started
Clone repo or use a [GitHub Codespace](https://github.com/features/codespaces)

```
TERMINAL
------------
dotnet build
dotnet test
```


## Approach
The purpose of this exercise was to refactor existing code to improve testability, readability and maintainability. I focused on changes that would not alter the current behavior of the code.



### Scope of Changes
#### Completed
- [X] **Add unit tests** - To validate behavior has not changed during refactoring
- [X] **Strongly typed app settings** - IOptions, single source of truth, improves testability
- [X] **AccountDataStoreFactory** - Decouple service from datastore, extensible, remove if/else logic
- [X] **PaymentScheme request validators** - Remove if/else logic, duplication
- [X] **Payment scheme request validator factory** - Decouple the service from the validators, extensible, aid testing
- [X] **Add AccountService** - PaymentService doesn't need to know about the account datastore + removes debiting logic

#### Todo
- [ ] **Error handling w/o changing current behavior**
- [ ] **Logging**
- [ ] **Metric**
- [ ] **Dependency Injection**
- [ ] **Component tests**
- [ ] **Integration tests**