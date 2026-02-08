# News & Weather App

## Overview
This is a distributed web application that provides weather forecasts, local news, and solar intensity data. It features a dual-interface system with separate portals for **Members** (end-users) and **Staff** (administrators).

The application is built using a **Service-Oriented Architecture (SOA)**, where the frontend (ASP.NET Web Forms) communicates with backend logic exposed via **WCF Services**.

## Tech Stack
- **Frontend**: ASP.NET Web Forms (.NET Framework 4.8)
- **Backend Services**: WCF (Windows Communication Foundation)
- **Language**: C#
- **Data Storage**: XML (File-based persistence for Users and Staff)
- **External APIs**:
    - **OpenWeatherMap**: 5-day weather forecasts.
    - **NewsAPI**: News search and headlines.
    - **NREL (National Renewable Energy Laboratory)**: Solar intensity data.
    - **RapidAPI**:
        - `us-zip-code-information`: location data.
        - `geocoding-by-api-ninjas`: reverse geocoding.
        - `ip-geo-location`: current location detection.
        - `news-api14`: news aggregation.

## Project Structure
The solution consists of three main projects:

1.  **`Weather` (Frontend)**:
    -   **Type**: ASP.NET Web Application.
    -   **Purpose**: The user-facing web interface.
    -   **Key Pages**:
        -   `Default.aspx`: Public landing page, Login, and Signup.
        -   `Member.aspx`: Main dashboard for logged-in members (Weather, News, Solar).
        -   `Staff.aspx`: Staff login portal.
        -   `StaffPage.aspx`: Main dashboard for staff management.
        -   `StaffViewRecords.aspx`: Page for staff to view all Member and Staff credentials/roles.
    -   **Configuration**:
        -   `Web.config`: Contains service endpoints pointing to `http://webstrar5.fulton.asu.edu/...`.
        -   `Global.asax`: Registers Routes (FriendlyUrls) and Bundles.
    -   **Dependencies**: Uses `Connected Services` to generate proxies for `CombinedServices` and `Services`.

2.  **`CombinedServices` (Backend Service)**:
    -   **Type**: WCF Service Library.
    -   **Purpose**: Handles core business logic for Weather and Staff management.
    -   **Key Operations**:
        -   `Weather5day`: Fetches 5-day forecast from OpenWeatherMap.
        -   `StaffValidation`: Authenticates staff credentials against `Staff.xml`.
        -   `CreateStaffMember` / `RemoveStaffMember`: Manages staff accounts.

3.  **`Services` (Backend Service)**:
    -   **Type**: WCF Service Library.
    -   **Purpose**: Handles Member management and utility services.
    -   **Key Operations**:
        -   `SignUp` / `Login`: Authenticates members against `Member.xml`.
        -   `GetNews`: Fetches news from NewsAPI.
        -   `SolarIntensity`: Fetches solar data from NREL.
        -   `GenerateCaptchaImage` / `GetText`: CAPTCHA generation and validation.
        -   `GetLocationInfo`: Resolves Zip Codes/Cities to Lat/Lon.

## Features

### Public / Member Features
*   **Authentication**: Secure Signup and Login with SHA256 password hashing.
*   **CAPTCHA**: Image-based CAPTCHA protection for forms.
*   **Weather Dashboard**:
    *   Automatic location detection (IP-based).
    *   5-day weather forecast with icons.
    *   Search by Zip Code, City, or Latitude/Longitude.
    *   Support for multiple units (Imperial, Metric, Standard).
*   **Local News**: Fetches news articles relevant to the current or searched location.
*   **Solar Data**: Provides solar intensity data for the location.

### Staff Features
*   **Staff Portal**: Dedicated login for administrators.
*   **User Management**: create and remove staff accounts.
*   **Data Access**: View all registered Members and Staff via `StaffViewRecords.aspx`.

## Configuration & Data
*   **Data Persistence**:
    *   `Services/Member.xml`: Stores Member credentials.
    *   `Weather/App_Data/Staff.xml`: Stores Staff credentials.
*   **Deployment Specifics**:
    > [!WARNING]
    > **Hardcoded Paths**: The application contains hardcoded paths specific to the `webstrar5.fulton.asu.edu` deployment environment.
    > - `Weather/Web.config`: Service endpoints are absolute URLs to `webstrar5`.
    > - `Weather/StaffViewRecords.aspx.cs`: Uses absolute mapped paths like `~/Page2/Member.xml` and `~/Page9/App_Data/Staff.xml`. These **must be updated** if deploying to a different environment or functionality will break.
*   **API Keys**: API keys are currently hardcoded in the service files (`Service1.svc.cs` and `Services.svc.cs`).

---

## 👤 Author

**Sumit Patel**
*   [LinkedIn](https://linkedin.com/in/sumit4183)
*   [GitHub](https://github.com/sumit4183)
*   [Portfolio](https://sumitp.netlify.app)