# �o�O�����[�c�� RentalCarV2.Web �M�׵{���X�޿軡��

## �M�׷��z

RentalCarV2.Web �O�����[�c�]Hexagonal Architecture�^���� **Adapter-In�]��J�A�t���^** �h�A�t�d�B�z�~���ШD�ñN���ഫ�����ε{���֤߯���z�Ѫ��榡�C���M�ױĥ� ASP.NET Core 8.0 �}�o�A�]�t Razor Pages �M Web API ����C

---

## �M�׵��c

```
RentalCarV2.Web/
�u�w�w Controllers/          # API �M MVC ���
�u�w�w Models/              # ���ϼҫ��M DTO
�u�w�w Views/               # Razor �����ɮ�
�|�w�w Program.cs           # ���ε{���i�J�I�M�A�Ȱt�m
```

---

## �֤��ɮ׻���

### 1. Program.cs - ���ε{���i�J�I

**�γ~�G** ���ε{�����ҰʩM�t�m�ɮסA�t�d���U�Ҧ��A�ȡB�����n��M���ѳ]�w�C

#### �D�n�t�m�϶��G

##### 1.1 �A�Ȯe���t�m�]Service Container Configuration�^

**AppSettings �t�m�G**
```csharp
IConfigurationSection configRoot = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(configRoot);
```
- ���J���ε{���]�w��
- �ϥοﶵ�Ҧ��]Options Pattern�^�޲z�t�m

**MVC �A�ȵ��U�G**
```csharp
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
```
- `AddControllersWithViews()`�G���U MVC ����M���Ϥ䴩
- `AddHttpContextAccessor()`�G���� HttpContext ���̿�`�J�䴩

##### 1.2 �������Ұt�m�]Authentication Configuration�^

```csharp
builder.Services.AddAuthentication(configure =>
{
    configure.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configure.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToReturnUrl = async (context) =>
        {
            context.HttpContext.Response.Cookies.Delete(AuthenticateRequest.LOGIN_USER_INFO);
        }
    };
});
```
- �ϥ� Cookie ���Ҿ���
- Cookie ���Ĵ����G3 ����
- HttpOnly �]�w���� XSS ����
- �۰ʲM�z�n�J��T Cookie

##### 1.3 �����b����Ƶ��U

```csharp
builder.Services.AddScoped<IEnumerable<IAccountEnt>, List<AccountEnt>>(account =>
    new List<AccountEnt>(
        new AccountEnt[]
        {
            new AccountEnt() { Id=1, UserID="gelis", AID="F123456789" },
            new AccountEnt() { Id=2, UserID="mary", AID="F123456780" },
            new AccountEnt() { Id=3, UserID="allan", AID="F123456781" },
        }));
```
- ���U�������ϥΪ̱b���M��
- �Ω�}�o�M��������
- �]�t�T�Ӵ��ձb���Ggelis, mary, allan

##### 1.4 �����[�c�̿�`�J�t�m

**�ϥΪ̪A�ȡG**
```csharp
builder.Services.AddScoped<IUserService, UserService>();
```
- ���U�ϥΪ̨������ҩM���v�A��

**�����A�ȡ]�����[�c�ݤf�M�A�t���^�G**
```csharp
builder.Services.AddScoped<RentalCarRepository>();
builder.Services.AddScoped<IRentalCarRepository, RentalCarRepository>(service => service.GetRequiredService<RentalCarRepository>());
builder.Services.AddScoped<IRentalCarUseCase, RentalCarRepository>(service => service.GetRequiredService<RentalCarRepository>());
builder.Services.AddScoped<RentalCarSerAppServices>();
```
- `RentalCarRepository`�G��ƫ��[�ƾA�t���]Adapter-Out�^
- `IRentalCarRepository`�G��X�ݤf�]Port-Out�^����
- `IRentalCarUseCase`�G��J�ݤf�]Port-In�^����
- `RentalCarSerAppServices`�G���ΪA�ȼh

