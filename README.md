# Sequence Diagrams
## Eventhub_Flow_Notification.cs
```mermaid
sequenceDiagram
    participant EH as Event Hub
    participant AF as Azure Function
    participant Config as ConfigurationBuilder
    participant Log as ILogger
    participant HC as HttpClient
    participant Flow as Microsoft Flow API

    EH->>AF: Trigger with EventData[]
    activate AF
    AF->>Config: Build Configuration
    Config-->>AF: Return Configuration
    loop For each EventData
        AF->>AF: Process message
        AF->>Log: Log message information
        AF->>HC: Create HttpClient
        AF->>HC: Create StringContent
        HC->>Flow: POST request
        Flow-->>HC: HTTP Response
        HC-->>AF: Return response
        AF->>AF: EnsureSuccessStatusCode
    end
    AF->>AF: Handle exceptions
    deactivate AF

```
***
## h_2_Cosmo.cs 
```mermaid
sequenceDiagram
    participant EventHub as Event Hub
    participant Function as Eh_2_Cosmo Function
    participant Logger as ILogger

    EventHub->>Function: Trigger EventData[]
    Function->>Function: Iterate through EventData[]
    alt Process EventData
        Function->>Function: Decode messageBody
        Function->>Logger: Log messageBody
        Function->>Function: Yield control
    else Exception occurs
        Function->>Function: Capture exception
    end
    Function->>Function: Check for exceptions
    alt Exceptions exist
        alt Multiple exceptions
            Function->>Function: Throw AggregateException
        else Single exception
            Function->>Function: Throw single exception
        end
    end
```
***
## Eh_Alert.cs
```mermaid
sequenceDiagram
    participant EH as Event Hub
    participant F as Eh_Alert Function
    participant C as Configuration
    participant L as Logger
    participant HC as HttpClient
    participant PBI as Power BI API

    EH->>F: Trigger with EventData[]
    F->>C: Load configuration from local.settings.json
    F->>F: Iterate over EventData[]
    F->>L: Log message body
    F->>F: Prepare payload for Power BI
    F->>HC: Create HttpClient
    F->>HC: Send POST request to Power BI API
    HC->>PBI: POST payload
    PBI-->>HC: Response
    HC-->>F: Ensure success
    F->>F: Handle exceptions
    F-->>EH: Complete processing
```
***
## Eventhub_Built_in_IoTHub.cs
```mermaid
sequenceDiagram
    participant EventHub
    participant FunctionApp
    participant PowerBI
    participant HttpClient

    EventHub->>FunctionApp: Trigger EventHub with EventData[]
    FunctionApp->>FunctionApp: Initialize exceptions list
    FunctionApp->>FunctionApp: Load configuration
    FunctionApp->>FunctionApp: Get PowerBI API URL

    loop Process each EventData
        FunctionApp->>FunctionApp: Extract messageBody
        FunctionApp->>FunctionApp: Log messageBody
        FunctionApp->>FunctionApp: Format payload for PowerBI

        HttpClient->>PowerBI: Post payload
        PowerBI-->>HttpClient: Response
        HttpClient->>FunctionApp: Ensure success
    end

    FunctionApp->>FunctionApp: Check for exceptions
    alt Multiple exceptions
        FunctionApp->>FunctionApp: Throw AggregateException
    else Single exception
        FunctionApp->>FunctionApp: Throw single exception
    end
```
***
## Program.cs
This diagram shows the sequence of actions when the program starts. The HostBuilder is configured with FunctionsWorker defaults, then it builds a Host. The Host is then run.
```mermaid
sequenceDiagram
    participant HostBuilder
    participant FunctionsWorker
    participant Host

    HostBuilder->>FunctionsWorker: ConfigureFunctionsWorkerDefaults()
    FunctionsWorker-->>HostBuilder: Configuration Complete
    HostBuilder->>Host: Build()
    Host-->>HostBuilder: Host Created
    Host->>Host: Run()
```
***