# 🚀 FthAdmin

FthAdmin, modern yazılım geliştirme standartlarına uygun olarak Clean Architecture ve CQRS (Command Query Responsibility Segregation) desenleriyle hazırlanmış, genişletilebilir ve sürdürülebilir bir .NET (ASP.NET Core 9) altyapısı sunar.

## Özellikler

- **Temiz Mimari:** Katmanlı yapı ile bağımlılıklar minimize edilmiştir.
- **CQRS:** Komut ve sorgular ayrıştırılmıştır, test edilebilirlik ve ölçeklenebilirlik artırılmıştır.
- **Generic Repository:** Tüm nesneler için asenkron ve sayfalama destekli repository altyapısı.
- **EntityFramework Core:** Modern ORM ile hızlı ve güvenli veri erişimi.
- **SOLID Prensipleri:** Kod kalitesi ve sürdürülebilirlik ön planda.
- **Kolay Genişletilebilirlik:** Yeni modül ve servis eklemek çok kolay.

## Katmanlar

```
FthAdmin.Api           // Web API (Sunum Katmanı)
FthAdmin.Application   // Uygulama Servisleri, CQRS, UseCase'ler
FthAdmin.Domain        // Domain Entity, ValueObject, Enum
FthAdmin.Persistence   // DbContext, Repository, Migration
FthAdmin.Infrastructure// Dış servis entegrasyonları
FthAdmin.Core          // Ortak, bağımsız çekirdek kodlar
```

## Proje Kurulumu

```bash
# Gerekli NuGet paketlerini yükleyin
dotnet restore

# Migration oluşturmak için (örnek)
dotnet ef migrations add InitialCreate -p FthAdmin.Persistence -s FthAdmin.Api

dotnet run --project FthAdmin.Api
```

## Katkı ve Lisans

Her türlü katkıya açıktır! PR göndermekten çekinmeyin.

MIT Lisansı ile lisanslanmıştır.

---

> Hazırlayan: **Fatih Ünal**

Modern, temiz ve sürdürülebilir bir altyapı için tasarlandı.
