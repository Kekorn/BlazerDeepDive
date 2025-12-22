Telepített csomagok:
    <PackageReference Include="Microsoft.AspNetCore.Authentication.Google" Version="8.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.11">

Modellek létrehozása:
    AppUser: felhasználói adatok tárolására
    AppConstants: állandó értékek tárolására
    AppDbContext: az Entity Framework Core adatbázis kontextus osztálya

A program.cs fájlban a dbContext szolgáltatást hozzáadjuk a szolgáltatásokhoz:
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
Az appsettings.json fájlban hozzáadjuk az adatbázis kapcsolat karakterláncát:
        "ConnectionStrings": {
        "DefaultConnection": "Data Source=Auth.db"
        }
Ezzel biztosítjuk a paraméterezhetõséget és a könnyû konfigurálhatóságot az alkalmazás számára.

Süti és hitelesítési beállítások konfigurálása a program.cs fájlban:
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
    builder.Services.AddAuthentication(AppConstants.AuthScheme)
        .AddCookie(AppConstants.AuthScheme, cookieOptions =>
        {
            cookieOptions.Cookie.Name = AppConstants.AuthScheme;
        })
        .AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
        {
            googleOptions.ClientId = "";
            googleOptions.ClientSecret = "";
            googleOptions.AccessDeniedPath = "/access-denied";
        });

Hozzáadjuk a hitelestési és azonosítási középrétegeket a HTTP kérés feldolgozási csõvezetéken:
    app.UseAuthentication();
    app.UseAuthorization();

A következõ lépésként lekérjük az ügyfélazonosítót és titkos kulcsot a Google fejlesztõi konzoljából
(developers.google.com), majd beállítjuk ezeket a googleOptions objektumban a program.cs fájlban.




Elõkészületek és csomagok: A folyamat a projekt létrehozásával és a szükséges NuGet csomagok
telepítésével kezdõdik. Szükség van a Microsoft.AspNetCore.Authentication.Google csomagra a 
hitelesítéshez, valamint az Entity Framework Core (EF Core) és SQLite eszközökre az adatok 
tárolásához.

Adatmodell és Adatbázis: Létrehozunk egy AppUser modellt, amely a felhasználó nevét, e-mail 
címét és azonosítóját tárolja. Érdekesség, hogy jelszót nem kell tárolnunk, mivel azt a 
Google kezeli. Ezt követõen regisztráljuk a DB Contextet a Program.cs fájlban .

Google Cloud Console beállítása: Ez a szakasz kritikus a mûködéshez. A videó bemutatja, 
hogyan kell projektet létrehozni a Google Cloud felületén, konfigurálni az OAuth beleegyezõ 
képernyõt, és legenerálni a kliensazonosítót (Client ID) és a titkos kulcsot (Client Secret). Fontos a visszahívási URL pontos megadása az alkalmazásunk portja alapján [09:45].

Backend konfiguráció: A megszerzett kulcsokat a Program.cs fájlban rögzítjük, ahol 
beállítjuk a Cookie-alapú hitelesítést és a Google szolgáltatásait .

Bejelentkezési logika: A videó bemutatja a Login.razor oldal létrehozását, amely elindítja 
a hitelesítési folyamatot a Google felé. Sikeres belépés után egy külön oldalon dolgozzuk 
fel a visszatérõ adatokat (claim-eket), elmentve az új felhasználót az adatbázisba, és 
szerepköröket (Admin vagy User) rendelünk hozzájuk.

Védelem és Tesztelés: Végül az útvonalakat az AuthorizeView komponenssel védjük le. 
A bemutató végén láthatjuk a gyakorlatban is a folyamatot: a Google-fiókkal való 
bejelentkezést, a hozzájárulás megadását, majd a sikeres belépést, ahol az alkalmazás 
már név szerint ismeri a felhasználót.