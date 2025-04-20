# Proje Ana Yapısı ve Kod İskeleti

## 1. Katmanlı Mimari

```
FthAdmin
│
├── src
│   ├── FthAdmin.Api           # ASP.NET Core 9.0 Web API (Giriş noktası)
│   ├── FthAdmin.Application   # CQRS, MediatR, iş mantığı, UseCase/Features
│   ├── FthAdmin.Domain        # Entityler, Domain Eventler, DDD
│   ├── FthAdmin.Infrastructure # DB erişimi, dış servisler, Vault entegrasyonu
│   └── FthAdmin.Persistence   # EF Core, SQL Server context, migration
│
├── client
│   └── fthadmin-react         # React ile frontend (Vite/CRA)
│
├── docs                      # Dokümantasyon
└── tests                     # Unit/Integration testleri
```

## 2. Temel Kod İskeleti

### FthAdmin.Api (Program.cs)
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(); // Application katmanı DI
builder.Services.AddPersistenceServices(builder.Configuration); // DB
builder.Services.AddInfrastructureServices(builder.Configuration); // Vault vb.
builder.Services.AddIdentityServices(builder.Configuration); // Identity, MFA

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### FthAdmin.Application (CQRS/Feature Örneği)
```csharp
// Features/User/LoginUserCommand.cs
public record LoginUserCommand(string Email, string Password) : IRequest<LoginResultDto>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResultDto>
{
    // ...
    public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcı doğrulama, MFA kontrolü, JWT üretimi
    }
}
```

### FthAdmin.Domain (Entity Örneği)
```csharp
public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool MFAEnabled { get; set; }
    // ...
}
```

### FthAdmin.Infrastructure (Vault Servisi Örneği)
```csharp
public interface ISecretManager
{
    Task<string> GetSecretAsync(string key);
    Task SetSecretAsync(string key, string value);
}

public class VaultSecretManager : ISecretManager
{
    // HashiCorp Vault API ile entegrasyon
}
```

### FthAdmin.Persistence (DbContext Örneği)
```csharp
public class FthAdminDbContext : IdentityDbContext<User>
{
    public DbSet<Backup> Backups { get; set; }
    public DbSet<Document> Documents { get; set; }
    // ...
}
```

### client/fthadmin-react (Klasör Yapısı)
```
src/
  ├── components/
  ├── features/
  │     ├── auth/
  │     ├── backup/
  │     ├── document/
  │     └── ...
  ├── services/
  ├── App.tsx
  └── main.tsx
```

## 3. Güvenlik Notları
- Parolalar Vault (HashiCorp) ile saklanacak, uygulama içinden erişim için servis katmanı olacak.
- MFA zorunlu olacak (ASP.NET Core Identity + 2FA).
- Tüm API uçları için JWT veya Cookie tabanlı kimlik doğrulama.
- Rollere göre yetkilendirme.
- Sensitive veri erişiminde ekstra doğrulama (örn. tekrar MFA).

## 4. Geliştirme Adımları
1. Proje klasörlerini oluştur.
2. Katmanları ayırıp temel bağımlılıkları kur.
3. Identity, MFA ve Vault entegrasyonunu başlat.
4. CQRS/Feature bazlı örnek endpointler ekle.
5. React frontend'i başlat ve auth akışını hazırla.

---

Daha detaylı kod örnekleri veya belirli bir alanın tam implementasyonu için bana iletmen yeterli!