**URI �X�R�A�ȡG**
```csharp
builder.Services.AddScoped<IUriExtensions, UriExtensions>();
```
- ���� URI �������X�R�\��

##### 1.5 �����n��޹D�t�m�]Middleware Pipeline�^

```csharp
app.UseHttpsRedirection();        // HTTPS ���ɦV
app.UseStaticFiles();              // �R�A�ɮתA��
app.UseRouting();                  // ���Ѱt�m
app.UseAuthentication();           // ��������
app.UseAuthorization();            // ���v����
app.UseJWTAuthorizedMiddleware();  // JWT �ۭq�����n��
```

**���Ѱt�m�G**
```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```
- �w�]���ѡGHome/Index
- �䴩��ܩ� id �Ѽ�

---

### 2. Controllers/HomeController.cs - MVC �������

**�γ~�G** �B�z�������D�n�����ɯ�M���~�B�z�C

#### ���O�����G

```csharp
public class HomeController : Controller
```
- �~�Ӧ� `Controller` �����O
- �B�z MVC ���ϽШD

#### �غc�禡�G

```csharp
public HomeController(ILogger<HomeController> logger)
```
- **�ѼơG** `ILogger<HomeController>` - ASP.NET Core ���ت���x�O����
- **�γ~�G** �̿�`�J��x�A�ȡA�Ω�O�����ε{�������T

#### ��k�����G

##### 2.1 Index() - ����

```csharp
public IActionResult Index()
{
    return View();
}
```
- **�γ~�G** ��ܺ�������
- **�^�ǡG** ��V `Views/Home/Index.cshtml` ����
- **���ѡG** `/` �� `/Home/Index`

##### 2.2 Privacy() - ���p�v�F������

```csharp
public IActionResult Privacy()
{
    return View();
}
```
- **�γ~�G** ������p�v�F������
- **�^�ǡG** ��V `Views/Home/Privacy.cshtml` ����
- **���ѡG** `/Home/Privacy`

##### 2.3 Error() - ���~�B�z����

```csharp
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
```
- **�γ~�G** ��ܿ��~����
- **�ݩʡG** `[ResponseCache]` - �T�Χ֨��A�T�O���~��T�Y�����
- **�^�ǡG** �]�t�ШD�l�� ID �����~���ϼҫ�
- **�l�� ID �ӷ��G** 
  - �u���ϥ� `Activity.Current.Id`�]�������l�ܡ^
  - �ƥΨϥ� `HttpContext.TraceIdentifier`�]���a�l�ܡ^

---

### 3. Controllers/RentalCarApiController.cs - ���� API ���

**�γ~�G** ���ѯ����A�Ȫ� RESTful API ���I�A�O�����[�c�����D�n��J�A�t���]Adapter-In�^�C

#### ���O�����G

```csharp
public class RentalCarApiController : EasyArchiectV2ControllerBase
```
- �~�Ӧ� `EasyArchiectV2ControllerBase` �ۭq�򩳱��
- ��X�������ҩM��x�\��

#### �غc�禡�G

```csharp
public RentalCarApiController(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    IRentalCarUseCase rentalCarUseCase)
    : base(httpContextAccessor, userService)
```
- **�Ѽƻ����G**
  - `IHttpContextAccessor`�GHTTP �W�U��s����
  - `IUserService`�G�ϥΪ̨������ҪA��
  - `IRentalCarUseCase`�G�����~���޿誺��J�ݤf�]Port-In�^
- **�γ~�G** �̿�`�J�һݪA�ȡA�öǻ��������O

#### API ���I��k�G

##### 3.1 GetPersonsAsync() - ���o�H���M��]�d�ҡ^

```csharp
[NeedAuthorize]
[HttpGet]
[Route("GetPersons")]
[ApiLogonInfo]
public async Task<IEnumerable<Person>> GetPersonsAsync()
```
- **�γ~�G** �d�� API�A�i�ܨ������ҩM��x�\��
- **HTTP ��k�G** GET
- **���ѡG** `/GetPersons`
- **�ݩʡG**
  - `[NeedAuthorize]`�G�ݭn��������
  - `[ApiLogonInfo]`�G�O�� API �I�s��T
