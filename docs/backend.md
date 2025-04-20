# Backend (API) Projesi Oluşturma Rehberi

## 1. API Projesi Nasıl Oluşturulur?

### a) Klasör Yapısı
```
d:/Project/FthAdmin/backend
│
├── FthAdmin.Api           # ASP.NET Core Web API projesi (giriş noktası)
├── FthAdmin.Application   # İş mantığı, CQRS, MediatR
├── FthAdmin.Domain        # Entityler, Domain Eventler
├── FthAdmin.Infrastructure # Dış servisler, Vault, e-posta vs.
└── FthAdmin.Persistence   # EF Core, DbContext, Migration
```

### b) Komutlar ile Proje Oluşturma

1. Ana klasöre geç:  
   ```powershell
   cd d:/Project/FthAdmin/backend
   ```
2. Çekirdek projeleri oluştur:
   ```powershell
   dotnet new webapi -n FthAdmin.Api
   dotnet new classlib -n FthAdmin.Application
   dotnet new classlib -n FthAdmin.Domain
   dotnet new classlib -n FthAdmin.Infrastructure
   dotnet new classlib -n FthAdmin.Persistence
   ```
3. Proje referanslarını ekle:
   ```powershell
   dotnet add FthAdmin.Api reference FthAdmin.Application
   dotnet add FthAdmin.Api reference FthAdmin.Infrastructure
   dotnet add FthAdmin.Api reference FthAdmin.Persistence
   dotnet add FthAdmin.Application reference FthAdmin.Domain
   dotnet add FthAdmin.Infrastructure reference FthAdmin.Domain
   dotnet add FthAdmin.Persistence reference FthAdmin.Domain
   ```
4. Gerekli NuGet paketlerini yükle (örnek):
   ```powershell
   dotnet add FthAdmin.Application package MediatR
   dotnet add FthAdmin.Persistence package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add FthAdmin.Api package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add FthAdmin.Infrastructure package VaultSharp
   ```

## 2. Entity (Varlık) Tasarımı

### Temel Entityler:
- **User**: Kullanıcılar (adminler)
- **Server**: Yönetilecek sunucular
- **Database**: Sunucuya bağlı veritabanları
- **Credential**: Parola ve bağlantı bilgileri (şifreli tutulacak)
- **Backup**: Yedekleme kayıtları
- **Document**: Dokümanlar

### Entity Örnekleri

#### User.cs
```csharp
public class User : IdentityUser<Guid>
{
    public bool MFAEnabled { get; set; }
    // Ek alanlar: Ad, soyad, rol vb.
}
```

#### Server.cs
```csharp
public class Server : BaseEntity
{
    public string Name { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public ICollection<Database> Databases { get; set; }
}
```

#### Database.cs
```csharp
public class Database : BaseEntity
{
    public string Name { get; set; }
    public Guid ServerId { get; set; }
    public Server Server { get; set; }
}
```

#### Credential.cs
```csharp
public class Credential : BaseEntity
{
    public string Username { get; set; }
    public string PasswordEncrypted { get; set; } // Vault ile saklanacak
    public Guid RelatedEntityId { get; set; } // Server veya Database
    public string EntityType { get; set; } // 'Server' veya 'Database'
}
```

#### Backup.cs
```csharp
public class Backup : BaseEntity
{
    public string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid DatabaseId { get; set; }
    public Database Database { get; set; }
}
```

#### Document.cs
```csharp
public class Document : BaseEntity
{
    public string Name { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }
}
```

## 3. Sonraki Adımlar
- DbContext sınıfını oluştur.
- Migration işlemlerini başlat.
- API controllerlarını ve CQRS handlerlarını ekle.
- Vault ve MFA entegrasyonunu uygula.

Daha fazla detay veya örnek kodlar için bana iletebilirsin!
