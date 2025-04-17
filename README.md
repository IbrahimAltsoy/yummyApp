# yummyApp

# 🧾 YummyApplication – Backend & Web

---

## 🇹🇷 Proje Tanımı / 🇬🇧 Project Overview

**🇹🇷 Türkçe:**  
YummyApplication, aynı işletmeyi ziyaret eden kullanıcıların birbirleriyle etkileşime geçmesini sağlayan, konuma dayalı bir sosyal ağ platformudur. Bu repo, mobil uygulama için RESTful API, yönetici paneli ve kullanıcıya açık web arayüzü ile birlikte tam katmanlı bir backend mimarisi içerir.

**🇬🇧 English:**  
YummyApplication is a location-based social network platform that allows users who visit the same business to interact with each other. This repository includes a layered backend architecture with a RESTful API for the mobile app, an admin panel, and a public web interface.

---

## 🧩 🇹🇷 Proje Yapısı / 🇬🇧 Solution Structure

```bash
YummyApplication.sln
│
├── Core
│   ├── yummyApp.Application     # CQRS, Services, Rules, BackgroundJobs
│   └── yummyApp.Domain          # Entities, Events, Enums, Identity
│
├── Infrastructure
│   ├── yummyApp.Infrastructure  # External Services (Google, Storage, Tokens)
│   └── yummyApp.Persistance     # EF Core, Repositories, UoW, Interceptors, Configurations
│
├── yummyApp.Api                 # REST API for mobile client
├── yummyApp.Web                 # Admin panel + user-facing web app
├── Tests                        # Unit test projects
└── Solution Items               # README.md, .editorconfig etc.
```

---

## 🧱 🇹🇷 Kullanılan Teknolojiler / 🇬🇧 Tech Stack

### ⚙️ Genel Bileşenler / General Components

| Katman / Layer     | Teknolojiler / Technologies     |
| ------------------ | ------------------------------- |
| Backend            | ASP.NET Core 9 (Minimal API)    |
| Web                | ASP.NET Core MVC / Razor        |
| Database           | SQL Server + EF Core            |
| Authentication     | Firebase (Google / Apple Login) |
| Authorization      | JWT + Custom Claims             |
| File Storage       | Google Cloud Storage            |
| Push Notifications | Firebase Cloud Messaging (FCM)  |
| Logging            | Serilog                         |
| Deployment         | GitHub Actions + FTP (Plesk)    |

---

## 🧠 🇹🇷 Mimarî Yapılar / 🇬🇧 Architectural Patterns

**🇹🇷**  
Projede, genişleyebilirlik, test edilebilirlik ve temiz kod prensiplerine uygun olarak modern yazılım mimarileri uygulanmıştır. Katmanlar arası bağımlılıkların en aza indirilmesi ve iş kurallarının merkezi olarak yönetilebilmesi amacıyla **Onion Architecture** temelinde geliştirilmiştir.

**🇬🇧**  
The project is built upon modern software architecture principles such as scalability, testability, and clean code. It follows **Onion Architecture** to ensure a loosely coupled structure where business logic remains at the core.

#### 🧩 Kullanılan Yapılar / Implemented Patterns

| Yapı / Pattern                   |
| -------------------------------- |
| Onion Architecture               |
| CQRS (Command-Query Separation)  |
| MediatR                          |
| JWT Authentication               |
| Identity Integration             |
| Unit of Work                     |
| Repository Pattern               |
| AutoMapper                       |
| TokenHandler                     |
| Google API Integration           |
| Background Services              |
| Validation Behaviors             |
| Performance Logging              |
| Entity Configurations            |
| Business Rules Layer             |
| Global Exception Middleware      |
| Request/Response Wrapping        |
| FluentValidation Integration     |
| Custom Authorization Attributes  |
| Interceptors (Logging / Metrics) |

---

## 🚀 🇹🇷 Kurulum / 🇬🇧 Installation

### 🎯 Backend (API):

```bash
1. .NET 9 SDK yüklü olmalı / Ensure .NET 9 SDK is installed
2. appsettings.json yapılandır / Configure appsettings.json
3. dotnet ef database update
4. dotnet run --project yummyApp.Api
```

### 🎯 Web (Admin Panel + Web UI):

```bash
1. Gerekli bağlantı ayarlarını yap / Configure connection settings
2. dotnet run --project yummyApp.Web
```

> 📱 Mobil uygulama ayrı bir repoda yer almaktadır.  
> 📱 The mobile app is maintained in a separate repository.

---

## 🤝 🇹🇷 Katkı ve Geri Bildirim / 🇬🇧 Contributing

**🇹🇷** Geri bildirimde bulunmak ya da katkı sağlamak isterseniz `issue` açabilir veya `pull request` gönderebilirsiniz.  
**🇬🇧** Feel free to open an issue or submit a pull request if you'd like to contribute.

---

## 📄 🇹🇷 Lisans / 🇬🇧 License

**🇹🇷** Bu proje açık kaynak değildir. Tüm hakları saklıdır.  
**🇬🇧** This project is not open-source. All rights reserved.