- **�^�ǡG** �H�����󶰦X�]�d�Ҹ�ơ^
- **�^���N�X�G**
  - 200�G���\
  - 401�G�����v

##### 3.2 GetAllCarsAsync() - ���o�Ҧ��i���Ψ���

```csharp
[HttpGet]
[Route("GetAllCars")]
[ApiLogonInfo]
public async Task<IEnumerable<IVehicle>> GetAllCarsAsync()
{
    return await Task.FromResult(_rentalCarUseCase.GetAllCars());
}
```
- **�γ~�G** �d�ߩҦ��i���Ϊ������M��
- **HTTP ��k�G** GET
- **���ѡG** `/GetAllCars`
- **�ݩʡG** `[ApiLogonInfo]` - �O�� API �I�s��T
- **�^�ǡG** `IVehicle` �����������X
- **�~���޿�G** �z�L `IRentalCarUseCase.GetAllCars()` ���o�������
- **�^���N�X�G**
  - 200�G���\�^�Ǩ����M��
  - 500�G���A�����~
- **�S�I�G** �L�ݨ������ҧY�i�s��

##### 3.3 ToRentalCarAsync() - �������Χ@�~�]�w���ѡ^

```csharp
//[HttpPost]
//[Route("ToRentalCar")]
//public async Task<bool?> ToRentalCarAsync(RentalCarDto rentalCarDto)
//{
//    Car car = new Car(ModelType.Toyota);
//    return await Task.FromResult(_rentalCarUseCase.ToRentCar(car));
//}
```
- **�γ~�G** ���樮�����Χ@�~�]�ݹ�@�^
- **HTTP ��k�G** POST
- **���ѡG** `/ToRentalCar`
- **�ѼơG** `RentalCarDto` - ������ƶǿ骫��
- **�^�ǡG** `bool?` - �����O�_���\
- **���A�G** �ثe�w���ѡA���ݧ����@
- **�ݹ�@�\��G**
  - �����i�Ω��ˬd
  - ���δ�������
  - �����p��
  - ����B�z

---

### 4. Models/ErrorViewModel.cs - ���~���ϼҫ�

**�γ~�G** ���~��������Ƽҫ��A�Ω���ܿ��~�l�ܸ�T�C

#### ���O�����G

```csharp
public class ErrorViewModel
```
- ���~���Ϫ���ƶǿ骫��]DTO�^

#### �ݩʻ����G

##### 4.1 RequestId - �ШD�ѧO�X

```csharp
public string? RequestId { get; set; }
```
- **�γ~�G** �x�s�ШD���ߤ@�ѧO�X
- **���O�G** �i�� null ���r��
- **�ӷ��G** 
  - Activity.Current?.Id�]�������l�ܡ^
  - HttpContext.TraceIdentifier�]���a�l�ܡ^

##### 4.2 ShowRequestId - �O�_��ܽШD ID

```csharp
public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
```
- **�γ~�G** �p���ݩʡA�P�_�O�_�n�b���Ϥ���ܽШD ID
- **�޿�G** �� RequestId �����Ůɦ^�� true
- **�ϥγ����G** �b���~�������Ϥ�������ܰl�ܸ�T

---

### 5. Models/Person.cs - �H����Ƽҫ��]�d�ҡ^

**�γ~�G** �d�Ҹ�Ƽҫ��A��ڱM�������� Models/Dto/VO �M�סC

#### ���O�����G

```csharp
public class Person
```
- �d�ҥΪ���ƶǿ骫��
- ��ĳ���c�ܿW�ߪ� DTO �M��

#### �ݩʻ����G

##### 5.1 ID - �H���ѧO�X

