# ContactsAPI

ContactsAPI is an ASP.NET Web API (.NET Framework 4.7.2) backend service that provides RESTful endpoints to manage and query a corporate contacts directory stored in an Azure SQL Database. The API supports paginated and filtered queries, full contact retrieval, and Excel export functionality using ClosedXML. It is designed to serve as the data layer for a frontend contacts management application, exposing structured JSON responses and secure database access via ADO.NET.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Web API (.NET Framework 4.7.2) |
| Language | C# 7.3 |
| Database | Azure SQL Database |
| Data Access | ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`) |
| Excel Export | [ClosedXML](https://github.com/ClosedXML/ClosedXML) v0.95.4 |
| JSON | Newtonsoft.Json |
| CORS | Microsoft.AspNet.WebApi.Cors |

---

## 📁 Project Structure

```
ContactsAPI/
├── App_Start/
│   └── WebApiConfig.cs         # CORS and routing configuration
├── Controllers/
│   └── ContactsController.cs   # API endpoints
├── DataAccess/
│   ├── ContactsDataAccessQry.cs # Query builder
│   └── ContactsQryExecute.cs   # ADO.NET execution layer
├── Data/
│   ├── ContactsData.cs
│   └── ContactRepository.cs
├── Models/
│   ├── ContactsModelRequest.cs  # Request/filter model
│   ├── ContactsModelResponse.cs # Response model
│   ├── ContactsCountModelResponse.cs
│   ├── ExportColumnsFromUIModel.cs
│   └── Pagination.cs
└── Web.config                   # Connection string (not committed)
```

---

## 🔌 API Endpoints

### `POST /api/Contacts/GetAllContacts`
Returns the full list of contacts without filters.

**Response:** `200 OK` — Array of contact objects.

---

### `POST /api/Contacts/GetContacts`
Returns a paginated and filtered list of contacts.

**Request Body:**
```json
{
  "contactId": "",
  "plant": "PLANT1",
  "location": "",
  "function": "",
  "contactName": "",
  "phoneWork": "",
  "email": "",
  "sortBy": "CONTACT_ID",
  "descending": false,
  "initalRow": 0,
  "rowsPerPage": 10
}
```

**Response:** `200 OK` — Paginated contact list.

---

### `POST /api/Contacts/ExportContacts`
Exports filtered contacts to an `.xlsx` Excel file.

**Request Body:** Same as `GetContacts`, plus:
```json
{
  "EXPORT_COLUMNS": "[{\"field\":\"contactId\",\"label\":\"Contact ID\"}, ...]"
}
```

**Response:** `200 OK` — Binary `.xlsx` file download (`Contacts.xlsx`).

---

## ⚙️ Configuration

### Connection String
The database connection string is stored in `Web.config` under `<connectionStrings>` and is **not committed to source control**.

Copy the template below into your local `Web.config`:

```xml
<connectionStrings>
  <add name="ContactsDB"
	   connectionString="Server=tcp:{server}.database.windows.net,1433;Initial Catalog={database};User ID={user};Password={password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
	   providerName="System.Data.SqlClient" />
</connectionStrings>
```

### CORS
CORS is globally enabled in `App_Start/WebApiConfig.cs`. For production, replace `"*"` with your specific frontend origin:

```csharp
var cors = new EnableCorsAttribute("https://your-frontend-domain.com", "*", "*");
config.EnableCors(cors);
```

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- Access to an Azure SQL Database

### Setup

1. **Clone the repository**
   ```powershell
   git clone https://github.com/Jorgeaapaz/ContactsAPI.git
   cd ContactsAPI
   ```

2. **Restore NuGet packages**
   Open the solution in Visual Studio — packages restore automatically. Or run:
   ```powershell
   nuget restore ContactsAPI.sln
   ```

3. **Configure the connection string**
   Add the `<connectionStrings>` entry to `Web.config` (see [Configuration](#️-configuration) above).

4. **Run the project**
   Press **F5** in Visual Studio (IIS Express).

---

## 📦 NuGet Packages

| Package | Purpose |
|---|---|
| `Microsoft.AspNet.WebApi.Cors` | CORS support |
| `ClosedXML` | Excel file generation |
| `Newtonsoft.Json` | JSON serialization |
| `System.Configuration.ConfigurationManager` | Connection string access |

---

## 🔒 Security Notes

- `Web.config` is excluded from Git via `.gitignore` to prevent leaking credentials.
- Use `Web.Release.config` transforms or Azure App Service configuration for production secrets.
- Consider migrating to **Azure Key Vault** for connection string management in production.

---

## 📄 License

This project is for demonstration purposes.
