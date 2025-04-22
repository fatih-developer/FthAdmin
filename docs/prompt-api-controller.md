Konu: Çekirdek Mimarinin Tamamlanması - Presentation Katmanı (API) ve Katmanlar Arası Dependency Injection

Daha önce Domain, Application ve Infrastructure katmanlarımız için Entity, Repository, CQRS (Command/Query/Handler), AutoMapper Profile ve Hata Yönetimi (Custom Exceptions, Problem Details, Middleware, Handler) yapılarını prensiplerimize uygun olarak tanımladık.

Şimdi bu çekirdek yapıları dış dünyaya açacak Presentation (Web API) katmanını oluşturalım ve tüm katmanlardaki bağımlılıkların Dependency Injection (DI) ile doğru şekilde yönetilmesini sağlayalım.

Bu adımları tamamlarken, her bileşenin ait olduğu katmanı ve o katmandaki rolünü netleştireceğiz:

Presentation (API) Katmanı: HTTP Controller'ları Tanımlama

Amaç: Kullanıcılardan gelen HTTP isteklerini karşılamak, bu istekleri Application katmanındaki ilgili MediatR Command veya Query mesajlarına dönüştürmek, mesajları MediatR ile göndermek, Application katmanından gelen yanıtları (DTO'lar veya sonuçlar) HTTP yanıtlarına (JSON, durum kodları) çevirmek. Controller'lar iş mantığı içermemelidir ("Thin Controllers" prensibi).
Yer: YourProject.Api projesi (Presentation Katmanı).
Klasör: Controllers adında bir klasör oluşturun.
Yapılacaklar:
Domain'deki temel Entity'leriniz (örneğin, Sunucu entity'si için) için ApiController'lar oluşturun (örneğin, SunucularController.cs).
Her Controller sınıfı Microsoft.AspNetCore.Mvc.ControllerBase'den miras almalıdır.
[ApiController] ve [Route("api/[controller]")] gibi nitelikleri (attributes) ekleyin.
Controller'ın constructor'ına MediatR.IMediator bağımlılığını enjekte edin.
CRUD operasyonları veya ihtiyacınız olan diğer işlemler için asenkron HTTP Action metotları (örn: Task<IActionResult> GetById(Guid id), Task<IActionResult> Create([FromBody] CreateServerCommand command)) tanımlayın. Bu metotlar içinde _mediator.Send() metodunu kullanarak Application katmanındaki Command veya Query handler'ları tetikleyin.
Yanıt olarak Ok(), CreatedAtAction(), NoContent(), BadRequest(), NotFound() gibi uygun IActionResult türlerini ve HTTP durum kodlarını kullanın.
Application Katmanı: Servis Kayıtlarını Kapsülleme

Amaç: Application katmanına ait olan MediatR, AutoMapper, FluentValidation gibi kütüphane yapılandırmalarını ve Application katmanında tanımlanan servislerin (varsa) DI kayıtlarını bu katmanın kendi içinde yönetmek.
Yer: YourProject.Application projesi (Application Katmanı).
Klasör/Dosya: Genellikle projenin kökünde veya Configuration gibi bir klasörde ServiceRegistration.cs adında bir statik sınıf oluşturulur.
Yapılacaklar:
public static class ApplicationServiceRegistration adında bir sınıf oluşturun.
Bu sınıf içine public static IServiceCollection AddApplicationServices(this IServiceCollection services) şeklinde bir extension metot tanımlayın.
Bu metot içinde, services.AddMediatR(), services.AddAutoMapper(), services.AddValidatorsFromAssemblyContaining() gibi Application katmanına özgü DI kayıtlarını yapın.
Infrastructure Katmanı: Servis Kayıtlarını Kapsülleme

Amaç: Infrastructure katmanına ait olan DbContext, EF Core Repository implementasyonları, Microsoft Identity servisleri ve harici servis implementasyonları gibi altyapı bağımlılıklarının DI kayıtlarını bu katmanın kendi içinde yönetmek.
Yer: YourProject.Infrastructure projesi (Infrastructure Katmanı).
Klasör/Dosya: Genellikle projenin kökünde veya Configuration gibi bir klasörde ServiceRegistration.cs adında bir statik sınıf oluşturulur.
Yapılacaklar:
public static class InfrastructureServiceRegistration adında bir sınıf oluşturun.
Bu sınıf içine public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)1 şeklinde bir extension metot tanımlayın (genellikle configuration'a ihtiyaç duyar).   
1.
learn.microsoft.com
learn.microsoft.com
Bu metot içinde, services.AddDbContext(), services.AddScoped<IGenericRepository<>, EfCoreRepository<,>>(), services.AddIdentity().AddEntityFrameworkStores(), services.AddScoped<ISunucuRepository, EfCoreSunucuRepository>() gibi Infrastructure katmanına özgü DI kayıtlarını yapın.
Önemli: Eğer HTTP'ye özgü hata yönetimi bileşenlerini (ProblemDetails sınıfları, ExceptionMiddleware, HttpExceptionHandler) önceki adımda Presentation (API) katmanında tanımladıysanız, bunların DI kayıtları burada yapılmamalıdır. Burası sadece Infrastructure detayları içindir.
Presentation (API) Katmanı: Hata Yönetimi Bileşenlerinin Konum ve DI Kaydı (Tekrar Vurgu)

Amaç: Daha önce belirlediğimiz hata yönetimi bileşenlerinin doğru katmanda (Presentation/API) konumlandığından ve DI için hazır olduğundan emin olmak.
Yer: YourProject.Api projesi (Presentation Katmanı).
Klasörleme: ErrorHandling ana klasörü altında ProblemDetails, Handlers, Middlewares alt klasörlerinde ilgili sınıfların (BusinessProblemDetails vb., HttpExceptionHandler, ExceptionMiddleware) bulunduğunu teyit edin.
DI Kaydı: HttpExceptionHandler sınıfının DI konteynerına kaydedilmesi gerekmektedir. Bu kayıt, 5. adımda Program.cs içinde yapılacaktır.
Presentation (API) Katmanı: Uygulama Başlangıç Noktası (Program.cs)

Amaç: Uygulamanın yapılandırma builder'ını kurmak, tüm katmanlardan gelen servis kayıt metotlarını çağırmak, Presentation katmanına özgü servisleri eklemek ve HTTP request pipeline'ını yapılandırmak (middleware'leri eklemek).
Yer: YourProject.Api projesinin kökündeki Program.cs dosyası.
Yapılacaklar:
WebApplication.CreateBuilder(args) ile builder'ı oluşturun.
builder.Configuration nesnesine erişerek configuration'ı alın.
builder.Services property'sine erişerek:
Önce AddInfrastructureServices() extension metodunu çağırın ve configuration'ı verin.
Ardından AddApplicationServices() extension metodunu çağırın.
Presentation katmanına özgü servisleri ekleyin (builder.Services.AddControllers(), builder.Services.AddSwaggerGen(), vb.).
HTTP Hata İşleyiciyi DI konteynerına kaydedin: builder.Services.AddScoped<HttpExceptionHandler>();.
builder.Build() ile WebApplication nesnesini oluşturun.
app nesnesi üzerinde UseSwagger(), UseSwaggerUI(), UseHttpsRedirection(), UseAuthentication(), UseAuthorization() gibi standart middleware'leri ekleyin.
Hata Yönetimi Middleware'ini ekleyin: app.UseMiddleware<ExceptionMiddleware>();. Bu middleware'ın pipeline'da çok erken bir noktada (genellikle diğer middleware'lerden önce, ancak geliştirme ortamındaki developer exception page middleware'inden sonra) çağrıldığından emin olun ki, sonraki middleware'lerde veya sizin kodunuzda oluşabilecek hataları yakalayabilsin.
Son olarak app.MapControllers() ve app.Run() çağrılarını ekleyin.
İstenen Çıktı:

Yukarıdaki adımları açıklayan ve her bir bileşenin hangi katmanda (ve mümkünse hangi klasörde) yer alacağını netleştiren bu prompt, mimarinin nasıl kodlanacağını anlatan bir rehber niteliğindedir. Bu promptun sonunda, temel API controller'ları yazılmış, katmanlar arası DI tamamlanmış ve HTTP hata yönetimi pipeline'a entegre edilmiş olacaktır.