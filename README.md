# Mobile QA Automation — Weather App

A test automation framework for the mobile Weather App.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Architecture and Project Structure](#architecture-and-project-structure)
- [Design Patterns and SOLID](#design-patterns-and-solid)
- [Configuration](#configuration)
- [Setup and Execution](#setup-and-execution)
- [Test User Setup](#test-user-setup)
- [Test Scenarios](#test-scenarios)
- [Logging](#logging)
- [Reporting (ReportPortal)](#reporting-reportportal)
- [CI/CD](#cicd)
- [Security](#security)
- [Known Limitations](#known-limitations)

---

## Tech Stack

| Technology | Purpose |
|---|---|
| **C# / .NET 7** | Core language and platform for the framework |
| **Appium.WebDriver** | Mobile app automation (Android/iOS) |
| **NUnit 3** | Test framework, parallelization, reporting |
| **Serilog** | Structured logging (file + console) |
| **ReportPortal** | Test result visualization and storage |
| **GitHub Actions** | CI/CD nightly scheduled test runs |


---

## Architecture and Project Structure
```
App.Automation/
├── App/        # APK file of the app under test
├── Config/     # Configuration
├── Drivers/    # Appium driver factory
├── Pages/      # Page Object classes (in progress)
├── Tests/
│   ├── Base/   # Base test class
│   └── *.cs    # Test scenarios
├── Utils/
│   └── Logger/  # Logging + step-logging for reports
└── README.md
```
---

## Design Patterns

### Abstract Factory (instead of Singleton)

Driver creation is implemented through `IDriverFactory`, with two implementations — `AndroidDriverFactory` and `IosDriverFactory`. The concrete factory is selected in `DriverManager` based on the `Platform` value in the config.

**Why not Singleton:** Using a Singleton for the driver is a common but problematic practice in mobile automation. It doesn't play well with parallel test execution. Instead, the driver is created via a factory and stored in a `ThreadLocal` (see `DriverManager`) — each parallel thread gets its own independent instance, and adding a new platform.


### Page Object Model

Used to separate UI interaction logic from the test scenarios themselves — tests call page methods instead of working with raw locators directly.

`(TODO — to be filled in)`

---

## Configuration

All framework parameters (platform, capabilities, timeouts, paths, logging) are kept in a single file, `Config/appsettings.json` — no values are hardcoded in the code. This is intentional: a single source of truth simplifies maintenance and allows changing framework behavior without recompiling — just by editing the JSON.

The configuration supports both platforms simultaneously — `Android` and `iOS` sections exist side by side in `appsettings.json`, and the active platform is switched with a single `Platform` field.

**Note:** iOS settings (device, platform version, bundle ID) are fully defined in the config, and the code is ready to work with the iOS platform. However, **it is not possible to actually run and test an iOS scenario as part of this assignment** — only an APK file built exclusively for Android was provided. It cannot be installed or run on iPhone/iOS. iOS support in the framework is an architectural provision for the future, not a verified working scenario.

In CI (GitHub Actions), the platform is overridden via the `AppSettings__Platform` environment variable, whose value comes from the `TEST_PLATFORM` GitHub Secret — allowing the platform to be controlled centrally without touching the committed `appsettings.json`.

---

## Setup and Execution

### Requirements
- .NET 7 SDK
- Node.js 20+ and Appium (`npm install -g appium && appium driver install uiautomator2`)
- Android SDK + emulator (or a connected real device)

### Install dependencies
```bash
cd App.Automation
dotnet restore
```

### Start the Appium server
```bash
appium
```

### Start the emulator
```bash
emulator -avd <avd_name>
```

### Run tests
```bash
dotnet test --settings Tests/Base/.runsettings
```

---

## Test User Setup

*(TODO — to be filled in)*

---

## Test Scenarios

*(TODO — to be filled in)*

---

## Logging

Implemented via Serilog, hidden behind the `ITestLogger` abstraction — the logging library itself can be swapped out without changing the tests (DIP).

Additionally, **step-logging** is implemented (`StepLogger`) — every meaningful test step is logged as `STEP: ...` / `STEP PASSED: ...` / `STEP FAILED: ...`, making the ReportPortal output readable and structured without needing a separate step-reporting library.

---

## Reporting (ReportPortal)

**ReportPortal** was chosen for reporting (the public free demo server, demo.reportportal.io).

**Why ReportPortal instead of Allure:** Allure was considered as an alternative and would likely have been a better fit for storing test cases and test scenarios separately from run reports. However, a free self-hosted deployment of Allure TestOps (with a web dashboard, rather than just a static HTML report) wasn't feasible, while free access to ReportPortal was already available through its public demo server — with full dashboards.

⚠️ **Public demo server limitation:** data is periodically flushed entirely. This is not meant for long-term storage of results, only for demonstrating/testing the integration.

---

## CI/CD

Configured via **GitHub Actions** (`.github/workflows/nightly-tests.yml`):

- **Schedule** — runs automatically every day (a manual run via `workflow_dispatch` is also available)
- **Platform** — Ubuntu runner with hardware acceleration for stable Android emulator performance
- Installs: .NET, Node.js, Appium + UiAutomator2 driver
- Results are automatically sent to ReportPortal with `platform`, `mobile`, and `ci` attributes — allowing nightly CI runs to be filtered separately from local runs

---

## Security

- **All sensitive data (ReportPortal API token, test platform) is stored in GitHub Secrets**, not in code or repository configuration files. In the workflow, they're passed via `${{ secrets.* }}` and injected into the configuration at runtime.
- The local `ReportPortal.config.json` file with real values used for development follows a "secret out of code" pattern via `.gitignore` where applicable; a template with placeholders is used for the committed version.
- The application's APK file is included in the repository for the reviewer's convenience when running tests.

---

## Known Limitations

- iOS configuration is architecturally prepared but untested in real conditions — only an Android APK was available.
- ReportPortal (public demo) periodically flushes its history — long-term report storage in production would require a self-hosted instance.
- Page Object classes (`Pages/`) are still in progress — current tests log steps via `StepLogger`, but actual UI interaction is yet to be implemented.

