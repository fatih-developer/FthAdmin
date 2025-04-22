#region code: fatih.unal date: 2025-04-22
# Build sırasında .csproj ve AssemblyInfo.cs dosyalarındaki versiyonu otomatik güncelleyen script

# Build numarasını oku veya başlat
$buildFile = "BuildNumber.txt"
if (!(Test-Path $buildFile)) { Set-Content $buildFile 1 }
$buildNo = [int](Get-Content $buildFile)
$buildNo++
Set-Content $buildFile $buildNo

# Versiyon formatı: 1.0.0.buildNo
$version = "1.0.0.$buildNo"

# Güncellenecek projeler
$projects = @(
    "FthAdmin.Api/FthAdmin.Api.csproj",
    "FthAdmin.Application/FthAdmin.Application.csproj",
    "FthAdmin.Core/FthAdmin.Core.csproj",
    "FthAdmin.Domain/FthAdmin.Domain.csproj",
    "FthAdmin.Infrastructure/FthAdmin.Infrastructure.csproj",
    "FthAdmin.Persistence/FthAdmin.Persistence.csproj"
)

foreach ($proj in $projects) {
    (Get-Content $proj) -replace '<Version>.*</Version>', "<Version>$version</Version>" | Set-Content $proj
}

# AssemblyInfo.cs dosyalarını güncelle
$assemblyFiles = Get-ChildItem -Path . -Filter AssemblyInfo.cs -Recurse
foreach ($file in $assemblyFiles) {
    $content = Get-Content $file.FullName
    $content = $content -replace 'AssemblyVersion\(".*"\)', ("AssemblyVersion(`"{0}`")" -f $version)
    $content = $content -replace 'AssemblyFileVersion\(".*"\)', ("AssemblyFileVersion(`"{0}`")" -f $version)
    Set-Content $file.FullName $content;
}

Write-Host "Projelerde ve AssemblyInfo.cs dosyalarında versiyon güncellendi: $version"
#endregion
