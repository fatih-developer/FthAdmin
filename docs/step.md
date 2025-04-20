# Fth Admin Api

Proje Geliştirme Detaylı Adımlar (Sadece API)

## 1. Proje ve Klasör Yapısının Oluşturulması
- Katmanlı mimariye uygun ana klasörleri oluştur (Api, Application, Domain, Infrastructure, Persistence, docs, tests).
- Her katman için ayrı .NET projeleri başlat.

## 2. Backend (API) Projesi Kurulumu
- FthAdmin.Api (ASP.NET Core 9.0 Web API) projesini oluştur.
- Application, Domain, Infrastructure, Persistence için class library projeleri oluştur ve Class1.cs dosyalarını sil.
- Proje referanslarını ekle (Api → Application, Infrastructure, Persistence vb.).
- Gerekli NuGet paketlerini yükle: MediatR, EntityFrameworkCore, Identity, VaultSharp vb.

## 3. Entity ve Domain Model Tasarımı
- User, Server, Database, Credential, Backup, Document entitylerini Domain katmanında oluştur.
- BaseEntity ile ortak alanları (Id, CreatedAt vb.) soyutla.

## 4. Persistence ve DbContext
- Persistence katmanında FthAdminDbContext’i yaz.
- Entityleri DbSet olarak ekle.
- SQL Server bağlantı ayarlarını yapılandır.
- İlk migration’ı oluştur ve veritabanını hazırla.

## 5. Kimlik Doğrulama ve MFA
- ASP.NET Core Identity ile kullanıcı yönetimi entegre et.
- 2FA/MFA desteğini aktif et (Authenticator App, SMS veya e-posta ile).
- Rollere göre yetkilendirme kurallarını tanımla.

## 6. Parola ve Gizli Bilgi Yönetimi
- HashiCorp Vault ile Credential entity’sindeki şifreleri şifreli sakla.
- Infrastructure katmanında Vault servis entegrasyonu yaz.
- Parola görüntüleme/kopyalama işlemlerine ekstra doğrulama adımı ekle.

## 7. CQRS ve MediatR Entegrasyonu
- Application katmanında Feature/UseCase bazlı CQRS handlerları oluştur.
- Komut (Command) ve sorgu (Query) işlemlerini ayır.
- Örnek: LoginUserCommand, CreateBackupCommand, GetServerListQuery vb.

## 8. API Controller’larının Oluşturulması
- Api katmanında controller’ları oluştur (UserController, ServerController, BackupController vb.).
- CQRS handler’larını controller’larda kullan.

## 9. Güvenlik ve Test
- Tüm API uçlarını kimlik doğrulama ve yetkilendirme ile koru.
- Sensitive veri erişimlerinde ekstra MFA doğrulaması uygula.
- XSS, CSRF, SQL Injection gibi saldırılara karşı koruma ekle.
- Unit ve integration testleri yaz.

## 10. Yedekleme ve Doküman Yönetimi
- Yedekleme işlemlerini tetikleyen endpointler ekle.
- Yedek dosyalarını şifreli ve erişimi kısıtlı şekilde sakla.
- Doküman yükleme, listeleme ve indirme işlemleri için API akışını oluştur.

## 11. Loglama ve İzleme
- Kullanıcı ve işlem loglarını kaydet.
- Şüpheli aktiviteleri izlemek için log analiz altyapısı kur.

## 12. Devreye Alma ve Dokümantasyon
- Gerekli ortam ayarlarını (appsettings, connection string, vault adresi vb.) yapılandır.
- Proje dokümantasyonunu güncel tut.
- Deploy ve backup prosedürlerini yaz.