```csharp
public int ID { get; set; }
```
- **�γ~�G** �H�����ߤ@�ѧO�X
- **���O�G** ���

##### 5.2 Name - �H���m�W

```csharp
public string Name { get; set; }
```
- **�γ~�G** �H�����m�W
- **���O�G** �r��
- **��ĳ�G** ��ڱM�����[�J��������ݩʡ]Data Annotations�^

---

## �����[�c����w��

### Adapter-In�]��J�A�t���^¾�d

RentalCarV2.Web �M�צb�����[�c����t **Adapter-In** ������G

1. **�����~���ШD�G**
   - HTTP/HTTPS �ШD
   - RESTful API �I�s
   - ������洣��

2. **�ШD�ഫ�G**
   - �N HTTP �ШD�ഫ�����ε{���֤߯�z�Ѫ����O
   - ���ҿ�J��Ʈ榡
   - �B�z�������ҩM���v

3. **�I�s Port-In�]��J�ݤf�^�G**
   - �z�L `IRentalCarUseCase` �����I�s�~���޿�
   - �������̿�����@�A�O���C���X

4. **�^���ഫ�G**
   - �N�~�ȼh�^�Ǫ�����ഫ�� HTTP �^��
   - �B�z���~�M�ҥ~���p
   - �^�ǾA�� HTTP ���A�X

### �̿��V

```
�~���ШD �� RentalCarV2.Web (Adapter-In) �� IRentalCarUseCase (Port-In) �� Application.RentalCar (�֤߷~��)
```

- **�V���̿�G** Web �h�̿����ή֤ߪ������]Port-In�^
- **���V�~�̿�G** ���ή֤ߤ����D Web �h���s�b
- **�̿����G** �z�L�����ѽ��A�ŦX SOLID ��h

---

## �޳N�S�I

### 1. �������Ҿ���

- **Cookie Authentication�G** ��� Cookie ����������
- **JWT Middleware�G** �ۭq JWT �����n��䴩
- **Authorization Filters�G** `[NeedAuthorize]` �ݩʱ���s���v��

### 2. API ��x�O��

- **ApiLogonInfo�G** �O�� API �I�s��T
- **ApiLogException�G** �O���ҥ~���p�]�w���ѡ^
- **�������l�ܡG** �䴩 Activity �l�ܾ���

### 3. �̿�`�J�Ҧ�

- **Service Lifetime�G** �ϥ� Scoped �ͩR�g��
- **Interface Segregation�G** �����j����h
- **Factory Pattern�G** �ϥ� GetRequiredService �u�t��k

### 4. �D�P�B�{���]�p

- �Ҧ� API ��k�ϥ� `async/await` �Ҧ�
- �^�� `Task<T>` ���O�䴩�D�P�B�ާ@

---

## �̨ι���ĳ

### 1. DTO ����
- �N `Person` ���O���ܿW�ߪ� DTO �M��
- �إ߱M���� `RentalCarDto` ���O

### 2. ���~�B�z
- ��@����ҥ~�B�z��
- �ϥ� `[ApiLogException]` �O�����~

### 3. API ��������
- �[�J API �����޲z
- ���Ѥ��]�t������T�]�p�G`/api/v1/cars`�^

### 4. �������
- �ϥ� Data Annotations ���ҿ�J
- ��@ FluentValidation �i���������

### 5. �w���ʱj��
- ��@ CORS �F��
- �[�J Rate Limiting
- �ϥ� HTTPS �j��ɦV

---

## �`��

RentalCarV2.Web �M�ק@�������[�c�� Adapter-In �h�A���\��{�F�G

? **���`�I�����G** Web �h�u�t�d�ШD�B�z�A���]�t�~���޿�  
? **�̿����G** �z�L�����̿����ή֤�  
? **�i���թʡG** �̿�`�J�ϳ椸���է�e��  
? **�i���@�ʡG** �M�����[�c�h���M¾�d����  
? **�i�X�i�ʡG** ����[�J�s�� API ���I�����Ҿ���

